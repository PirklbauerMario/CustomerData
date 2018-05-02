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
    public partial class FormPay : Form
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

        public FormPay()
        {
            InitializeComponent();
            SetLanguage();
        }

        private void txbNumber_Validating(object sender, CancelEventArgs e)
        {

            int cNumber;
            if (!int.TryParse(txbNumber.Text, out cNumber))
            {
                e.Cancel = true;
                //errorProvider1.SetError(txbNumber, "Type in a number!");
                errorProvider1.SetError(txbNumber, Localization.getString("FrmEditNumberReq"));
                btnOK.Enabled = false;
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void txbAmmount_Validating(object sender, CancelEventArgs e)
        {
            double cNumber;
            if (!double.TryParse(txbAmmount.Text, out cNumber))
            {
                e.Cancel = true;
                //errorProvider1.SetError(radioButton2, "Type in a number!");
                errorProvider1.SetError(radioButton2, Localization.getString("FrmEditNumberReq"));
                btnOK.Enabled = false;
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void txbAmmount_TextChanged(object sender, EventArgs e)
        {
            btnOK.Enabled = true;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                int number = (Convert.ToInt32(txbNumber.Text));

                if (databaseIn.Count - 1 < number || number < 0)
                {
                    throw new InvalidDataException(Localization.getString("FrmPayNoCustomer"));
                }
                else
                {
                    if (radioButton1.Checked == true)//Inpayment
                    {
                        databaseIn.ChangeBankBalance(number, databaseIn.Customers.ElementAt(number).BankBalance + Convert.ToDouble(txbAmmount.Text.Replace(',','.')));
                    }
                    if (radioButton2.Checked == true)//Outpayment
                    {
                        databaseIn.ChangeBankBalance(number, databaseIn.Customers.ElementAt(number).BankBalance - Convert.ToDouble(txbAmmount.Text.Replace(',', '.')));
                    }
                }


                databaseOut = databaseIn;

                this.Close();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
                txbAmmount.Text = "";
                txbNumber.Text = "";
            }


        }
        private void SetLanguage()
        {
            this.Text = LangResources.FrmPayText;
            groupBox1.Text = LangResources.FrmPayLabelGroupBox1;
            groupBox2.Text = LangResources.FrmPayLabelGroupBox2;
            radioButton1.Text = LangResources.FrmPayLabelRdb1;
            radioButton2.Text = LangResources.FrmPayLabelRdb2;
        }
    }
}
