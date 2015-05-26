using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GCinemaCraft
{
    public partial class formGCinemaCraft : Form
    {
        Stopwatch sw;
        WebClient webClient;
        private int selected = -1;
        public delegate void DownloadFinished(string path);
        private ArrayList xhashDownloadList = new ArrayList();
        private ArrayList xhashToDelete = new ArrayList();
        private int xhashCount = 0;
        delegate Control getControlCallback(Control control);
        private bool shouldShowDownloadComplted { get; set; }
        public formGCinemaCraft()
        {
            InitializeComponent();
            shouldShowDownloadComplted = true;
            lblMessage.Text = cMessage.Random;
            
            try
            {
                string config = File.ReadAllText(cPath.Config);
                new cConfig().parseConfig(config);
                
                sw = new Stopwatch();
            }
            catch (Exception e)
            {
                killProcess(Process.GetCurrentProcess(), e.ToString());
                return;
            }
            
            for (int i = 0; i < cLauncher.Launcher.Count; i++)
            {
                cLauncher.Index = i;
                lbLauncher.Items.Add(cLauncher.Name);
            }
        }
        
        private string getDialogPath(string description)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowNewFolderButton = true;
            folderBrowserDialog.Description = description;
            
            folderBrowserDialog.ShowDialog();
            
            string selectedPath = folderBrowserDialog.SelectedPath;
            
            if (string.IsNullOrEmpty(selectedPath))
            {
                stopOperations();
            }
            
            return selectedPath;
        }
        
        private Control getControl(Control control)
        {
            if (control.InvokeRequired)
            {
                Control controlInvoke = (Control)this.Invoke((getControlCallback)delegate
                {
                    return getControl(control);
                });
                return controlInvoke;
            }
            else
            {
                return control;
            }
        }

        private void clearDirectory(string path)
        {
            string[] files = Directory.GetFiles(path);
            string[] directories = Directory.GetDirectories(path);

            foreach (string file in files)
            {
                File.Delete(file);
            }

            foreach (string directory in directories)
            {
                Directory.Delete(directory, true);
            }
        }

        private void copyDirectory(string pathSource, string pathDestination, bool recursive, params string[] filter)
        {
            DirectoryInfo dirSource = new DirectoryInfo(pathSource);

            if (!dirSource.Exists)
            {
                throw new DirectoryNotFoundException("Source directory does not exist or could not be found: " + pathSource);
            }

            if (!Directory.Exists(pathDestination))
            {
                Directory.CreateDirectory(pathDestination);
            }

            FileInfo[] files = dirSource.GetFiles();

            foreach (FileInfo file in files)
            {
                string fileName = file.Name;

                if (!filter.Contains(fileName.ToLower()))
                {
                    string destFileName = Path.Combine(pathDestination, fileName);

                    file.CopyTo(destFileName, true);
                }
            }

            if (recursive)
            {
                DirectoryInfo[] directories = dirSource.GetDirectories();

                foreach (DirectoryInfo directory in directories)
                {
                    string directoryName = directory.Name;
                    if (!filter.Contains(directoryName))
                    {
                        pathDestination = Path.Combine(pathDestination, directoryName);
                        copyDirectory(directory.FullName, pathDestination, recursive, filter);
                    }
                }
            }
        }
        
        private bool downloadFile(string uriString, string pathFile, DownloadFinished callback = null)
        {
            if (!File.Exists(pathFile))
            {
                try
                {
                    webClient = new WebClient();

                    Uri address = uriString.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ? new Uri(uriString) : new Uri("http://" + uriString);
                    string file = pathFile.Replace(pathFile.Remove(pathFile.LastIndexOf("\\") + 1), "");

                    sw.Start();
                    webClient.DownloadFileAsync(address, pathFile);

                    webClient.DownloadFileCompleted += (AsyncCompletedEventHandler)((sender, e) =>
                    {
                        this.webClient_DownloadFileCompleted(sender, e, pathFile);
                        if (callback != null && !e.Cancelled)
                        {
                            callback(pathFile);
                        }
                    });
                    webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler((sender, e) => webClient_DownloadProgressChanged(sender, e, file));
                }
                catch (Exception e)
                {
                    throw e;
                }
                
                return true;
            }
            else
            {
                if (callback != null)
                {
                    callback(pathFile);
                } 
            }
            
            return false;
        }
        
        private void executeFile(string pathFile)
        {
            if (!string.IsNullOrEmpty(pathFile))
            {
                Process process = new Process();
                ProcessStartInfo processStartInfo = process.StartInfo;

                process.EnableRaisingEvents = true;
                processStartInfo.ErrorDialog = true;
                processStartInfo.UseShellExecute = true;
                processStartInfo.WindowStyle = ProcessWindowStyle.Normal;
                processStartInfo.FileName = pathFile;

                try
                {
                    process.Start();
                    
                    /*process.Exited += (sender, e) =>
                        {
                            process = null;
                            handlerOperation();
                        };*/
                }
                catch (Exception e)
                {
                    process.Dispose();
                    throw e;
                }
            }
        }

        private void killProcess(Process process = null, string message = "")
        {
            if (!string.IsNullOrEmpty(message))
            {
                MessageBox.Show(message);
            }

            if (process != null)
            {
                process.Kill();
            }
            else
            {
                Process.GetCurrentProcess().Kill();
            }
        }
        
        private void stopOperations(string message = "")
        {
            shouldShowDownloadComplted = true;
            Label lblConsole = (Label)getControl(this.lblConsole);
            Button btnBeginOperation = (Button)getControl(this.btnBeginOperation);
            Button fileOpenButton = (Button)getControl(this.fileDialogOpen);
            CheckBox cbClearDownloaded = (CheckBox)getControl(this.cbClearDownloaded);
            CheckBox cbClearUncompressed = (CheckBox)getControl(this.cbClearUncompressed);
            ProgressBar prgWeb = (ProgressBar)getControl(this.prgWeb);
            ProgressBar xDeltaBar = (ProgressBar)getControl(this.xDeltaBar);
            xDeltaBar.Visible = false;
            fileOpenButton.Enabled = true;

            cLauncher.Index = 0;
            cLauncher.cMod.Index = -1;
            cOperation.Type = 0;

            lblConsole.Text = "Stopping operations...";
            btnBeginOperation.Text = "Begin Operation";

            if (!string.IsNullOrEmpty(message))
            {
                MessageBox.Show(message);
            }

            if (webClient != null && webClient.IsBusy)
            {
                webClient.CancelAsync();
            }

            if (cbClearDownloaded.Checked)
            {
                clearDirectory(cPath.Downloaded);
            }

            if (cbClearUncompressed.Checked)
            {
                clearDirectory(cPath.Uncompressed);
            }

            prgWeb.Value = 0;
            lblConsole.Text = "";
        }

        private void unzipFile(string extractFrom, string extractTo)
        {
            try
            {
                ZipArchive zip = ZipFile.OpenRead(extractFrom);

                if (!string.IsNullOrEmpty(extractTo))
                {
                    if (!Directory.Exists(extractTo))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(extractTo));
                    }
                }

                foreach (ZipArchiveEntry entry in zip.Entries)
                {
                    string pathEntry = Path.Combine(extractTo, entry.FullName);

                    if (entry.FullName.EndsWith("/"))
                    {
                        string dirEntry = Path.GetDirectoryName(pathEntry);
                        Directory.CreateDirectory(dirEntry);
                    }
                    else
                    {
                        entry.ExtractToFile(pathEntry, true);
                    }
                }
            }
            catch (Exception e)
            {
                if (cbFailedExtraction.Checked)
                {
                    if (e is InvalidDataException)
                    {
                        File.Delete(extractFrom);
                    }
                }
                
                throw e;
            }
        }
        
        private void handlerOperation()
        {
            switch (cOperation.Type)
            {
                case 1:
                    handlerDownload();
                    break;
                case 2:
                    handlerUpdate();
                    break;
                case 3:
                    deleteXDelta();
                    break;
                case 4:
                    downloadXDelta();
                    break;
                default:
                    stopOperations();
                    return;
            }
        }
        
        private void handlerDownload()
        {
            try
            {
                string fileName = "";
                string dialog = "";
                string link = "";
                string type = "";
                if (cLauncher.cMod.Index < 0)
                {
                    if (cbLauncher.Checked)
                    {
                        fileName = (string)cLauncher.FileName;
                        dialog = cPath.Dialog;
                        link = (string)cLauncher.Link;
                    }
                }
                else
                {
                    if (cbMod.Checked)
                    {
                        fileName = (string)cLauncher.cMod.FileName;
                        dialog = Path.Combine(cPath.Dialog, (string)cLauncher.cMod.Path);
                        link = (string)cLauncher.cMod.Link;
                        type = cLauncher.cMod.Type;
                    }
                }

                if (!string.IsNullOrEmpty(fileName))
                {
                    Label lblConsole = (Label)getControl(this.lblConsole);
                    string downloadedFileName = Path.Combine(cPath.Downloaded, fileName);
                    if (type == "XDelta")
                    {
                        if (!this.downloadFile(link, downloadedFileName))
                        {
                            lblConsole.Text = fileName + " - Uncompressing";
                            lblConsole.Refresh();
                            this.handleXDelta(downloadedFileName, false);
                            cOperation.Type = 3;
                            handlerOperation();
                            return;
                        }
                        else
                        {
                            lblConsole.Text = fileName + " - Downloading";
                            lblConsole.Refresh();
                            return;
                        }
                    }
                    else
                    {
                        if (!downloadFile(link, downloadedFileName))
                        {
                            lblConsole.Text = fileName + " - Uncompressing";

                            lblConsole.Refresh();
                            unzipFile(downloadedFileName, dialog);
                        }
                        else
                        {
                            lblConsole.Text = fileName + " - Downloading";

                            lblConsole.Refresh();
                            return;
                        }
                    }
                    
                }
                
                for (int i = cLauncher.cMod.Index + 1; i <= cLauncher.cMod.Mod.Count; ++i)
                {
                    if (cLauncher.cMod.Mod.Count != i)
                    {
                        if (cblMod.CheckedIndices.Contains(i))
                        {
                            cLauncher.cMod.Index = i;
                            if ((string)cLauncher.cMod.Type == "XDelta")
                            {
                                string str = Path.Combine(cPath.Downloaded, (string)cLauncher.cMod.FileName);
                                File.Delete(str);
                            }
                            
                            handlerDownload();
                            break;
                        }
                    }
                    else
                    {
                        CheckBox cbLauncher = (CheckBox)getControl(this.cbLauncher);
                        if (cbLauncher.Checked)
                        {
                            string executable = Path.Combine(cPath.Dialog, (string)cLauncher.Executable);

                            lblConsole.Text = (string)cLauncher.Name + " - Loading";

                            lblConsole.Refresh();
                            executeFile(executable);
                        }
                        
                        stopOperations();
                        return;
                    }
                }
            }
            catch (Exception e)
            {
                stopOperations(e.ToString());
            }
        }
        
        private void handlerUpdate()
        {
            try
            {
                string fileName = "";
                string dialog = "";
                string link = "";
                string uncompressed = "";
                string type = "";
                List<string> filter = null;
                if (cLauncher.cMod.Index < 0)
                {
                    if (cbLauncher.Checked)
                    {
                        fileName = (string)cLauncher.FileName;
                        dialog = cPath.Dialog;
                        link = (string)cLauncher.Link;
                        uncompressed = Path.Combine(cPath.Uncompressed);
                        filter = cLauncher.cUpdate.Filter.ToObject<List<string>>();
                    }
                }
                else
                {
                    if (cbMod.Checked)
                    {
                        fileName = (string)cLauncher.cMod.FileName;
                        dialog = Path.Combine(cPath.Dialog, (string)cLauncher.cMod.Path);
                        link = (string)cLauncher.cMod.Link;
                        uncompressed = Path.Combine(cPath.Uncompressed, (string)cLauncher.cMod.Path);
                        filter = cLauncher.cMod.cUpdate.Filter.ToObject<List<string>>();
                        type = cLauncher.cMod.Type;
                    }
                }

                if (!string.IsNullOrEmpty(fileName))
                {
                    Label lblConsole = (Label)getControl(this.lblConsole);
                    string downloadedFile = Path.Combine(cPath.Downloaded, fileName);

                    if (type == "XDelta")
                    {
                        if (!this.downloadFile(link, downloadedFile))
                        {
                            lblConsole.Text = fileName + " - Uncompressing";
                            lblConsole.Refresh();
                            this.handleXDelta(downloadedFile, true);
                            cOperation.Type = 3;
                            handlerOperation();
                            return;
                        }
                        else
                        {
                            lblConsole.Text = fileName + " - Downloading";
                            lblConsole.Refresh();
                            return;
                        }
                    }
                    else
                    {
                        if (!downloadFile(link, downloadedFile))
                        {
                            lblConsole.Text = fileName + " - Uncompressing";

                            lblConsole.Refresh();
                            unzipFile(downloadedFile, dialog);
                        }
                        else
                        {
                            lblConsole.Text = fileName + " - Downloading";

                            lblConsole.Refresh();
                            return;
                        }
                    }
                }
                
                for (int i = cLauncher.cMod.Index + 1; i <= cLauncher.cMod.Mod.Count; ++i)
                {
                    if (cLauncher.cMod.Mod.Count != i)
                    {
                        if (cblMod.CheckedIndices.Contains(i))
                        {
                            cLauncher.cMod.Index = i;
                            if ((string)cLauncher.cMod.Type == "XDelta")
                            {
                                string str = Path.Combine(cPath.Downloaded, (string)cLauncher.cMod.FileName);
                                File.Delete(str);
                            }
                            handlerUpdate();
                            return;
                        }
                    }
                    else
                    {
                        if (cLauncher.cMod.Index != -1)
                        {
                            dialog = Directory.GetParent(dialog).Parent.FullName;
                            lblConsole.Text = "Copying updates";
                            
                            lblConsole.Refresh();
                            copyDirectory(uncompressed, dialog, true, filter.ToArray());
                        }
                        
                        stopOperations();
                        return;
                    }
                }
                
            }
            catch (Exception e)
            {
                stopOperations(e.ToString());
            }
        }

        static System.Security.Cryptography.MD5 hasher = System.Security.Cryptography.MD5.Create();
        private void handleXDelta(string manifestPath, bool update)
        {
            shouldShowDownloadComplted = false;
            xDeltaBar.Visible = true;
            xhashDownloadList = new ArrayList();
            xhashToDelete = new ArrayList();
            using (FileStream fs = File.Open(manifestPath, FileMode.Open))
            using (StreamReader sw = new StreamReader(fs))
            {
                JObject obj = JObject.Parse(sw.ReadToEnd());
                JObject oldManifest = new JObject();
                string str1 = Path.Combine(cPath.Dialog, (string)cLauncher.cMod.Path, "manifest.json");
                if (File.Exists(str1))
                {
                    using (FileStream fs2 = File.Open(str1, FileMode.Open, FileAccess.Read, FileShare.Read))
                    using (StreamReader sw2 = new StreamReader(fs2))
                    {
                        oldManifest = JObject.Parse(sw2.ReadToEnd());
                    }
                }
                Dictionary<string, JObject> parsed = new Dictionary<string, JObject>();
                Dictionary<string, JObject> parsed2 = new Dictionary<string, JObject>();
                if (obj["files"] != null)
                {
                    foreach (var t in obj["files"])
                    {
                        JObject fileProc = (JObject)t;
                        parsed.Add((string)fileProc["path"], fileProc);
                    }
                }
                if (oldManifest["files"] != null)
                {
                    foreach (var t in oldManifest["files"])
                    {
                        JObject fileProc = (JObject)t;
                        parsed2.Add((string)fileProc["path"], fileProc);
                    }
                }
                foreach (var t in parsed2)
                {
                    if (!parsed.ContainsKey(t.Key))
                    {
                        xhashToDelete.Add(t.Value);
                    }
                }
                foreach (var t in parsed)
                {
                    bool sensitive = false;
                    foreach (var t2 in obj["doNotUpdate"])
                    {
                        if ((string)t2 == t.Key)
                        {
                            sensitive = true;
                        }
                    }
                    if (!update || !sensitive)
                    {
                        bool good = !update;
                        if (update)
                        {
                            if (parsed2.ContainsKey(t.Key))
                            {
                                JObject output = parsed2[t.Key];
                                if ((string)output["hash"] != (string)t.Value["hash"])
                                {
                                    good = true;
                                }
                            }
                            else
                            {
                                good = true;
                            }
                            string filePath = Path.Combine(cPath.Dialog, (string)cLauncher.cMod.Path, (string)t.Value["path"]);
                            if (!File.Exists(filePath))
                            {
                                good = true;
                            }
                            else
                            {
                                using (FileStream hashFileStream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                                {
                                    byte[] hash = hasher.ComputeHash(hashFileStream);
                                    string hashStr = "";
                                    for (int I = 0; I < hash.Length; I++)
                                    {
                                        hashStr += hash[I].ToString();
                                    }
                                    if ((string)t.Value["hash"] != hashStr)
                                    {
                                        good = true;
                                    }
                                }
                            }
                        }

                        if (good)
                        {
                            xhashDownloadList.Add(t.Value);
                        }

                    }
                }
                xhashCount = xhashDownloadList.Count;
                
            }
        }
        private void deleteXDelta()
        {
            foreach (JObject file in xhashToDelete)
            {
                string str1 = Path.Combine(cPath.Dialog, (string)cLauncher.cMod.Path, (string)file["path"]);
                File.Delete(str1);
            }
            cOperation.Type = 4;
            handlerOperation();
        }
        private void downloadXDelta()
        {
            if (xhashDownloadList.Count == 0)
            {
                string downloadedFileName = Path.Combine(cPath.Downloaded, (string)cLauncher.cMod.FileName);
                string str1 = Path.Combine(cPath.Dialog, (string)cLauncher.cMod.Path, "manifest.json");
                File.Copy(downloadedFileName, str1, true);
                stopOperations("Pack Updated!");
                return;
            }
            float waffle = ((xhashCount - xhashDownloadList.Count) / (float)xhashCount);
            xDeltaBar.Value = (int)(waffle * 100);
            JObject obj = (JObject)xhashDownloadList[0];
            xhashDownloadList.RemoveAt(0);
            string outputStr = Path.Combine(cPath.Dialog, (string)cLauncher.cMod.Path, (string)obj["path"]);
            if (File.Exists(outputStr + ".bz2"))
            {
                File.Delete(outputStr + ".bz2");
            }
            string[] c = outputStr.Split('/');
            string outputDir2 = outputStr.Substring(0, outputStr.Length - c[c.Length - 1].Length);
            Directory.CreateDirectory(outputDir2);
            string url = (string)obj["url"];
            //Label label = (Label)this.getControl((Control)this.lblConsole);
            //label.Text = (string)obj["path"] + " - Downloading";
            //label.Refresh();
            bool test = this.downloadFile(url, outputStr + ".bz2", (fileoutput) =>
            {
                if (File.Exists(outputStr + ".bz2"))
                {

                    try
                    {
                        using (Stream reader = new StreamReader(outputStr + ".bz2").BaseStream)
                        using (Stream writer = new StreamWriter(outputStr).BaseStream)
                            ICSharpCode.SharpZipLib.BZip2.BZip2.Decompress(reader, writer, true);
                        File.Delete(outputStr + ".bz2");
                    }
                    catch (Exception e)
                    {
                        File.Delete(outputStr + ".bz2");
                    }

                }
            });



        }
        void webClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e, string file)
        {
            //throw new NotImplementedException();
            Label lblConsole = (Label)getControl(this.lblConsole);
            Label lblProgress = (Label)getControl(this.fileProgresText);
            ProgressBar prgWeb = (ProgressBar)getControl(this.prgWeb);

            lblConsole.Text = file;
            lblProgress.Text = string.Format("{0} MB / {1} MB", (e.BytesReceived / 1024d / 1024d).ToString("0.00"), (e.TotalBytesToReceive / 1024d / 1024d).ToString("0.00")) +
                string.Format(" - {0} MB/s", (e.BytesReceived / 1024d / 1024d / sw.Elapsed.TotalSeconds).ToString("0.00"));
            prgWeb.Value = e.ProgressPercentage;
        }
        void webClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e, string path)
        {
            //throw new NotImplementedException();
            sw.Reset();

            if (e.Cancelled)
            {
                if (cbFailedDownload.Checked)
                {
                    File.Delete(path);
                }
                
                stopOperations("Download has been cancelled");
            }
            else
            {
                Label lblConsole = (Label)getControl(this.lblConsole);
                if (shouldShowDownloadComplted)
                {
                    lblConsole.Text = "File downloaded successfully";
                }
                

                handlerOperation();
            }
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show(cMessage.About, "About");
        }

        private void btnInstructions_Click(object sender, EventArgs e)
        {
            MessageBox.Show(cMessage.Instructions, "Instructions");
        }
        
        private void btnCheck_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("No updates available... or are they?", "Fake message");
            Process.Start("http://www.gcinema.net/forums/index.php?/topic/1235-gcinemacraft-automatic-downloader-and-updater/");
        }

        private void btnBeginOperation_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(fileDialogText.Text) || string.IsNullOrEmpty(cPath.Dialog)) return;
            Label lblConsole = (Label)getControl(this.lblConsole);
            Button btnBeginOperation = (Button)getControl(this.btnBeginOperation);
            ListBox lbLauncher = (ListBox)getControl(this.lbLauncher);
            Button fileDialogOpen = (Button)getControl(this.fileDialogOpen);

            if (btnBeginOperation.Text.Equals("Begin Operation", StringComparison.OrdinalIgnoreCase))
            {
                fileDialogOpen.Enabled = false;
                string dialogDescription = "";

                lblConsole.Text = "Initializing handler...";
                
                if (rbDownload.Checked)
                {
                    cOperation.Type = 1;
                    dialogDescription = cMessage.cDialog.Download;
                }

                if (rbUpdate.Checked)
                {
                    cOperation.Type = 2;
                    dialogDescription = cMessage.cDialog.Update;
                }

                if (!string.IsNullOrEmpty(dialogDescription))
                {
                    //cPath.Dialog = getDialogPath(dialogDescription);
                    cLauncher.Index = lbLauncher.SelectedIndex;
                    btnBeginOperation.Text = "Cancel";

                    handlerOperation();
                    return;
                }
            }
            fileDialogOpen.Enabled = true;
            stopOperations();
        }
        
        private void cbMod_CheckedChanged(object sender, EventArgs e)
        {
            CheckedListBox cblMod = (CheckedListBox)getControl(this.cblMod);
            CheckBox cbMod = (CheckBox)getControl(this.cbMod);
            
            cblMod.ItemCheck -= cblMod_ItemCheck;
            
            for (int i = 0; i < cblMod.Items.Count; i++)
            {
                cblMod.SetItemChecked(i, cbMod.Checked);
            }
            
            cblMod.ItemCheck += cblMod_ItemCheck;
        }

        private void lbLauncher_SelectedValueChanged(object sender, EventArgs e)
        {
            ListBox lbLauncher = (ListBox)getControl(this.lbLauncher);
            CheckBox cbMod = (CheckBox)getControl(this.cbMod);
            CheckedListBox cblMod = (CheckedListBox)getControl(this.cblMod);
            Button btnBeginOperation = (Button)getControl(this.btnBeginOperation);
            int lbLauncher_SelectedIndex = lbLauncher.SelectedIndex;

            if (lbLauncher_SelectedIndex != -1)
            {
                cbMod.Enabled = true;
                btnBeginOperation.Enabled = true;
                cLauncher.Index = lbLauncher_SelectedIndex;

                cblMod.Items.Clear();

                for (int i = 0; i < cLauncher.cMod.Mod.Count; i++)
                {
                    cLauncher.cMod.Index = i;
                    cblMod.Items.Add(cLauncher.cMod.Name);
                }

                cLauncher.cMod.Index = -1;
            }
            else
            {
                cbMod.Enabled = false;
            }
        }

        private void cblMod_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            CheckedListBox cblMod = (CheckedListBox)getControl(this.cblMod);
            CheckBox cbMod = (CheckBox)getControl(this.cbMod);
            int cblMod_Items_Count = cblMod.Items.Count;
            int cblMod_CheckedItems_Count = cblMod.CheckedItems.Count;

            cblMod_CheckedItems_Count = (e.NewValue != CheckState.Checked ? cblMod_CheckedItems_Count - 1 : cblMod_CheckedItems_Count + 1);
            cbMod.CheckedChanged -= cbMod_CheckedChanged;

            if (cblMod_Items_Count != cblMod_CheckedItems_Count)
            {
                if (cblMod_CheckedItems_Count != 0)
                {
                    cbMod.CheckState = CheckState.Indeterminate;
                }
                else
                {
                    cbMod.CheckState = CheckState.Unchecked;
                }
            }
            else
            {
                cbMod.CheckState = CheckState.Checked;
            }

            cbMod.CheckedChanged += cbMod_CheckedChanged;
            cPath.Dialog = "";
            fileDialogText.Text = cPath.Dialog;
            for (int index = cLauncher.cMod.Index + 1; index <= cLauncher.cMod.Mod.Count; ++index)
            {
                if (e.Index == index)
                {
                    if (e.CurrentValue != CheckState.Checked)
                    {
                        fileDialogOpen.Enabled = true;
                        selected = cblMod_CheckedItems_Count;
                        cLauncher.cMod.Index = index;
                        cPath.Dialog = cLauncher.cMod.InstallPath;
                        fileDialogText.Text = cPath.Dialog;
                        cLauncher.cMod.Index = -1;
                        break;
                    }
                    else
                    {
                        selected = -1;
                        fileDialogOpen.Enabled = false;
                        cPath.Dialog = "";
                        fileDialogText.Text = cPath.Dialog;
                    }

                }
            }
        }

        private void fileDialogOpen_Click(object sender, EventArgs e)
        {
            if (selected != -1)
            {
                cLauncher.cMod.Index = selected;
                string newPath = this.getDialogPath("Select Install Location");
                if(!string.IsNullOrWhiteSpace(newPath))
                {
                    cPath.Dialog = newPath;
                    fileDialogText.Text = cPath.Dialog;
                    cLauncher.cMod.InstallPath = cPath.Dialog;
                    cLauncher.cMod.Index = -1;
                }
            }
        }
    }
}
