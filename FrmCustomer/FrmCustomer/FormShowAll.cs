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
    public partial class FormShowAll : Form
    {
        public CustomerDatabase databaseIn
        {
            get;
            set;
        }
        public FormShowAll()
        {
            InitializeComponent();

            LinkedList<Customer> databaseSorted = databaseIn.GetSortedCustomers(1);

            foreach (var element in databaseSorted)
            {
                dataGridView1.Rows.Add(element.ToString());
            }
        }


    }
}
