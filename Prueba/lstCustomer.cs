using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prueba
{
    public partial class lstCustomer : Form
    {
        public lstCustomer()
        {
            InitializeComponent();
        }

        private void lstCustomer_Load(object sender, EventArgs e)
        {
            loadCustomers();
        }

        private void loadCustomers()
        {
            ConnectionDB conn = new ConnectionDB();
            bool isOpen = conn.Open();
            if (isOpen)
            {
                SqlDataAdapter customerAdapter = conn.executeQuery("SELECT id AS ID, name AS Nombres, last_name AS Apellidos, address AS Dirección, cellphone AS Célular FROM customer");
                DataTable dataTable = new DataTable();
                customerAdapter.Fill(dataTable);
                dgvCustomers.DataSource = dataTable;
            }
            else
            {
                MessageBox.Show(conn.errorMessage);
            }
            conn.Close();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            Customer customer = new Customer();
            DialogResult dialogResult = customer.ShowDialog();
            if(dialogResult == DialogResult.OK)
            {
                loadCustomers();
            }
        }

        private void dgvCustomers_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int customer_id = Convert.ToInt32(dgvCustomers.CurrentRow.Cells[0].Value.ToString());
            Customer customer = new Customer();
            customer.customer_id = customer_id;
            DialogResult dialogResult = customer.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                loadCustomers();
            }
        }
    }
}
