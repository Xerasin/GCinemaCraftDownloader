namespace GCinemaCraft
{
    partial class formGCinemaCraft
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formGCinemaCraft));
            this.prgWeb = new System.Windows.Forms.ProgressBar();
            this.btnAbout = new System.Windows.Forms.Button();
            this.lblConsole = new System.Windows.Forms.Label();
            this.btnInstructions = new System.Windows.Forms.Button();
            this.btnCheck = new System.Windows.Forms.Button();
            this.lblMessage = new System.Windows.Forms.Label();
            this.cbClearDownloaded = new System.Windows.Forms.CheckBox();
            this.grpInfo = new System.Windows.Forms.GroupBox();
            this.grpOptions = new System.Windows.Forms.GroupBox();
            this.cbFailedExtraction = new System.Windows.Forms.CheckBox();
            this.cbFailedDownload = new System.Windows.Forms.CheckBox();
            this.cbClearUncompressed = new System.Windows.Forms.CheckBox();
            this.grpOperations = new System.Windows.Forms.GroupBox();
            this.fileDialogOpen = new System.Windows.Forms.Button();
            this.fileDialogText = new System.Windows.Forms.TextBox();
            this.btnBeginOperation = new System.Windows.Forms.Button();
            this.cbMod = new System.Windows.Forms.CheckBox();
            this.cbLauncher = new System.Windows.Forms.CheckBox();
            this.rbDownload = new System.Windows.Forms.RadioButton();
            this.rbUpdate = new System.Windows.Forms.RadioButton();
            this.lbLauncher = new System.Windows.Forms.ListBox();
            this.cblMod = new System.Windows.Forms.CheckedListBox();
            this.grpItems = new System.Windows.Forms.GroupBox();
            this.xDeltaBar = new System.Windows.Forms.ProgressBar();
            this.fileProgresText = new System.Windows.Forms.Label();
            this.grpInfo.SuspendLayout();
            this.grpOptions.SuspendLayout();
            this.grpOperations.SuspendLayout();
            this.grpItems.SuspendLayout();
            this.SuspendLayout();
            // 
            // prgWeb
            // 
            this.prgWeb.Location = new System.Drawing.Point(6, 425);
            this.prgWeb.Name = "prgWeb";
            this.prgWeb.Size = new System.Drawing.Size(460, 23);
            this.prgWeb.TabIndex = 1;
            // 
            // btnAbout
            // 
            this.btnAbout.Location = new System.Drawing.Point(6, 19);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(75, 23);
            this.btnAbout.TabIndex = 2;
            this.btnAbout.Text = "About";
            this.btnAbout.UseVisualStyleBackColor = true;
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // lblConsole
            // 
            this.lblConsole.BackColor = System.Drawing.SystemColors.ControlLight;
            this.lblConsole.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblConsole.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConsole.Location = new System.Drawing.Point(6, 480);
            this.lblConsole.Name = "lblConsole";
            this.lblConsole.Size = new System.Drawing.Size(460, 23);
            this.lblConsole.TabIndex = 3;
            this.lblConsole.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnInstructions
            // 
            this.btnInstructions.Location = new System.Drawing.Point(87, 19);
            this.btnInstructions.Name = "btnInstructions";
            this.btnInstructions.Size = new System.Drawing.Size(75, 23);
            this.btnInstructions.TabIndex = 4;
            this.btnInstructions.Text = "Instructions";
            this.btnInstructions.UseVisualStyleBackColor = true;
            this.btnInstructions.Click += new System.EventHandler(this.btnInstructions_Click);
            // 
            // btnCheck
            // 
            this.btnCheck.Location = new System.Drawing.Point(6, 48);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(156, 23);
            this.btnCheck.TabIndex = 6;
            this.btnCheck.Text = "Check For Updates";
            this.btnCheck.UseVisualStyleBackColor = true;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // lblMessage
            // 
            this.lblMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMessage.Location = new System.Drawing.Point(6, 337);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(460, 85);
            this.lblMessage.TabIndex = 8;
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbClearDownloaded
            // 
            this.cbClearDownloaded.Location = new System.Drawing.Point(6, 19);
            this.cbClearDownloaded.Name = "cbClearDownloaded";
            this.cbClearDownloaded.Size = new System.Drawing.Size(145, 43);
            this.cbClearDownloaded.TabIndex = 9;
            this.cbClearDownloaded.Text = "Clear \'Downloaded\' folder after ending operation";
            this.cbClearDownloaded.UseVisualStyleBackColor = true;
            // 
            // grpInfo
            // 
            this.grpInfo.Controls.Add(this.btnAbout);
            this.grpInfo.Controls.Add(this.btnInstructions);
            this.grpInfo.Controls.Add(this.btnCheck);
            this.grpInfo.Location = new System.Drawing.Point(12, 12);
            this.grpInfo.Name = "grpInfo";
            this.grpInfo.Size = new System.Drawing.Size(168, 77);
            this.grpInfo.TabIndex = 10;
            this.grpInfo.TabStop = false;
            this.grpInfo.Text = "Info";
            // 
            // grpOptions
            // 
            this.grpOptions.Controls.Add(this.cbFailedExtraction);
            this.grpOptions.Controls.Add(this.cbFailedDownload);
            this.grpOptions.Controls.Add(this.cbClearUncompressed);
            this.grpOptions.Controls.Add(this.cbClearDownloaded);
            this.grpOptions.Location = new System.Drawing.Point(6, 217);
            this.grpOptions.Name = "grpOptions";
            this.grpOptions.Size = new System.Drawing.Size(460, 117);
            this.grpOptions.TabIndex = 11;
            this.grpOptions.TabStop = false;
            this.grpOptions.Text = "Options";
            // 
            // cbFailedExtraction
            // 
            this.cbFailedExtraction.Checked = true;
            this.cbFailedExtraction.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbFailedExtraction.Location = new System.Drawing.Point(309, 19);
            this.cbFailedExtraction.Name = "cbFailedExtraction";
            this.cbFailedExtraction.Size = new System.Drawing.Size(145, 43);
            this.cbFailedExtraction.TabIndex = 15;
            this.cbFailedExtraction.Text = "Delete file on failed extraction";
            this.cbFailedExtraction.UseVisualStyleBackColor = true;
            // 
            // cbFailedDownload
            // 
            this.cbFailedDownload.Checked = true;
            this.cbFailedDownload.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbFailedDownload.Location = new System.Drawing.Point(6, 68);
            this.cbFailedDownload.Name = "cbFailedDownload";
            this.cbFailedDownload.Size = new System.Drawing.Size(145, 43);
            this.cbFailedDownload.TabIndex = 14;
            this.cbFailedDownload.Text = "Delete file on failed download";
            this.cbFailedDownload.UseVisualStyleBackColor = true;
            // 
            // cbClearUncompressed
            // 
            this.cbClearUncompressed.Checked = true;
            this.cbClearUncompressed.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbClearUncompressed.Location = new System.Drawing.Point(157, 19);
            this.cbClearUncompressed.Name = "cbClearUncompressed";
            this.cbClearUncompressed.Size = new System.Drawing.Size(146, 43);
            this.cbClearUncompressed.TabIndex = 10;
            this.cbClearUncompressed.Text = "Clear \'Uncompressed\' folder after ending operation";
            this.cbClearUncompressed.UseVisualStyleBackColor = true;
            // 
            // grpOperations
            // 
            this.grpOperations.Controls.Add(this.fileDialogOpen);
            this.grpOperations.Controls.Add(this.fileDialogText);
            this.grpOperations.Controls.Add(this.btnBeginOperation);
            this.grpOperations.Controls.Add(this.cbMod);
            this.grpOperations.Controls.Add(this.cbLauncher);
            this.grpOperations.Controls.Add(this.rbDownload);
            this.grpOperations.Controls.Add(this.rbUpdate);
            this.grpOperations.Location = new System.Drawing.Point(12, 95);
            this.grpOperations.Name = "grpOperations";
            this.grpOperations.Size = new System.Drawing.Size(168, 116);
            this.grpOperations.TabIndex = 12;
            this.grpOperations.TabStop = false;
            this.grpOperations.Text = "Operations";
            // 
            // fileDialogOpen
            // 
            this.fileDialogOpen.Enabled = false;
            this.fileDialogOpen.Location = new System.Drawing.Point(132, 64);
            this.fileDialogOpen.Name = "fileDialogOpen";
            this.fileDialogOpen.Size = new System.Drawing.Size(30, 21);
            this.fileDialogOpen.TabIndex = 17;
            this.fileDialogOpen.Text = "...";
            this.fileDialogOpen.UseVisualStyleBackColor = true;
            this.fileDialogOpen.Click += new System.EventHandler(this.fileDialogOpen_Click);
            // 
            // fileDialogText
            // 
            this.fileDialogText.Location = new System.Drawing.Point(6, 65);
            this.fileDialogText.Name = "fileDialogText";
            this.fileDialogText.ReadOnly = true;
            this.fileDialogText.Size = new System.Drawing.Size(126, 20);
            this.fileDialogText.TabIndex = 15;
            // 
            // btnBeginOperation
            // 
            this.btnBeginOperation.Enabled = false;
            this.btnBeginOperation.Location = new System.Drawing.Point(6, 87);
            this.btnBeginOperation.Name = "btnBeginOperation";
            this.btnBeginOperation.Size = new System.Drawing.Size(156, 23);
            this.btnBeginOperation.TabIndex = 14;
            this.btnBeginOperation.Text = "Begin Operation";
            this.btnBeginOperation.UseVisualStyleBackColor = true;
            this.btnBeginOperation.Click += new System.EventHandler(this.btnBeginOperation_Click);
            // 
            // cbMod
            // 
            this.cbMod.AutoSize = true;
            this.cbMod.Enabled = false;
            this.cbMod.Location = new System.Drawing.Point(102, 42);
            this.cbMod.Name = "cbMod";
            this.cbMod.Size = new System.Drawing.Size(47, 17);
            this.cbMod.TabIndex = 16;
            this.cbMod.Text = "Mod";
            this.cbMod.UseVisualStyleBackColor = true;
            this.cbMod.CheckedChanged += new System.EventHandler(this.cbMod_CheckedChanged);
            // 
            // cbLauncher
            // 
            this.cbLauncher.AutoSize = true;
            this.cbLauncher.Location = new System.Drawing.Point(6, 42);
            this.cbLauncher.Name = "cbLauncher";
            this.cbLauncher.Size = new System.Drawing.Size(71, 17);
            this.cbLauncher.TabIndex = 14;
            this.cbLauncher.Text = "Launcher";
            this.cbLauncher.UseVisualStyleBackColor = true;
            // 
            // rbDownload
            // 
            this.rbDownload.AutoSize = true;
            this.rbDownload.Checked = true;
            this.rbDownload.Location = new System.Drawing.Point(6, 19);
            this.rbDownload.Name = "rbDownload";
            this.rbDownload.Size = new System.Drawing.Size(73, 17);
            this.rbDownload.TabIndex = 14;
            this.rbDownload.TabStop = true;
            this.rbDownload.Text = "Download";
            this.rbDownload.UseVisualStyleBackColor = true;
            // 
            // rbUpdate
            // 
            this.rbUpdate.AutoSize = true;
            this.rbUpdate.Location = new System.Drawing.Point(102, 19);
            this.rbUpdate.Name = "rbUpdate";
            this.rbUpdate.Size = new System.Drawing.Size(60, 17);
            this.rbUpdate.TabIndex = 15;
            this.rbUpdate.Text = "Update";
            this.rbUpdate.UseVisualStyleBackColor = true;
            // 
            // lbLauncher
            // 
            this.lbLauncher.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbLauncher.FormattingEnabled = true;
            this.lbLauncher.IntegralHeight = false;
            this.lbLauncher.Location = new System.Drawing.Point(6, 19);
            this.lbLauncher.Name = "lbLauncher";
            this.lbLauncher.Size = new System.Drawing.Size(134, 174);
            this.lbLauncher.TabIndex = 6;
            this.lbLauncher.SelectedValueChanged += new System.EventHandler(this.lbLauncher_SelectedValueChanged);
            // 
            // cblMod
            // 
            this.cblMod.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cblMod.CheckOnClick = true;
            this.cblMod.FormattingEnabled = true;
            this.cblMod.IntegralHeight = false;
            this.cblMod.Location = new System.Drawing.Point(146, 19);
            this.cblMod.Name = "cblMod";
            this.cblMod.Size = new System.Drawing.Size(134, 174);
            this.cblMod.TabIndex = 14;
            this.cblMod.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.cblMod_ItemCheck);
            // 
            // grpItems
            // 
            this.grpItems.Controls.Add(this.cblMod);
            this.grpItems.Controls.Add(this.lbLauncher);
            this.grpItems.Location = new System.Drawing.Point(186, 12);
            this.grpItems.Name = "grpItems";
            this.grpItems.Size = new System.Drawing.Size(286, 199);
            this.grpItems.TabIndex = 15;
            this.grpItems.TabStop = false;
            this.grpItems.Text = "Items";
            // 
            // xDeltaBar
            // 
            this.xDeltaBar.Location = new System.Drawing.Point(6, 454);
            this.xDeltaBar.Name = "xDeltaBar";
            this.xDeltaBar.Size = new System.Drawing.Size(460, 23);
            this.xDeltaBar.TabIndex = 16;
            this.xDeltaBar.Visible = false;
            // 
            // fileProgresText
            // 
            this.fileProgresText.BackColor = System.Drawing.SystemColors.ControlLight;
            this.fileProgresText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fileProgresText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileProgresText.Location = new System.Drawing.Point(6, 509);
            this.fileProgresText.Name = "fileProgresText";
            this.fileProgresText.Size = new System.Drawing.Size(460, 23);
            this.fileProgresText.TabIndex = 17;
            this.fileProgresText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // formGCinemaCraft
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 541);
            this.Controls.Add(this.fileProgresText);
            this.Controls.Add(this.xDeltaBar);
            this.Controls.Add(this.grpItems);
            this.Controls.Add(this.grpOperations);
            this.Controls.Add(this.grpOptions);
            this.Controls.Add(this.grpInfo);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.lblConsole);
            this.Controls.Add(this.prgWeb);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "formGCinemaCraft";
            this.Text = "GCinemaCraft";
            this.grpInfo.ResumeLayout(false);
            this.grpOptions.ResumeLayout(false);
            this.grpOperations.ResumeLayout(false);
            this.grpOperations.PerformLayout();
            this.grpItems.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar prgWeb;
        private System.Windows.Forms.Button btnAbout;
        private System.Windows.Forms.Label lblConsole;
        private System.Windows.Forms.Button btnInstructions;
        private System.Windows.Forms.Button btnCheck;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.CheckBox cbClearDownloaded;
        private System.Windows.Forms.GroupBox grpInfo;
        private System.Windows.Forms.GroupBox grpOptions;
        private System.Windows.Forms.GroupBox grpOperations;
        private System.Windows.Forms.CheckBox cbClearUncompressed;
        private System.Windows.Forms.CheckBox cbFailedDownload;
        private System.Windows.Forms.ListBox lbLauncher;
        private System.Windows.Forms.CheckBox cbMod;
        private System.Windows.Forms.CheckBox cbLauncher;
        private System.Windows.Forms.RadioButton rbDownload;
        private System.Windows.Forms.RadioButton rbUpdate;
        private System.Windows.Forms.Button btnBeginOperation;
        private System.Windows.Forms.CheckedListBox cblMod;
        private System.Windows.Forms.GroupBox grpItems;
        private System.Windows.Forms.CheckBox cbFailedExtraction;
        private System.Windows.Forms.Button fileDialogOpen;
        private System.Windows.Forms.TextBox fileDialogText;
        private System.Windows.Forms.ProgressBar xDeltaBar;
        private System.Windows.Forms.Label fileProgresText;
    }
}

