using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prueba
{
    class ConnectionDB
    {
        SqlConnection conn;
        public string errorMessage = "";
        public ConnectionDB()
        {
            string appPath = Path.GetDirectoryName(Application.ExecutablePath);
            string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Proyectos\Inventory\Prueba\inventario.mdf;Integrated Security=True";
            //string connString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = " + appPath + @"\inventario.mdf; Integrated Security = True";
            this.conn = new SqlConnection(connString);
        }

        // SELECT 
        public SqlDataAdapter executeQuery(string query)
        {
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, this.conn);
                return adapter;
            }catch(Exception error)
            {
                this.errorMessage = error.Message;
                return null;
            }
            
        }

        // INSERT, UPDATE or DELETE
        public int executeNonQuery(SqlCommand command)
        {
            try
            {
                command.Connection = this.conn;
                return command.ExecuteNonQuery();
            }catch(Exception error)
            {
                this.errorMessage = error.Message;
                return -1;
            }
        }

        public Boolean Open()
        {
            try
            {
                this.conn.Open();
                return true;
            }catch(Exception error)
            {
                this.errorMessage = error.Message;
                return false;
            }
        }

        public Boolean Close()
        {
            try
            {
                this.conn.Close();
                return true;
            }catch(Exception error)
            {
                this.errorMessage = error.Message;
                return false;
            }
        }
    }
}
