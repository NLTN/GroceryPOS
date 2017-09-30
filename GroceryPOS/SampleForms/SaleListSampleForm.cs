using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GroceryPOS.Data.BLL;
using GroceryPOS.Data.DAL;

namespace GroceryPOS.SampleForms
{
    public partial class SaleListSampleForm : Form
    {
        public SaleListSampleForm()
        {
            InitializeComponent();
        }

        private void SaleListSampleForm_Load(object sender, EventArgs e)
        {
            var a = new SaleDAL().GetAllSales();

            foreach (var i in a)
            {
                Console.WriteLine(i.SaleID);

                foreach (var j in i.SaleItems)
                {
                    Console.WriteLine(">>>>>>" + j.ProductID + " | " + j.ProductName + " | " + j.Quantity + " | " + j.Price);
                }


            }
        }
    }
}
