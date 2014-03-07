namespace PuTTYWordness
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.valueGrid = new System.Windows.Forms.DataGridView();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.mnuSave = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExport = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuImport = new System.Windows.Forms.ToolStripMenuItem();
            this.lstSessions = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.valueGrid)).BeginInit();
            this.mainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // valueGrid
            // 
            this.valueGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.valueGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.valueGrid.Location = new System.Drawing.Point(158, 25);
            this.valueGrid.Margin = new System.Windows.Forms.Padding(2);
            this.valueGrid.Name = "valueGrid";
            this.valueGrid.RowTemplate.Height = 24;
            this.valueGrid.Size = new System.Drawing.Size(665, 492);
            this.valueGrid.TabIndex = 0;
            this.valueGrid.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.valueGrid_CellValidating);
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSave,
            this.mnuExport,
            this.mnuImport});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.mainMenu.Size = new System.Drawing.Size(824, 24);
            this.mainMenu.TabIndex = 1;
            this.mainMenu.Text = "menuStrip1";
            // 
            // mnuSave
            // 
            this.mnuSave.Enabled = false;
            this.mnuSave.Name = "mnuSave";
            this.mnuSave.Size = new System.Drawing.Size(43, 20);
            this.mnuSave.Text = "Save";
            this.mnuSave.Click += new System.EventHandler(this.mnuSave_Click);
            // 
            // mnuExport
            // 
            this.mnuExport.Enabled = false;
            this.mnuExport.Name = "mnuExport";
            this.mnuExport.Size = new System.Drawing.Size(52, 20);
            this.mnuExport.Text = "Export";
            this.mnuExport.Click += new System.EventHandler(this.mnuExport_Click);
            // 
            // mnuImport
            // 
            this.mnuImport.Enabled = false;
            this.mnuImport.Name = "mnuImport";
            this.mnuImport.Size = new System.Drawing.Size(55, 20);
            this.mnuImport.Text = "Import";
            this.mnuImport.Click += new System.EventHandler(this.mnuImport_Click);
            // 
            // lstSessions
            // 
            this.lstSessions.Dock = System.Windows.Forms.DockStyle.Left;
            this.lstSessions.FormattingEnabled = true;
            this.lstSessions.Location = new System.Drawing.Point(0, 24);
            this.lstSessions.Margin = new System.Windows.Forms.Padding(2);
            this.lstSessions.Name = "lstSessions";
            this.lstSessions.Size = new System.Drawing.Size(155, 493);
            this.lstSessions.TabIndex = 2;
            this.lstSessions.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstSessions_MouseDoubleClick);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 517);
            this.Controls.Add(this.lstSessions);
            this.Controls.Add(this.valueGrid);
            this.Controls.Add(this.mainMenu);
            this.MainMenuStrip = this.mainMenu;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmMain";
            this.Text = "PuTTY Wordness Editor";
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.valueGrid)).EndInit();
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView valueGrid;
        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem mnuSave;
        private System.Windows.Forms.ListBox lstSessions;
        private System.Windows.Forms.ToolStripMenuItem mnuExport;
        private System.Windows.Forms.ToolStripMenuItem mnuImport;
    }
}

