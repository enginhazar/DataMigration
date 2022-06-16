namespace DbConverter
{
    partial class DbConverterFrm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.GetTableButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.sourceConnectionStringTxtBox = new System.Windows.Forms.TextBox();
            this.Db = new System.Windows.Forms.Label();
            this.sourceDatabaseTypeCombo = new System.Windows.Forms.ComboBox();
            this.sourceConnectBtn = new System.Windows.Forms.Button();
            this.sourceTableGridView = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.targeDatabaseRoleTxt = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.targetDatabaseTxt = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.targetConnectBtn = new System.Windows.Forms.Button();
            this.targetConnectionStringTxtBox = new System.Windows.Forms.TextBox();
            this.targetDatabaseTypeCombo = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.convertBtn = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.TablesCreateIndex = new System.Windows.Forms.CheckBox();
            this.truncateTableChecked = new System.Windows.Forms.CheckBox();
            this.RecreateDatabase = new System.Windows.Forms.CheckBox();
            this.recreateTableCheck = new System.Windows.Forms.CheckBox();
            this.DataConvertCheck = new System.Windows.Forms.CheckBox();
            this.ConvertProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sourceTableGridView)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.GetTableButton);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.sourceConnectionStringTxtBox);
            this.groupBox1.Controls.Add(this.Db);
            this.groupBox1.Controls.Add(this.sourceDatabaseTypeCombo);
            this.groupBox1.Controls.Add(this.sourceConnectBtn);
            this.groupBox1.Location = new System.Drawing.Point(43, 43);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(644, 132);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Source Database";
            // 
            // GetTableButton
            // 
            this.GetTableButton.Location = new System.Drawing.Point(361, 95);
            this.GetTableButton.Margin = new System.Windows.Forms.Padding(4);
            this.GetTableButton.Name = "GetTableButton";
            this.GetTableButton.Size = new System.Drawing.Size(100, 28);
            this.GetTableButton.TabIndex = 4;
            this.GetTableButton.Text = "Get Tables";
            this.GetTableButton.UseVisualStyleBackColor = true;
            this.GetTableButton.Click += new System.EventHandler(this.GetTableButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 68);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(220, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Source Database Connection String";
            // 
            // sourceConnectionStringTxtBox
            // 
            this.sourceConnectionStringTxtBox.Location = new System.Drawing.Point(252, 63);
            this.sourceConnectionStringTxtBox.Margin = new System.Windows.Forms.Padding(4);
            this.sourceConnectionStringTxtBox.Name = "sourceConnectionStringTxtBox";
            this.sourceConnectionStringTxtBox.Size = new System.Drawing.Size(377, 22);
            this.sourceConnectionStringTxtBox.TabIndex = 2;
                       this.sourceConnectionStringTxtBox.TextChanged += new System.EventHandler(this.sourceConnectionStringTxtBox_TextChanged);
            // 
            // Db
            // 
            this.Db.AutoSize = true;
            this.Db.Location = new System.Drawing.Point(8, 33);
            this.Db.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Db.Name = "Db";
            this.Db.Size = new System.Drawing.Size(148, 16);
            this.Db.TabIndex = 1;
            this.Db.Text = "Source Database Type";
            // 
            // sourceDatabaseTypeCombo
            // 
            this.sourceDatabaseTypeCombo.FormattingEnabled = true;
            this.sourceDatabaseTypeCombo.Location = new System.Drawing.Point(252, 30);
            this.sourceDatabaseTypeCombo.Margin = new System.Windows.Forms.Padding(4);
            this.sourceDatabaseTypeCombo.Name = "sourceDatabaseTypeCombo";
            this.sourceDatabaseTypeCombo.Size = new System.Drawing.Size(160, 24);
            this.sourceDatabaseTypeCombo.TabIndex = 1;
            // 
            // sourceConnectBtn
            // 
            this.sourceConnectBtn.Location = new System.Drawing.Point(252, 95);
            this.sourceConnectBtn.Margin = new System.Windows.Forms.Padding(4);
            this.sourceConnectBtn.Name = "sourceConnectBtn";
            this.sourceConnectBtn.Size = new System.Drawing.Size(100, 28);
            this.sourceConnectBtn.TabIndex = 0;
            this.sourceConnectBtn.Text = "Connect";
            this.sourceConnectBtn.UseVisualStyleBackColor = true;
            this.sourceConnectBtn.Click += new System.EventHandler(this.sourceConnectBtn_Click);
            // 
            // sourceTableGridView
            // 
            this.sourceTableGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.sourceTableGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sourceTableGridView.Location = new System.Drawing.Point(4, 19);
            this.sourceTableGridView.Margin = new System.Windows.Forms.Padding(4);
            this.sourceTableGridView.Name = "sourceTableGridView";
            this.sourceTableGridView.RowHeadersWidth = 51;
            this.sourceTableGridView.Size = new System.Drawing.Size(541, 295);
            this.sourceTableGridView.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.sourceTableGridView);
            this.groupBox2.Location = new System.Drawing.Point(43, 182);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(549, 318);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Source Tables";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.targeDatabaseRoleTxt);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.targetDatabaseTxt);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.targetConnectBtn);
            this.groupBox3.Controls.Add(this.targetConnectionStringTxtBox);
            this.groupBox3.Controls.Add(this.targetDatabaseTypeCombo);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Location = new System.Drawing.Point(731, 58);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(643, 164);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Target Database";
            // 
            // targeDatabaseRoleTxt
            // 
            this.targeDatabaseRoleTxt.Location = new System.Drawing.Point(251, 124);
            this.targeDatabaseRoleTxt.Margin = new System.Windows.Forms.Padding(4);
            this.targeDatabaseRoleTxt.Name = "targeDatabaseRoleTxt";
            this.targeDatabaseRoleTxt.Size = new System.Drawing.Size(132, 22);
            this.targeDatabaseRoleTxt.TabIndex = 8;
            this.targeDatabaseRoleTxt.Text = "admin";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 124);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(142, 16);
            this.label5.TabIndex = 7;
            this.label5.Text = "Target Database Role";
            // 
            // targetDatabaseTxt
            // 
            this.targetDatabaseTxt.Location = new System.Drawing.Point(251, 90);
            this.targetDatabaseTxt.Margin = new System.Windows.Forms.Padding(4);
            this.targetDatabaseTxt.Name = "targetDatabaseTxt";
            this.targetDatabaseTxt.Size = new System.Drawing.Size(132, 22);
            this.targetDatabaseTxt.TabIndex = 6;
            this.targetDatabaseTxt.Text = "testConvert";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 98);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 16);
            this.label4.TabIndex = 5;
            this.label4.Text = "Target Database";
            // 
            // targetConnectBtn
            // 
            this.targetConnectBtn.Location = new System.Drawing.Point(535, 100);
            this.targetConnectBtn.Margin = new System.Windows.Forms.Padding(4);
            this.targetConnectBtn.Name = "targetConnectBtn";
            this.targetConnectBtn.Size = new System.Drawing.Size(100, 28);
            this.targetConnectBtn.TabIndex = 4;
            this.targetConnectBtn.Text = "Connect";
            this.targetConnectBtn.UseVisualStyleBackColor = true;
            this.targetConnectBtn.Click += new System.EventHandler(this.targetConnectBtn_Click);
            // 
            // targetConnectionStringTxtBox
            // 
            this.targetConnectionStringTxtBox.Location = new System.Drawing.Point(251, 57);
            this.targetConnectionStringTxtBox.Margin = new System.Windows.Forms.Padding(4);
            this.targetConnectionStringTxtBox.Name = "targetConnectionStringTxtBox";
            this.targetConnectionStringTxtBox.Size = new System.Drawing.Size(327, 22);
            this.targetConnectionStringTxtBox.TabIndex = 3;
            this.targetConnectionStringTxtBox.Text = "User ID=admin;Password=1;Host=127.0.0.1;Port=5432;db=postgres";
            // 
            // targetDatabaseTypeCombo
            // 
            this.targetDatabaseTypeCombo.FormattingEnabled = true;
            this.targetDatabaseTypeCombo.Location = new System.Drawing.Point(251, 23);
            this.targetDatabaseTypeCombo.Margin = new System.Windows.Forms.Padding(4);
            this.targetDatabaseTypeCombo.Name = "targetDatabaseTypeCombo";
            this.targetDatabaseTypeCombo.Size = new System.Drawing.Size(160, 24);
            this.targetDatabaseTypeCombo.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 60);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(217, 16);
            this.label3.TabIndex = 1;
            this.label3.Text = "Target Database Connection String";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 25);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(145, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "Target Database Type";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1416, 596);
            this.tabControl1.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.toolStrip1);
            this.tabPage1.Controls.Add(this.convertBtn);
            this.tabPage1.Controls.Add(this.richTextBox1);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage1.Size = new System.Drawing.Size(1408, 567);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Database Convert";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.ConvertProgressBar});
            this.toolStrip1.Location = new System.Drawing.Point(4, 538);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1400, 25);
            this.toolStrip1.TabIndex = 6;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(111, 22);
            this.toolStripLabel1.Text = "toolStripLabel1";
            // 
            // convertBtn
            // 
            this.convertBtn.Enabled = false;
            this.convertBtn.Location = new System.Drawing.Point(611, 320);
            this.convertBtn.Margin = new System.Windows.Forms.Padding(4);
            this.convertBtn.Name = "convertBtn";
            this.convertBtn.Size = new System.Drawing.Size(100, 68);
            this.convertBtn.TabIndex = 5;
            this.convertBtn.Text = "Convert";
            this.convertBtn.UseVisualStyleBackColor = true;
            this.convertBtn.Click += new System.EventHandler(this.convertBtn_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(731, 230);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(4);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(643, 270);
            this.richTextBox1.TabIndex = 4;
            this.richTextBox1.Text = "";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.TablesCreateIndex);
            this.tabPage2.Controls.Add(this.truncateTableChecked);
            this.tabPage2.Controls.Add(this.RecreateDatabase);
            this.tabPage2.Controls.Add(this.recreateTableCheck);
            this.tabPage2.Controls.Add(this.DataConvertCheck);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage2.Size = new System.Drawing.Size(1408, 567);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Convert Settings";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // TablesCreateIndex
            // 
            this.TablesCreateIndex.AutoSize = true;
            this.TablesCreateIndex.Location = new System.Drawing.Point(219, 28);
            this.TablesCreateIndex.Margin = new System.Windows.Forms.Padding(4);
            this.TablesCreateIndex.Name = "TablesCreateIndex";
            this.TablesCreateIndex.Size = new System.Drawing.Size(150, 20);
            this.TablesCreateIndex.TabIndex = 7;
            this.TablesCreateIndex.Text = "Tables Create Index";
            this.TablesCreateIndex.UseVisualStyleBackColor = true;
            // 
            // truncateTableChecked
            // 
            this.truncateTableChecked.AutoSize = true;
            this.truncateTableChecked.Location = new System.Drawing.Point(28, 57);
            this.truncateTableChecked.Margin = new System.Windows.Forms.Padding(4);
            this.truncateTableChecked.Name = "truncateTableChecked";
            this.truncateTableChecked.Size = new System.Drawing.Size(121, 20);
            this.truncateTableChecked.TabIndex = 6;
            this.truncateTableChecked.Text = "Truncate Table";
            this.truncateTableChecked.UseVisualStyleBackColor = true;
            // 
            // RecreateDatabase
            // 
            this.RecreateDatabase.AutoSize = true;
            this.RecreateDatabase.Location = new System.Drawing.Point(28, 113);
            this.RecreateDatabase.Margin = new System.Windows.Forms.Padding(4);
            this.RecreateDatabase.Name = "RecreateDatabase";
            this.RecreateDatabase.Size = new System.Drawing.Size(148, 20);
            this.RecreateDatabase.TabIndex = 5;
            this.RecreateDatabase.Text = "Recreate Database";
            this.RecreateDatabase.UseVisualStyleBackColor = true;
            // 
            // recreateTableCheck
            // 
            this.recreateTableCheck.AutoSize = true;
            this.recreateTableCheck.Location = new System.Drawing.Point(28, 85);
            this.recreateTableCheck.Margin = new System.Windows.Forms.Padding(4);
            this.recreateTableCheck.Name = "recreateTableCheck";
            this.recreateTableCheck.Size = new System.Drawing.Size(124, 20);
            this.recreateTableCheck.TabIndex = 4;
            this.recreateTableCheck.Text = "Recreate Table";
            this.recreateTableCheck.UseVisualStyleBackColor = true;
            // 
            // DataConvertCheck
            // 
            this.DataConvertCheck.AutoSize = true;
            this.DataConvertCheck.Checked = true;
            this.DataConvertCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.DataConvertCheck.Location = new System.Drawing.Point(28, 28);
            this.DataConvertCheck.Margin = new System.Windows.Forms.Padding(4);
            this.DataConvertCheck.Name = "DataConvertCheck";
            this.DataConvertCheck.Size = new System.Drawing.Size(107, 20);
            this.DataConvertCheck.TabIndex = 3;
            this.DataConvertCheck.Text = "Data Convert";
            this.DataConvertCheck.UseVisualStyleBackColor = true;
            // 
            // ConvertProgressBar
            // 
            this.ConvertProgressBar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.ConvertProgressBar.Name = "ConvertProgressBar";
            this.ConvertProgressBar.Size = new System.Drawing.Size(100, 22);
            // 
            // DbConverterFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1416, 596);
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "DbConverterFrm";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sourceTableGridView)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label Db;
        private System.Windows.Forms.ComboBox sourceDatabaseTypeCombo;
        private System.Windows.Forms.Button sourceConnectBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox sourceConnectionStringTxtBox;
        private System.Windows.Forms.Button GetTableButton;
        private System.Windows.Forms.DataGridView sourceTableGridView;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button targetConnectBtn;
        private System.Windows.Forms.TextBox targetConnectionStringTxtBox;
        private System.Windows.Forms.ComboBox targetDatabaseTypeCombo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button convertBtn;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.CheckBox recreateTableCheck;
        private System.Windows.Forms.CheckBox DataConvertCheck;
        private System.Windows.Forms.CheckBox RecreateDatabase;
        private System.Windows.Forms.TextBox targetDatabaseTxt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox truncateTableChecked;
        private System.Windows.Forms.CheckBox TablesCreateIndex;
        private System.Windows.Forms.TextBox targeDatabaseRoleTxt;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripProgressBar ConvertProgressBar;
    }
}

