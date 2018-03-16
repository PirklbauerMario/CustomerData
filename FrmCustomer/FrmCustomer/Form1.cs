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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormAdd fAdd = new FormAdd();
            fAdd.databaseIn = database;
            fAdd.ShowDialog();
            database = fAdd.databaseOut;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormEdit fEdit = new FormEdit();
            fEdit.databaseIn = database;
            fEdit.ShowDialog();
            database = fEdit.databaseOut;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int i = 2;
            FormShowAll fShow = new FormShowAll();
            fShow.databaseIn = database;
            fShow.ShowDialog();

        }

    }
}
