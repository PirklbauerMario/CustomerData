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

namespace FrmCustomer
{
    public partial class FormAdd : Form
    {

        public CustomerDatabase databaseIn
        {
            get;
            set;
        }
        public CustomerDatabase databaseOut
        {
            get;
            set;
        }
        public FormAdd()
        {
            InitializeComponent();
            SetLanguage();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                databaseIn.AddCustomer(textBox1.Text, textBox2.Text, textBox3.Text, 0.0);
                databaseOut = databaseIn;

                this.Close();
            }
            catch(Exception er)
            {
                //errorProvider1.SetError(button1, er.Message);
                errorProvider1.SetError(button1, Localization.getString(er.Message));

                
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text!=""&&textBox3.Text!="")
            {
                button1.Enabled = true;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox3.Text != "")
            {
                button1.Enabled = true;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                button1.Enabled = true;
            }
        }
        private void SetLanguage()
        {
            this.Text = LangResources.FrmAddText;
            groupBox1.Text = LangResources.FrmAddLabelGroupBox1;
            groupBox2.Text = LangResources.FrmAddLabelGroupBox2;

        }
    }
}
