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
    public partial class Customer : Form
    {
        public int customer_id = 0;
        public Customer()
        {
            InitializeComponent();
        }

        private void Customer_Load(object sender, EventArgs e)
        {
            if (customer_id != 0)
            {
                ConnectionDB conn = new ConnectionDB();
                bool isOpen = conn.Open();
                if (isOpen)
                {
                    SqlDataAdapter customerAdapter = conn.executeQuery("SELECT * FROM customer WHERE id="+customer_id);
                    DataTable dataTable = new DataTable();
                    customerAdapter.Fill(dataTable);
                    if(dataTable != null && dataTable.Rows.Count > 0)
                    {
                        txtName.Text = dataTable.Rows[0]["name"].ToString();
                        txtLastName.Text = dataTable.Rows[0]["last_name"].ToString();
                        txtAddress.Text = dataTable.Rows[0]["address"].ToString();
                        txtCellphone.Text = dataTable.Rows[0]["cellphone"].ToString();
                    }
                }
                else
                {
                    MessageBox.Show(conn.errorMessage);
                }
                conn.Close();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ConnectionDB conn = new ConnectionDB();
            bool isOpen = conn.Open();
            if (isOpen)
            {
                string commandText = "";
                if (customer_id == 0)
                {
                    commandText = "INSERT INTO customer(name, last_name, address, cellphone) VALUES(@name, @last_name, @address, @cellphone)";
                }
                else
                {
                    commandText = "UPDATE customer SET name=@name, last_name=@last_name, address=@address, cellphone=@cellphone WHERE id=@id";
                }
                SqlCommand command = new SqlCommand(commandText);
                command.Parameters.Add("@name", SqlDbType.NVarChar);
                command.Parameters["@name"].Value = txtName.Text;
                command.Parameters.Add("@last_name", SqlDbType.NVarChar);
                command.Parameters["@last_name"].Value = txtLastName.Text;
                command.Parameters.Add("@address", SqlDbType.NVarChar);
                command.Parameters["@address"].Value = txtAddress.Text;
                command.Parameters.Add("@cellphone", SqlDbType.NVarChar);
                command.Parameters["@cellphone"].Value = txtCellphone.Text;
                if(customer_id > 0)
                {
                    command.Parameters.Add("@id", SqlDbType.Int);
                    command.Parameters["@id"].Value = customer_id;
                }
                int result = conn.executeNonQuery(command);
                if (result >= 0)
                {
                    if (customer_id == 0)
                    {
                        DialogResult dialogResult = MessageBox.Show("Cliente registrado. Desea registrar otro?", "Prueba", MessageBoxButtons.YesNo);
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
                    MessageBox.Show("Error al guardar el cliente", "Prueba");
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
            txtName.Text = "";
            txtLastName.Text = "";
            txtAddress.Text = "";
            txtCellphone.Text = "";
            txtName.Focus();
        }
    }
}
