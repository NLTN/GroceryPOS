using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using GroceryPOS.Data.DB;
using GroceryPOS.Data.BLL;


namespace GroceryPOS
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            // Print App Path.
            Console.WriteLine(Environment.CurrentDirectory);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            ProductBLL.Add(txtProductID.Text, txtProductName.Text, 20.00, "blueberry.jpg");
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            foreach (var i in ProductBLL.GetProductsBySearchString(txtSearchBox.Text))
            {
                Console.WriteLine(i.Name + "-" + i.Price);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            ProductBLL.Delete("FRUIT-001");
        }

        private void btnProductSample_Click(object sender, EventArgs e)
        {
            new SampleForms.ProductSampleForm().ShowDialog();
        }
    }
}
