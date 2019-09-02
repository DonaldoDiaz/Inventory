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
    public partial class lstProducts : Form
    {
        public lstProducts()
        {
            InitializeComponent();
        }

        public void lstProducts_Load(Object sender, EventArgs e)
        {

            loadProducts();
        }

        public void loadProducts()
        {
            ConnectionDB conn = new ConnectionDB();
            bool isOpen = conn.Open();
            if (isOpen)
            {                
                SqlDataAdapter customerAdapter = conn.executeQuery("SELECT id AS ID, name AS Nombre, description AS Detalle, cost AS Costo, price AS Precio, status AS Estado FROM products");
                DataTable dataTable = new DataTable();
                customerAdapter.Fill(dataTable);
                dgvProducts.DataSource = dataTable;
            }
            else
            {
                MessageBox.Show(conn.errorMessage);
            }
            conn.Close();
        }

        private void BtnNew_Click(object sender, EventArgs e)
        {
            Productos products = new Productos();
            DialogResult dialogResult = products.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                loadProducts();
            }
        }

        private void DgvProducts_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int product_id = Convert.ToInt32(dgvProducts.CurrentRow.Cells[0].Value.ToString());
            Productos product = new Productos();
            product.product_id = product_id;
            DialogResult dialogResult = product.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                loadProducts();
            }
        }
    }
}
