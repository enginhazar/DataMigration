namespace WindowsFormsApplication1
{
    partial class Form1
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
            this.textTableNameFilter = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.connectionString = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboProvider = new System.Windows.Forms.ComboBox();
            this.tableListBox = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonHedef = new System.Windows.Forms.Button();
            this.hedefConnectionString = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonHedefTabloOlustur = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.buttonKayitAktar = new System.Windows.Forms.Button();
            this.richTextBoxLog = new System.Windows.Forms.RichTextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textTableNameFilter);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.buttonConnect);
            this.groupBox1.Controls.Add(this.connectionString);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.comboProvider);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(439, 104);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Kaynak Data";
            // 
            // textTableNameFilter
            // 
            this.textTableNameFilter.Enabled = false;
            this.textTableNameFilter.Location = new System.Drawing.Point(314, 73);
            this.textTableNameFilter.Name = "textTableNameFilter";
            this.textTableNameFilter.Size = new System.Drawing.Size(100, 20);
            this.textTableNameFilter.TabIndex = 6;
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(232, 72);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Tablo Getir";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(150, 73);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(75, 23);
            this.buttonConnect.TabIndex = 4;
            this.buttonConnect.Text = "baglan";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // connectionString
            // 
            this.connectionString.Location = new System.Drawing.Point(150, 46);
            this.connectionString.Name = "connectionString";
            this.connectionString.Size = new System.Drawing.Size(277, 20);
            this.connectionString.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Connection String";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Provider";
            // 
            // comboProvider
            // 
            this.comboProvider.FormattingEnabled = true;
            this.comboProvider.Items.AddRange(new object[] {
            "DB2"});
            this.comboProvider.Location = new System.Drawing.Point(150, 20);
            this.comboProvider.Name = "comboProvider";
            this.comboProvider.Size = new System.Drawing.Size(121, 21);
            this.comboProvider.TabIndex = 0;
            this.comboProvider.SelectedIndexChanged += new System.EventHandler(this.comboProvider_SelectedIndexChanged);
            // 
            // tableListBox
            // 
            this.tableListBox.FormattingEnabled = true;
            this.tableListBox.Location = new System.Drawing.Point(13, 123);
            this.tableListBox.Name = "tableListBox";
            this.tableListBox.Size = new System.Drawing.Size(149, 316);
            this.tableListBox.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonHedef);
            this.groupBox2.Controls.Add(this.hedefConnectionString);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(471, 16);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(294, 100);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Hedef SQL";
            // 
            // buttonHedef
            // 
            this.buttonHedef.Location = new System.Drawing.Point(9, 67);
            this.buttonHedef.Name = "buttonHedef";
            this.buttonHedef.Size = new System.Drawing.Size(75, 23);
            this.buttonHedef.TabIndex = 5;
            this.buttonHedef.Text = "baglan";
            this.buttonHedef.UseVisualStyleBackColor = true;
            this.buttonHedef.Click += new System.EventHandler(this.buttonHedef_Click);
            // 
            // hedefConnectionString
            // 
            this.hedefConnectionString.Location = new System.Drawing.Point(6, 40);
            this.hedefConnectionString.Name = "hedefConnectionString";
            this.hedefConnectionString.Size = new System.Drawing.Size(277, 20);
            this.hedefConnectionString.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Connection String";
            // 
            // buttonHedefTabloOlustur
            // 
            this.buttonHedefTabloOlustur.Enabled = false;
            this.buttonHedefTabloOlustur.Location = new System.Drawing.Point(180, 123);
            this.buttonHedefTabloOlustur.Name = "buttonHedefTabloOlustur";
            this.buttonHedefTabloOlustur.Size = new System.Drawing.Size(80, 64);
            this.buttonHedefTabloOlustur.TabIndex = 3;
            this.buttonHedefTabloOlustur.Text = "Hedef Tablo Oluştur";
            this.buttonHedefTabloOlustur.UseVisualStyleBackColor = true;
            this.buttonHedefTabloOlustur.Click += new System.EventHandler(this.buttonHedefTabloOlustur_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.progressBar1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 445);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1000, 34);
            this.panel1.TabIndex = 4;
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressBar1.Location = new System.Drawing.Point(0, 0);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(1000, 34);
            this.progressBar1.TabIndex = 0;
            // 
            // buttonKayitAktar
            // 
            this.buttonKayitAktar.Enabled = false;
            this.buttonKayitAktar.Location = new System.Drawing.Point(180, 193);
            this.buttonKayitAktar.Name = "buttonKayitAktar";
            this.buttonKayitAktar.Size = new System.Drawing.Size(80, 64);
            this.buttonKayitAktar.TabIndex = 5;
            this.buttonKayitAktar.Text = "Kayıt Aktar";
            this.buttonKayitAktar.UseVisualStyleBackColor = true;
            this.buttonKayitAktar.Click += new System.EventHandler(this.buttonKayitAktar_Click);
            // 
            // richTextBoxLog
            // 
            this.richTextBoxLog.Location = new System.Drawing.Point(385, 137);
            this.richTextBoxLog.Name = "richTextBoxLog";
            this.richTextBoxLog.Size = new System.Drawing.Size(449, 302);
            this.richTextBoxLog.TabIndex = 6;
            this.richTextBoxLog.Text = "";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(180, 275);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(80, 58);
            this.button2.TabIndex = 7;
            this.button2.Text = "Kayıt Aktar File";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(180, 349);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(80, 64);
            this.button3.TabIndex = 8;
            this.button3.Text = "Kayıt Aktar Taşınmayan";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(266, 193);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 64);
            this.button4.TabIndex = 9;
            this.button4.Text = "DataReader";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 479);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.richTextBoxLog);
            this.Controls.Add(this.buttonKayitAktar);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.buttonHedefTabloOlustur);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.tableListBox);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox connectionString;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboProvider;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox tableListBox;
        private System.Windows.Forms.TextBox textTableNameFilter;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonHedef;
        private System.Windows.Forms.TextBox hedefConnectionString;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonHedefTabloOlustur;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button buttonKayitAktar;
        public System.Windows.Forms.RichTextBox richTextBoxLog;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
    }
}

