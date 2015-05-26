using System;
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

namespace GCinemaCraft
{
    public partial class formGCinemaCraft : Form
    {
        Stopwatch sw;
        WebClient webClient;
        
        delegate Control getControlCallback(Control control);
        
        public formGCinemaCraft()
        {
            InitializeComponent();

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
        
        private bool downloadFile(string uriString, string pathFile)
        {
            if (!File.Exists(pathFile))
            {
                try
                {
                    webClient = new WebClient();

                    Uri address = uriString.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ? new Uri(uriString) : new Uri("http://" + uriString);
                    string file = pathFile.Replace(pathFile.Remove(pathFile.LastIndexOf("\\") + 1), "");

                    sw.Start();
                    sw.
                    webClient.DownloadFileAsync(address, pathFile);

                    webClient.DownloadFileCompleted += (sender, e) => webClient_DownloadFileCompleted(sender, e, pathFile);
                    webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler((sender, e) => webClient_DownloadProgressChanged(sender, e, file));
                }
                catch (Exception e)
                {
                    throw e;
                }
                
                return true;
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
            Label lblConsole = (Label)getControl(this.lblConsole);
            Button btnBeginOperation = (Button)getControl(this.btnBeginOperation);
            CheckBox cbClearDownloaded = (CheckBox)getControl(this.cbClearDownloaded);
            CheckBox cbClearUncompressed = (CheckBox)getControl(this.cbClearUncompressed);
            ProgressBar prgWeb = (ProgressBar)getControl(this.prgWeb);

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
                    }
                }

                if (!string.IsNullOrEmpty(fileName))
                {
                    Label lblConsole = (Label)getControl(this.lblConsole);
                    string downloadedFileName = Path.Combine(cPath.Downloaded, fileName);

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
                
                for (int i = cLauncher.cMod.Index + 1; i <= cLauncher.cMod.Mod.Count; ++i)
                {
                    if (cLauncher.cMod.Mod.Count != i)
                    {
                        if (cblMod.CheckedIndices.Contains(i))
                        {
                            cLauncher.cMod.Index = i;
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
                    }
                }

                if (!string.IsNullOrEmpty(fileName))
                {
                    Label lblConsole = (Label)getControl(this.lblConsole);
                    string downloadedFile = Path.Combine(cPath.Downloaded, fileName);

                    if (!downloadFile(link, downloadedFile))
                    {
                        lblConsole.Text = fileName + " - Uncompressing";
                        
                        lblConsole.Refresh();
                        unzipFile(downloadedFile, uncompressed);
                    }
                    else
                    {
                        lblConsole.Text = fileName + " - Downloading";
                        
                        lblConsole.Refresh();
                        return;
                    }
                }
                
                for (int i = cLauncher.cMod.Index + 1; i <= cLauncher.cMod.Mod.Count; ++i)
                {
                    if (cLauncher.cMod.Mod.Count != i)
                    {
                        if (cblMod.CheckedIndices.Contains(i))
                        {
                            cLauncher.cMod.Index = i;
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
        
        void webClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e, string file)
        {
            //throw new NotImplementedException();
            Label lblConsole = (Label)getControl(this.lblConsole);
            ProgressBar prgWeb = (ProgressBar)getControl(this.prgWeb);

            lblConsole.Text = file +
                string.Format(" - {0} MB / {1} MB", (e.BytesReceived / 1024d / 1024d).ToString("0.00"), (e.TotalBytesToReceive / 1024d / 1024d).ToString("0.00")) +
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
                lblConsole.Text = "File downloaded successfully";

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
            Label lblConsole = (Label)getControl(this.lblConsole);
            Button btnBeginOperation = (Button)getControl(this.btnBeginOperation);
            ListBox lbLauncher = (ListBox)getControl(this.lbLauncher);
            
            if (btnBeginOperation.Text.Equals("Begin Operation", StringComparison.OrdinalIgnoreCase))
            {
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
                    cPath.Dialog = getDialogPath(dialogDescription);
                    cLauncher.Index = lbLauncher.SelectedIndex;
                    btnBeginOperation.Text = "Cancel";

                    handlerOperation();
                    return;
                }
            }
            
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
        }
    }
}
