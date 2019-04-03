namespace Mst.Dexter.PocoGenerator
{
    partial class FrmCodeGenerator
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
            this.tblLytMain = new System.Windows.Forms.TableLayoutPanel();
            this.tblLytConnType = new System.Windows.Forms.TableLayoutPanel();
            this.lblConnectionType = new System.Windows.Forms.Label();
            this.cmbConnectionType = new System.Windows.Forms.ComboBox();
            this.btnGetTables = new System.Windows.Forms.Button();
            this.tblLytConnString = new System.Windows.Forms.TableLayoutPanel();
            this.lblConnectionString = new System.Windows.Forms.Label();
            this.txtConnectionString = new System.Windows.Forms.TextBox();
            this.btnSaveConnStr = new System.Windows.Forms.Button();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.tblLytBrowse = new System.Windows.Forms.TableLayoutPanel();
            this.txtSaveToPath = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.lblSaveToPath = new System.Windows.Forms.Label();
            this.tbctrlTablesAndLog = new System.Windows.Forms.TabControl();
            this.tbpgTables = new System.Windows.Forms.TabPage();
            this.chklstTables = new System.Windows.Forms.CheckedListBox();
            this.tbpgLog = new System.Windows.Forms.TabPage();
            this.txtLog = new System.Windows.Forms.RichTextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblNameSpace = new System.Windows.Forms.Label();
            this.txtNameSpace = new System.Windows.Forms.TextBox();
            this.chkSelectAll = new System.Windows.Forms.CheckBox();
            this.browserDestinationDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.tblLytMain.SuspendLayout();
            this.tblLytConnType.SuspendLayout();
            this.tblLytConnString.SuspendLayout();
            this.tblLytBrowse.SuspendLayout();
            this.tbctrlTablesAndLog.SuspendLayout();
            this.tbpgTables.SuspendLayout();
            this.tbpgLog.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblLytMain
            // 
            this.tblLytMain.ColumnCount = 1;
            this.tblLytMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLytMain.Controls.Add(this.tblLytConnType, 0, 0);
            this.tblLytMain.Controls.Add(this.tblLytConnString, 0, 1);
            this.tblLytMain.Controls.Add(this.btnGenerate, 0, 6);
            this.tblLytMain.Controls.Add(this.tblLytBrowse, 0, 5);
            this.tblLytMain.Controls.Add(this.tbctrlTablesAndLog, 0, 3);
            this.tblLytMain.Controls.Add(this.tableLayoutPanel1, 0, 2);
            this.tblLytMain.Controls.Add(this.chkSelectAll, 0, 4);
            this.tblLytMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLytMain.Location = new System.Drawing.Point(0, 0);
            this.tblLytMain.Name = "tblLytMain";
            this.tblLytMain.RowCount = 7;
            this.tblLytMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tblLytMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tblLytMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLytMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 440F));
            this.tblLytMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblLytMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tblLytMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tblLytMain.Size = new System.Drawing.Size(567, 687);
            this.tblLytMain.TabIndex = 0;
            // 
            // tblLytConnType
            // 
            this.tblLytConnType.ColumnCount = 3;
            this.tblLytConnType.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.50356F));
            this.tblLytConnType.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 71.49644F));
            this.tblLytConnType.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tblLytConnType.Controls.Add(this.lblConnectionType, 0, 0);
            this.tblLytConnType.Controls.Add(this.cmbConnectionType, 1, 0);
            this.tblLytConnType.Controls.Add(this.btnGetTables, 2, 0);
            this.tblLytConnType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLytConnType.Location = new System.Drawing.Point(3, 3);
            this.tblLytConnType.Name = "tblLytConnType";
            this.tblLytConnType.RowCount = 1;
            this.tblLytConnType.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblLytConnType.Size = new System.Drawing.Size(561, 39);
            this.tblLytConnType.TabIndex = 0;
            // 
            // lblConnectionType
            // 
            this.lblConnectionType.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblConnectionType.AutoSize = true;
            this.lblConnectionType.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblConnectionType.Location = new System.Drawing.Point(10, 11);
            this.lblConnectionType.Margin = new System.Windows.Forms.Padding(10, 0, 3, 0);
            this.lblConnectionType.Name = "lblConnectionType";
            this.lblConnectionType.Size = new System.Drawing.Size(108, 16);
            this.lblConnectionType.TabIndex = 0;
            this.lblConnectionType.Text = "Connection Type:";
            // 
            // cmbConnectionType
            // 
            this.cmbConnectionType.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cmbConnectionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbConnectionType.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.cmbConnectionType.Location = new System.Drawing.Point(146, 7);
            this.cmbConnectionType.Margin = new System.Windows.Forms.Padding(15, 3, 15, 3);
            this.cmbConnectionType.Name = "cmbConnectionType";
            this.cmbConnectionType.Size = new System.Drawing.Size(299, 24);
            this.cmbConnectionType.TabIndex = 1;
            this.cmbConnectionType.SelectedIndexChanged += new System.EventHandler(this.CmbConnectionTypeIndexChange);
            // 
            // btnGetTables
            // 
            this.btnGetTables.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnGetTables.Font = new System.Drawing.Font("Tahoma", 10F);
            this.btnGetTables.Location = new System.Drawing.Point(463, 3);
            this.btnGetTables.Name = "btnGetTables";
            this.btnGetTables.Size = new System.Drawing.Size(95, 33);
            this.btnGetTables.TabIndex = 2;
            this.btnGetTables.Text = "Get Tables";
            this.btnGetTables.UseVisualStyleBackColor = true;
            this.btnGetTables.Click += new System.EventHandler(this.GetTablesClick);
            // 
            // tblLytConnString
            // 
            this.tblLytConnString.ColumnCount = 3;
            this.tblLytConnString.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.69565F));
            this.tblLytConnString.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 71.30434F));
            this.tblLytConnString.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tblLytConnString.Controls.Add(this.lblConnectionString, 0, 0);
            this.tblLytConnString.Controls.Add(this.txtConnectionString, 1, 0);
            this.tblLytConnString.Controls.Add(this.btnSaveConnStr, 2, 0);
            this.tblLytConnString.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLytConnString.Location = new System.Drawing.Point(3, 48);
            this.tblLytConnString.Name = "tblLytConnString";
            this.tblLytConnString.RowCount = 1;
            this.tblLytConnString.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblLytConnString.Size = new System.Drawing.Size(561, 39);
            this.tblLytConnString.TabIndex = 4;
            // 
            // lblConnectionString
            // 
            this.lblConnectionString.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblConnectionString.AutoSize = true;
            this.lblConnectionString.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblConnectionString.Location = new System.Drawing.Point(10, 11);
            this.lblConnectionString.Margin = new System.Windows.Forms.Padding(10, 0, 3, 0);
            this.lblConnectionString.Name = "lblConnectionString";
            this.lblConnectionString.Size = new System.Drawing.Size(114, 16);
            this.lblConnectionString.TabIndex = 0;
            this.lblConnectionString.Text = "Connection String:";
            // 
            // txtConnectionString
            // 
            this.txtConnectionString.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtConnectionString.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtConnectionString.Location = new System.Drawing.Point(147, 8);
            this.txtConnectionString.Margin = new System.Windows.Forms.Padding(15, 3, 15, 3);
            this.txtConnectionString.Name = "txtConnectionString";
            this.txtConnectionString.Size = new System.Drawing.Size(298, 22);
            this.txtConnectionString.TabIndex = 1;
            // 
            // btnSaveConnStr
            // 
            this.btnSaveConnStr.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSaveConnStr.Font = new System.Drawing.Font("Tahoma", 10F);
            this.btnSaveConnStr.Location = new System.Drawing.Point(465, 3);
            this.btnSaveConnStr.Name = "btnSaveConnStr";
            this.btnSaveConnStr.Size = new System.Drawing.Size(90, 33);
            this.btnSaveConnStr.TabIndex = 2;
            this.btnSaveConnStr.Text = "Save";
            this.btnSaveConnStr.UseVisualStyleBackColor = true;
            this.btnSaveConnStr.Click += new System.EventHandler(this.SaveSettings);
            // 
            // btnGenerate
            // 
            this.btnGenerate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnGenerate.Font = new System.Drawing.Font("Tahoma", 10F);
            this.btnGenerate.Location = new System.Drawing.Point(225, 648);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(117, 32);
            this.btnGenerate.TabIndex = 3;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.GenerateClick);
            // 
            // tblLytBrowse
            // 
            this.tblLytBrowse.ColumnCount = 3;
            this.tblLytBrowse.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tblLytBrowse.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLytBrowse.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tblLytBrowse.Controls.Add(this.txtSaveToPath, 1, 0);
            this.tblLytBrowse.Controls.Add(this.btnBrowse, 2, 0);
            this.tblLytBrowse.Controls.Add(this.lblSaveToPath, 0, 0);
            this.tblLytBrowse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLytBrowse.Location = new System.Drawing.Point(3, 600);
            this.tblLytBrowse.Name = "tblLytBrowse";
            this.tblLytBrowse.RowCount = 1;
            this.tblLytBrowse.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLytBrowse.Size = new System.Drawing.Size(561, 39);
            this.tblLytBrowse.TabIndex = 5;
            // 
            // txtSaveToPath
            // 
            this.txtSaveToPath.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtSaveToPath.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.txtSaveToPath.Location = new System.Drawing.Point(103, 8);
            this.txtSaveToPath.Name = "txtSaveToPath";
            this.txtSaveToPath.ReadOnly = true;
            this.txtSaveToPath.Size = new System.Drawing.Size(355, 23);
            this.txtSaveToPath.TabIndex = 1;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnBrowse.Font = new System.Drawing.Font("Tahoma", 10F);
            this.btnBrowse.Location = new System.Drawing.Point(473, 3);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 33);
            this.btnBrowse.TabIndex = 0;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.Browse);
            // 
            // lblSaveToPath
            // 
            this.lblSaveToPath.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblSaveToPath.AutoSize = true;
            this.lblSaveToPath.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblSaveToPath.Location = new System.Drawing.Point(5, 11);
            this.lblSaveToPath.Name = "lblSaveToPath";
            this.lblSaveToPath.Size = new System.Drawing.Size(89, 16);
            this.lblSaveToPath.TabIndex = 2;
            this.lblSaveToPath.Text = "Save To Path:";
            // 
            // tbctrlTablesAndLog
            // 
            this.tbctrlTablesAndLog.Controls.Add(this.tbpgTables);
            this.tbctrlTablesAndLog.Controls.Add(this.tbpgLog);
            this.tbctrlTablesAndLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbctrlTablesAndLog.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.tbctrlTablesAndLog.Location = new System.Drawing.Point(3, 130);
            this.tbctrlTablesAndLog.Name = "tbctrlTablesAndLog";
            this.tbctrlTablesAndLog.SelectedIndex = 0;
            this.tbctrlTablesAndLog.Size = new System.Drawing.Size(561, 434);
            this.tbctrlTablesAndLog.TabIndex = 8;
            // 
            // tbpgTables
            // 
            this.tbpgTables.Controls.Add(this.chklstTables);
            this.tbpgTables.Location = new System.Drawing.Point(4, 25);
            this.tbpgTables.Name = "tbpgTables";
            this.tbpgTables.Padding = new System.Windows.Forms.Padding(3);
            this.tbpgTables.Size = new System.Drawing.Size(553, 405);
            this.tbpgTables.TabIndex = 0;
            this.tbpgTables.Text = "Tables";
            this.tbpgTables.UseVisualStyleBackColor = true;
            // 
            // chklstTables
            // 
            this.chklstTables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chklstTables.Location = new System.Drawing.Point(3, 3);
            this.chklstTables.Name = "chklstTables";
            this.chklstTables.Size = new System.Drawing.Size(547, 399);
            this.chklstTables.TabIndex = 1;
            this.chklstTables.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chklstTables_ItemCheck);
            // 
            // tbpgLog
            // 
            this.tbpgLog.Controls.Add(this.txtLog);
            this.tbpgLog.Location = new System.Drawing.Point(4, 25);
            this.tbpgLog.Name = "tbpgLog";
            this.tbpgLog.Padding = new System.Windows.Forms.Padding(3);
            this.tbpgLog.Size = new System.Drawing.Size(553, 405);
            this.tbpgLog.TabIndex = 1;
            this.tbpgLog.Text = "Log";
            this.tbpgLog.UseVisualStyleBackColor = true;
            // 
            // txtLog
            // 
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLog.Location = new System.Drawing.Point(3, 3);
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.Size = new System.Drawing.Size(547, 399);
            this.txtLog.TabIndex = 1;
            this.txtLog.Text = "";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.52941F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 76.47059F));
            this.tableLayoutPanel1.Controls.Add(this.lblNameSpace, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtNameSpace, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 93);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(561, 31);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // lblNameSpace
            // 
            this.lblNameSpace.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblNameSpace.AutoSize = true;
            this.lblNameSpace.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblNameSpace.Location = new System.Drawing.Point(10, 7);
            this.lblNameSpace.Margin = new System.Windows.Forms.Padding(10, 0, 3, 0);
            this.lblNameSpace.Name = "lblNameSpace";
            this.lblNameSpace.Size = new System.Drawing.Size(88, 16);
            this.lblNameSpace.TabIndex = 0;
            this.lblNameSpace.Text = "NameSpace:";
            // 
            // txtNameSpace
            // 
            this.txtNameSpace.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtNameSpace.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.txtNameSpace.Location = new System.Drawing.Point(146, 4);
            this.txtNameSpace.Margin = new System.Windows.Forms.Padding(15, 3, 15, 3);
            this.txtNameSpace.Name = "txtNameSpace";
            this.txtNameSpace.Size = new System.Drawing.Size(400, 23);
            this.txtNameSpace.TabIndex = 1;
            this.txtNameSpace.TextChanged += new System.EventHandler(this.TxtNameSpaceTextChangeEvent);
            // 
            // chkSelectAll
            // 
            this.chkSelectAll.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkSelectAll.AutoSize = true;
            this.chkSelectAll.Font = new System.Drawing.Font("Tahoma", 10F);
            this.chkSelectAll.Location = new System.Drawing.Point(20, 571);
            this.chkSelectAll.Margin = new System.Windows.Forms.Padding(20, 3, 3, 3);
            this.chkSelectAll.Name = "chkSelectAll";
            this.chkSelectAll.Size = new System.Drawing.Size(79, 21);
            this.chkSelectAll.TabIndex = 10;
            this.chkSelectAll.Text = "Select All";
            this.chkSelectAll.UseVisualStyleBackColor = true;
            this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
            // 
            // FrmCodeGenerator
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(567, 687);
            this.Controls.Add(this.tblLytMain);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(583, 726);
            this.MinimumSize = new System.Drawing.Size(583, 726);
            this.Name = "FrmCodeGenerator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Table Class Generator Form";
            this.Load += new System.EventHandler(this.FormLoad);
            this.tblLytMain.ResumeLayout(false);
            this.tblLytMain.PerformLayout();
            this.tblLytConnType.ResumeLayout(false);
            this.tblLytConnType.PerformLayout();
            this.tblLytConnString.ResumeLayout(false);
            this.tblLytConnString.PerformLayout();
            this.tblLytBrowse.ResumeLayout(false);
            this.tblLytBrowse.PerformLayout();
            this.tbctrlTablesAndLog.ResumeLayout(false);
            this.tbpgTables.ResumeLayout(false);
            this.tbpgLog.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblLytMain;
        private System.Windows.Forms.TableLayoutPanel tblLytConnType;
        private System.Windows.Forms.Label lblConnectionType;
        private System.Windows.Forms.ComboBox cmbConnectionType;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.TableLayoutPanel tblLytConnString;
        private System.Windows.Forms.Label lblConnectionString;
        private System.Windows.Forms.TextBox txtConnectionString;
        private System.Windows.Forms.Button btnGetTables;
        private System.Windows.Forms.Button btnSaveConnStr;
        private System.Windows.Forms.FolderBrowserDialog browserDestinationDialog;
        private System.Windows.Forms.TableLayoutPanel tblLytBrowse;
        private System.Windows.Forms.TextBox txtSaveToPath;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label lblSaveToPath;
        private System.Windows.Forms.TabControl tbctrlTablesAndLog;
        private System.Windows.Forms.TabPage tbpgTables;
        private System.Windows.Forms.CheckedListBox chklstTables;
        private System.Windows.Forms.TabPage tbpgLog;
        private System.Windows.Forms.RichTextBox txtLog;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblNameSpace;
        private System.Windows.Forms.TextBox txtNameSpace;
        private System.Windows.Forms.CheckBox chkSelectAll;
    }
}

