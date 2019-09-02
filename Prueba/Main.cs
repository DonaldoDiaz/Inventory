using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prueba
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void verToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lstCustomer client = new lstCustomer();
            client.ShowDialog();
        }

        private void ListadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lstProducts products = new lstProducts();
            products.ShowDialog();
        }
    }
}
