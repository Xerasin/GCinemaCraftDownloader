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
            this.lblConsole = new System.Windows.Forms.Label();
            this.btnInstructions = new System.Windows.Forms.Button();
            this.grpInfo = new System.Windows.Forms.GroupBox();
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
            this.progressBar = new GCinemaCraft.ProgressBar2();
            this.xDeltaBar = new GCinemaCraft.ProgressBar2();
            this.grpInfo.SuspendLayout();
            this.grpOperations.SuspendLayout();
            this.grpItems.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblConsole
            // 
            this.lblConsole.BackColor = System.Drawing.SystemColors.ControlLight;
            this.lblConsole.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblConsole.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConsole.Location = new System.Drawing.Point(12, 272);
            this.lblConsole.Name = "lblConsole";
            this.lblConsole.Size = new System.Drawing.Size(460, 23);
            this.lblConsole.TabIndex = 3;
            this.lblConsole.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblConsole.Visible = false;
            // 
            // btnInstructions
            // 
            this.btnInstructions.Location = new System.Drawing.Point(6, 19);
            this.btnInstructions.Name = "btnInstructions";
            this.btnInstructions.Size = new System.Drawing.Size(156, 52);
            this.btnInstructions.TabIndex = 4;
            this.btnInstructions.Text = "Instructions";
            this.btnInstructions.UseVisualStyleBackColor = true;
            this.btnInstructions.Click += new System.EventHandler(this.btnInstructions_Click);
            // 
            // grpInfo
            // 
            this.grpInfo.Controls.Add(this.btnInstructions);
            this.grpInfo.Location = new System.Drawing.Point(12, 12);
            this.grpInfo.Name = "grpInfo";
            this.grpInfo.Size = new System.Drawing.Size(168, 77);
            this.grpInfo.TabIndex = 10;
            this.grpInfo.TabStop = false;
            this.grpInfo.Text = "Info";
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
            // progressBar
            // 
            this.progressBar.CustomText = null;
            this.progressBar.DisplayStyle = GCinemaCraft.ProgressBarDisplayText.CustomText;
            this.progressBar.Location = new System.Drawing.Point(12, 217);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(460, 23);
            this.progressBar.TabIndex = 19;
            this.progressBar.Visible = false;
            // 
            // xDeltaBar
            // 
            this.xDeltaBar.CustomText = "";
            this.xDeltaBar.DisplayStyle = GCinemaCraft.ProgressBarDisplayText.CustomText;
            this.xDeltaBar.Location = new System.Drawing.Point(12, 246);
            this.xDeltaBar.Name = "xDeltaBar";
            this.xDeltaBar.Size = new System.Drawing.Size(460, 23);
            this.xDeltaBar.TabIndex = 18;
            this.xDeltaBar.Visible = false;
            // 
            // formGCinemaCraft
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 307);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.xDeltaBar);
            this.Controls.Add(this.grpItems);
            this.Controls.Add(this.grpOperations);
            this.Controls.Add(this.grpInfo);
            this.Controls.Add(this.lblConsole);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "formGCinemaCraft";
            this.Text = "GCinemaCraft";
            this.grpInfo.ResumeLayout(false);
            this.grpOperations.ResumeLayout(false);
            this.grpOperations.PerformLayout();
            this.grpItems.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lblConsole;
        private System.Windows.Forms.Button btnInstructions;
        private System.Windows.Forms.GroupBox grpInfo;
        private System.Windows.Forms.GroupBox grpOperations;
        private System.Windows.Forms.ListBox lbLauncher;
        private System.Windows.Forms.CheckBox cbMod;
        private System.Windows.Forms.CheckBox cbLauncher;
        private System.Windows.Forms.RadioButton rbDownload;
        private System.Windows.Forms.RadioButton rbUpdate;
        private System.Windows.Forms.Button btnBeginOperation;
        private System.Windows.Forms.CheckedListBox cblMod;
        private System.Windows.Forms.GroupBox grpItems;
        private System.Windows.Forms.Button fileDialogOpen;
        private System.Windows.Forms.TextBox fileDialogText;
        private ProgressBar2 xDeltaBar;
        private ProgressBar2 progressBar;
    }
}

