namespace FrmCustomer
{
    partial class FormWelcome
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
            this.components = new System.ComponentModel.Container();
            this.rdbNew = new System.Windows.Forms.RadioButton();
            this.rdbLoad = new System.Windows.Forms.RadioButton();
            this.txbFilename = new System.Windows.Forms.TextBox();
            this.txbPassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rdbEnglish = new System.Windows.Forms.RadioButton();
            this.rdbGerman = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // rdbNew
            // 
            this.rdbNew.AutoSize = true;
            this.rdbNew.Checked = true;
            this.rdbNew.Location = new System.Drawing.Point(20, 21);
            this.rdbNew.Name = "rdbNew";
            this.rdbNew.Size = new System.Drawing.Size(96, 17);
            this.rdbNew.TabIndex = 10;
            this.rdbNew.TabStop = true;
            this.rdbNew.Text = "New Database";
            this.rdbNew.UseVisualStyleBackColor = true;
            this.rdbNew.CheckedChanged += new System.EventHandler(this.rdbNew_CheckedChanged);
            // 
            // rdbLoad
            // 
            this.rdbLoad.AutoSize = true;
            this.rdbLoad.Location = new System.Drawing.Point(20, 44);
            this.rdbLoad.Name = "rdbLoad";
            this.rdbLoad.Size = new System.Drawing.Size(98, 17);
            this.rdbLoad.TabIndex = 10;
            this.rdbLoad.Text = "Load Database";
            this.rdbLoad.UseVisualStyleBackColor = true;
            this.rdbLoad.CheckedChanged += new System.EventHandler(this.rdbLoad_CheckedChanged);
            // 
            // txbFilename
            // 
            this.txbFilename.Enabled = false;
            this.txbFilename.Location = new System.Drawing.Point(20, 80);
            this.txbFilename.Name = "txbFilename";
            this.txbFilename.Size = new System.Drawing.Size(100, 20);
            this.txbFilename.TabIndex = 0;
            this.txbFilename.TabStopChanged += new System.EventHandler(this.txbFilename_TabStopChanged);
            this.txbFilename.TextChanged += new System.EventHandler(this.txbFilename_TextChanged);
            // 
            // txbPassword
            // 
            this.txbPassword.Enabled = false;
            this.txbPassword.Location = new System.Drawing.Point(126, 80);
            this.txbPassword.Name = "txbPassword";
            this.txbPassword.PasswordChar = '*';
            this.txbPassword.Size = new System.Drawing.Size(100, 20);
            this.txbPassword.TabIndex = 1;
            this.txbPassword.TabStopChanged += new System.EventHandler(this.txbPassword_TabStopChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(123, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Password";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Filename";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.rdbNew);
            this.groupBox2.Controls.Add(this.rdbLoad);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txbPassword);
            this.groupBox2.Controls.Add(this.txbFilename);
            this.groupBox2.Location = new System.Drawing.Point(6, 11);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(248, 115);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "ChooseDatabase";
            // 
            // rdbEnglish
            // 
            this.rdbEnglish.AutoSize = true;
            this.rdbEnglish.Checked = true;
            this.rdbEnglish.Location = new System.Drawing.Point(269, 32);
            this.rdbEnglish.Name = "rdbEnglish";
            this.rdbEnglish.Size = new System.Drawing.Size(59, 17);
            this.rdbEnglish.TabIndex = 8;
            this.rdbEnglish.TabStop = true;
            this.rdbEnglish.Text = "English";
            this.rdbEnglish.UseVisualStyleBackColor = true;
            this.rdbEnglish.Click += new System.EventHandler(this.rdbEnglish_Click);
            // 
            // rdbGerman
            // 
            this.rdbGerman.AutoSize = true;
            this.rdbGerman.Location = new System.Drawing.Point(269, 55);
            this.rdbGerman.Name = "rdbGerman";
            this.rdbGerman.Size = new System.Drawing.Size(62, 17);
            this.rdbGerman.TabIndex = 9;
            this.rdbGerman.Text = "German";
            this.rdbGerman.UseVisualStyleBackColor = true;
            this.rdbGerman.Click += new System.EventHandler(this.rdbGerman_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Location = new System.Drawing.Point(260, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(92, 81);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Language";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.button1);
            this.groupBox4.Controls.Add(this.groupBox2);
            this.groupBox4.Controls.Add(this.rdbGerman);
            this.groupBox4.Controls.Add(this.rdbEnglish);
            this.groupBox4.Controls.Add(this.groupBox3);
            this.groupBox4.Location = new System.Drawing.Point(12, 7);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(357, 136);
            this.groupBox4.TabIndex = 12;
            this.groupBox4.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(269, 99);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // FormWelcome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(381, 155);
            this.Controls.Add(this.groupBox4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormWelcome";
            this.Text = "Welcome";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormWelcome_FormClosing);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton rdbNew;
        private System.Windows.Forms.RadioButton rdbLoad;
        private System.Windows.Forms.TextBox txbFilename;
        private System.Windows.Forms.TextBox txbPassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rdbEnglish;
        private System.Windows.Forms.RadioButton rdbGerman;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Button button1;
    }
}