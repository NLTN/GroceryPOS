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
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void btnProductSample_Click(object sender, EventArgs e)
        {
            new SampleForms.ProductSampleForm().ShowDialog();
        }

        private void btnCreateASale_Click(object sender, EventArgs e)
        {
            new SampleForms.CreateEditSaleSampleForm().ShowDialog();
        }
    }
}
