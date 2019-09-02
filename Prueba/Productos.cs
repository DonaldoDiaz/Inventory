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
    public partial class Productos : Form
    {
        public int product_id = 0;
        public Productos()
        {
            InitializeComponent();
        }

        private void Product_Load(object sender, EventArgs e)
        {
            if (product_id != 0)
            {
                ConnectionDB conn = new ConnectionDB();
                bool isOpen = conn.Open();
                if (isOpen)
                {
                    SqlDataAdapter productAdapter = conn.executeQuery("SELECT * FROM products WHERE id=" + product_id);
                    DataTable dataTable = new DataTable();
                    productAdapter.Fill(dataTable);
                    if (dataTable != null && dataTable.Rows.Count > 0)
                    {
                        txtNameProduct.Text = dataTable.Rows[0]["name"].ToString();
                        txtDetailProduct.Text = dataTable.Rows[0]["description"].ToString();
                        txtCost.Text = dataTable.Rows[0]["cost"].ToString();
                        txtPrice.Text = dataTable.Rows[0]["price"].ToString();
                        if (Convert.ToInt32(dataTable.Rows[0]["status"].ToString()) == 1)
                        {
                            checkState.Checked = true;
                        }
                        else
                        {
                            checkState.Checked = false;

                        }
                    }
                }
                else
                {
                    MessageBox.Show(conn.errorMessage);
                }
                conn.Close();
            }
        }
        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            ConnectionDB conn = new ConnectionDB();
            bool isOpen = conn.Open();
            if (isOpen)
            {
                string commandText = "";
                if (product_id == 0)
                {
                    commandText = "INSERT INTO products(name, description, cost, price, status) VALUES(@name, @description, @cost, @price, @status)";
                }
                else
                {
                    commandText = "UPDATE products SET name=@name, description=@description, cost=@cost, price=@price, status=@status WHERE id=@id";
                }
                SqlCommand command = new SqlCommand(commandText);
                command.Parameters.Add("@name", SqlDbType.NVarChar);
                command.Parameters["@name"].Value = txtNameProduct.Text;
                command.Parameters.Add("@description", SqlDbType.Text);
                command.Parameters["@description"].Value = txtDetailProduct.Text;
                command.Parameters.Add("@cost", SqlDbType.Float);
                command.Parameters["@cost"].Value = txtCost.Text;
                command.Parameters.Add("@price", SqlDbType.Float);
                command.Parameters["@price"].Value = txtPrice.Text;
                if (checkState.Checked)
                {
                    command.Parameters.Add("@status", SqlDbType.Int);
                    command.Parameters["@status"].Value = 1;
                }
                else {
                    command.Parameters.Add("@status", SqlDbType.Int);
                    command.Parameters["@status"].Value = 0;
                }
                if (product_id > 0)
                {
                    command.Parameters.Add("@id", SqlDbType.Int);
                    command.Parameters["@id"].Value = product_id;
                }
                int result = conn.executeNonQuery(command);
                if (result >= 0)
                {
                    if (product_id == 0)
                    {
                        DialogResult dialogResult = MessageBox.Show("Producto registrado. Desea registrar otro?", "Prueba", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            ClearForm();
                        }
                        else
                        {
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                    }
                    else
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Error al guardar el Producto", "Prueba");
                }
            }
            else
            {
                MessageBox.Show(conn.errorMessage);
            }
            conn.Close();
        }

        public void ClearForm()
        {
            txtNameProduct.Text = "";
            txtDetailProduct.Text = "";
            txtCost.Text = "";
            txtPrice.Text = "";
            txtNameProduct.Focus();
        }
    }
}
