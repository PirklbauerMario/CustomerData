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
    public partial class FormEdit : Form
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
        public FormEdit()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                databaseIn.ChangeLastName(Int32.Parse(textBox1.Text), textBox2.Text);
                databaseIn.ChangeMail(Int32.Parse(textBox1.Text), textBox3.Text);
                databaseOut = databaseIn;
                
                this.Close();
            }
            catch (Exception er)
            {
                errorProvider1.SetError(button1, er.Message);
            }
        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            int cNumber;
            if (!int.TryParse(textBox1.Text ,out cNumber))
            {
                e.Cancel = true;
                errorProvider1.SetError(textBox1, "Type in a number!");
                button1.Enabled = false;
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void textBox2_Validated(object sender, EventArgs e)
        {
            button1.Enabled = true;
        }

        private void textBox3_Validated(object sender, EventArgs e)
        {
            button1.Enabled = true;
        }
    }
}
