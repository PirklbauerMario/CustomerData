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
    public partial class Form1 : Form
    {

        public CustomerDatabase database
        {
            get;
            set;
        }

        public Form1()
        {
            InitializeComponent();

            database = new CustomerDatabase();

            //Call Form Welcome

            FormWelcome fWel = new FormWelcome();
            fWel.ShowDialog();
            database = fWel.databaseOut;

            if (database.Count > 0)
            {
                btnEdit.Enabled = true;
                btnPay.Enabled = true;
                groupBox3.Enabled = true;
                groupBox2.Enabled = true;

                //Print
                foreach (var elem in database.Customers)
                {
                    dgvDisplay.Rows.Add(elem.CustomerNumber.ToString(), elem.LastName.ToString(), elem.FirstName.ToString(), elem.Mail.ToString(), elem.LastChange.ToString(), elem.BankBalance.ToString());
                }
                txbSearch.Focus();
            }
            //call set language method
            SetLanguage();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FormAdd fAdd = new FormAdd();
            fAdd.databaseIn = database;
            fAdd.ShowDialog();
            if (fAdd.databaseOut != null)
            {
                database = fAdd.databaseOut;
                btnEdit.Enabled = true;
                btnPay.Enabled = true;
                groupBox3.Enabled = true;
                groupBox2.Enabled = true;
            }

            //Print
            dgvDisplay.Rows.Clear();
            foreach (var elem in database.Customers)
            {
                dgvDisplay.Rows.Add(elem.CustomerNumber.ToString(), elem.LastName.ToString(), elem.FirstName.ToString(), elem.Mail.ToString(), elem.LastChange.ToString(), elem.BankBalance.ToString());
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            FormEdit fEdit = new FormEdit();
            fEdit.databaseIn = database;
            fEdit.ShowDialog();

            if (fEdit.databaseOut != null)
            {
                database = fEdit.databaseOut;
            }

            //Print
            dgvDisplay.Rows.Clear();
            foreach (var elem in database.Customers)
            {
                dgvDisplay.Rows.Add(elem.CustomerNumber.ToString(), elem.LastName.ToString(), elem.FirstName.ToString(), elem.Mail.ToString(), elem.LastChange.ToString(), elem.BankBalance.ToString());
            }
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            FormPay fPay = new FormPay();
            fPay.databaseIn = database;
            fPay.ShowDialog();
            if (fPay.databaseOut != null)
            {
                database = fPay.databaseOut;
            }

            //Print
            dgvDisplay.Rows.Clear();
            foreach (var elem in database.Customers)
            {
                dgvDisplay.Rows.Add(elem.CustomerNumber.ToString(), elem.LastName.ToString(), elem.FirstName.ToString(), elem.Mail.ToString(), elem.LastChange.ToString(), elem.BankBalance.ToString());
            }
        }

        private void txbSearch_TextChanged(object sender, EventArgs e)
        {
            btnSearch.Enabled = true;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {

                //Filter Datagridview
                string searchValue = txbSearch.Text.ToLower();
                dgvDisplay.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                bool valueResult = false;
                foreach (DataGridViewRow row in dgvDisplay.Rows)
                {
                    for (int i = 0; i < row.Cells.Count; i++)
                    {
                        dgvDisplay.Rows[row.Index].Selected = false;
                        if (row.Cells[i].Value != null && row.Cells[i].Value.ToString().ToLower().Equals(searchValue))
                        {
                            int rowIndex = row.Index;
                            dgvDisplay.Rows[rowIndex].Selected = true;
                            valueResult = true;
                            txbSearch.Text = "";
                            break;
                        }
                    }

                }
                if (!valueResult)
                {
                    MessageBox.Show(LangResources.MassageBoxSearch);
                    txbSearch.Text = "";
                    txbSearch.Focus();
                    return;
                }
                txbSearch.Focus();

            }
            catch (Exception er)
            {
                errorProvider1.SetError(btnSearch, Localization.getString(er.Message));
                txbSearch.Text = "";
            }

        }

        private void txbFilename_TextChanged(object sender, EventArgs e)
        {
            txbPassword.Enabled = true;
        }

        private void txbPassword_TextChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                database.SaveDatabaseToCSVEncrypted(txbFilename.Text, txbPassword.Text);
                MessageBox.Show(LangResources.MassageBoxSave);
                txbPassword.Text = "";
                txbFilename.Text = "";
            }

            catch (Exception er)
            {
                //errorProvider1.SetError(btnSave, Localization.getString(er.Message));
                MessageBox.Show(Localization.getString(er.Message));
                txbFilename.Text = "";
                txbPassword.Text = "";
            }
        }

        private void txbFilenameLoad_TextChanged(object sender, EventArgs e)
        {
            txbPasswordLoad.Enabled = true;
        }

        private void txbPasswordLoad_TextChanged(object sender, EventArgs e)
        {
            btnLoad.Enabled = true;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                database = new CustomerDatabase();
                database.LoadDatabaseFromCsvDecrypted(txbFilenameLoad.Text, txbPasswordLoad.Text);

                //Print

                dgvDisplay.Rows.Clear();
                foreach (var elem in database.Customers)
                {
                    dgvDisplay.Rows.Add(elem.CustomerNumber.ToString(), elem.LastName.ToString(), elem.FirstName.ToString(), elem.Mail.ToString(), elem.LastChange.ToString(), elem.BankBalance.ToString());
                }
                txbFilenameLoad.Text = "";
                txbPasswordLoad.Text = "";
                btnEdit.Enabled = true;
                btnPay.Enabled = true;
                groupBox3.Enabled = true;
                groupBox2.Enabled = true;
                btnLoad.Enabled = false;
                txbPasswordLoad.Enabled = false;
                MessageBox.Show(LangResources.MassegeBoxLoad);
            }
            catch (Exception er)
            {
                MessageBox.Show(Localization.getString(er.Message));
                txbFilenameLoad.Text = "";
                txbPasswordLoad.Text = "";
            }
        }


        private void SetLanguage()
        {
            this.Text = LangResources.Frm1Text;
            btnAdd.Text = LangResources.Frm1LableBtnAdd;
            btnEdit.Text = LangResources.Frm1LabelBtnEdit;
            btnPay.Text = LangResources.Frm1LabelBtnPay;
            dgvDisplay.Columns[1].HeaderText = LangResources.Frm1DgvLabelHeader1;
            dgvDisplay.Columns[2].HeaderText = LangResources.Frm1DgvLabelHeader2;
            dgvDisplay.Columns[4].HeaderText = LangResources.Frm1DgvLabelHeader4;
            dgvDisplay.Columns[5].HeaderText = LangResources.Frm1DgvLabelHeader5;
            groupBox3.Text = LangResources.Frm1LabelGroupBox3;
            btnSearch.Text = LangResources.Frm1LabelBtnSearch;
            groupBox5.Text = LangResources.Frm1LabelGroupBox5;
            groupBox2.Text = LangResources.Frm1LabelGroupBox2;
            btnSave.Text = LangResources.Frm1LabelBtnSave;
            btnLoad.Text = LangResources.Frm1LabelBtnLoad;
            label2.Text = LangResources.Frm1Label2u3;
            label3.Text = LangResources.Frm1Label2u3;
            label1.Text = LangResources.Frm1Label1u4;
            label4.Text = LangResources.Frm1Label1u4;
            toolTip1.SetToolTip(groupBox3, LangResources.TootipTextSearch);
            toolTip1.SetToolTip(txbSearch, LangResources.TootipTextSearch);
        }
    }
}
