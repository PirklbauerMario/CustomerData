using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClassLibraryCustomerData;
using System.IO;

namespace FrmCustomer
{
    public partial class FormWelcome : Form
    {
        CustomerDatabase database = new CustomerDatabase();
        bool startpressed = false;

        public CustomerDatabase databaseOut
        {
            get;
            set;
        }
        public FormWelcome()
        {
            InitializeComponent();

            //Set last language
            try
            {
                StreamReader strReader = File.OpenText("LastLanguage.csv");
                string lastLanguage = strReader.ReadLine();
                strReader.Close();
                if (lastLanguage == "ENG")
                {
                    rdbGerman.Checked = false;
                    rdbEnglish.Checked = true;

                    Localization.UpdateLanguage("en-US");

                    UpdateFrmWelcome();
                }

                if (lastLanguage == "GER")
                {
                    rdbEnglish.Checked = false;
                    rdbGerman.Checked = true;

                    Localization.UpdateLanguage("de-DE");

                    UpdateFrmWelcome();
                }
            }
            catch
            {
                rdbGerman.Checked = false;
                rdbEnglish.Checked = true;

                Localization.UpdateLanguage("en-US");

                UpdateFrmWelcome();
            }


        }

        private void FormWelcome_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!startpressed)
            {
                databaseOut = new CustomerDatabase();
                Application.Exit();
            }

        }

        private void txbFilename_TextChanged(object sender, EventArgs e)
        {
            txbPassword.Enabled = true;
        }

        private void rdbLoad_CheckedChanged(object sender, EventArgs e)
        {
            txbFilename.Enabled = true;
            txbFilename.Focus();
        }

        private void txbFilename_TabStopChanged(object sender, EventArgs e)
        {
            txbPassword.Focus();
        }


        private void txbPassword_TextChanged(object sender, EventArgs e)
        {
            button1.Focus();
        }

        private void txbPassword_TabStopChanged(object sender, EventArgs e)
        {
            button1.Focus();
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                startpressed = true;
                if (rdbLoad.Checked == true)
                {
                    database.LoadDatabaseFromCsvDecrypted(txbFilename.Text, txbPassword.Text);
                    databaseOut = database;
                    this.Close();
                }
                else
                {
                    databaseOut = database;
                    this.Close();
                }

                //Store selected language
                StreamWriter strmWriter = new StreamWriter("LastLanguage.csv");
                if (rdbEnglish.Checked == true)
                {
                    strmWriter.WriteLine("ENG");
                }
                if (rdbGerman.Checked == true)
                {
                    strmWriter.WriteLine("GER");
                }
                strmWriter.Close();
            }
            catch (Exception er)
            {
                MessageBox.Show(Localization.getString(er.Message));
                startpressed = false;
                txbFilename.Text = "";
                txbPassword.Text = "";
            }
        }

        private void rdbEnglish_Click(object sender, EventArgs e)
        {
            // uncheck german button
            rdbGerman.Checked = false;

            if (rdbEnglish.Checked == true)
            {
                Localization.UpdateLanguage("en-US");

                UpdateFrmWelcome();
            }
        }

        private void rdbGerman_Click(object sender, EventArgs e)
        {
            // uncheck german button
            rdbEnglish.Checked = false;

            if (rdbGerman.Checked == true)
            {
                Localization.UpdateLanguage("de-DE");

                UpdateFrmWelcome();
            }
        }

        private void UpdateFrmWelcome()
        {
            // Update FrmWelcome
            groupBox2.Text = LangResources.FrmWelcomeLabelGroupbox;
            rdbNew.Text = LangResources.FrmWelcomeLabelRdbNew;
            rdbLoad.Text = LangResources.FrmWelcomeLabelRdbLoad;
            label1.Text = LangResources.FrmWelcomeLabel1;
            label2.Text = LangResources.FrmWelcomeLabel2;
            rdbEnglish.Text = LangResources.FrmWelcomeLabelRdbEnglish;
            rdbGerman.Text = LangResources.FrmWelcomeLabelRdbGerman;
            groupBox3.Text = LangResources.FrmWelcomeLabelGroupbox3;
            this.Text = LangResources.FrmWelcomeText;
        }

        private void rdbNew_CheckedChanged(object sender, EventArgs e)
        {
            txbFilename.Enabled = false;
            txbPassword.Enabled = false;
            button1.Focus();
        }
    }
}
