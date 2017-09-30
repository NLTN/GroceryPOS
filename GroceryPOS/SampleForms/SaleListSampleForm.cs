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

namespace GroceryPOS.SampleForms
{
    public partial class SaleListSampleForm : Form
    {
        #region Constructor
        public SaleListSampleForm()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods
        private void LoadAllSales()
        {
            // Remove all items in Sales ListView
            lvSales.Items.Clear();

            // Get All Sales, and add them to the List View
            foreach (var s in SaleBLL.GetAllSales())
            {
                // Create a list item
                ListViewItem li = new ListViewItem(s.SaleID);

                li.SubItems.Add(s.Datetime.ToString());
                li.SubItems.Add(s.Total.ToString("C"));

                // Add the list item to the ListView
                lvSales.Items.Add(li);
            }
        }
        #endregion

        #region Events
        private void SaleListSampleForm_Load(object sender, EventArgs e)
        {
            LoadAllSales();
        }

        private void lvSales_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((ListView)sender).SelectedItems.Count == 0)
            {
                return;
            }

            // Remove all items in lvOrder
            lvOrder.Items.Clear();

            var sale = SaleBLL.GetSaleByID(((ListView)sender).SelectedItems[0].Text);
            Console.WriteLine(sale.SaleItems.Count());

            foreach (var i in sale.SaleItems)
            {
                // Calculate sub total
                decimal subTotal = (decimal)i.Quantity * i.Price;

                // Create a ListViewItem
                ListViewItem li = new ListViewItem(i.ProductName);

                // Add some text values to ListViewItem

                li.SubItems.Add(i.Quantity.ToString());
                li.SubItems.Add(i.Price.ToString("C"));
                li.SubItems.Add(subTotal.ToString("C"));

                // Add the ListViewItem to ListView
                lvOrder.Items.Add(li);
            }

            // Summary
            lblTotal.Text = sale.Total.ToString("C");
            lblTax.Text = (sale.Total * (decimal)sale.Tax).ToString("C");
            lblGrandTotal.Text = (sale.Total * (1 + (decimal)sale.Tax)).ToString("C");
        }

        private void btnCreateNewSale_Click(object sender, EventArgs e)
        {
            if (new SampleForms.CreateEditSaleSampleForm().ShowDialog() == DialogResult.OK)
            {
                LoadAllSales();
            }
        }
        #endregion
    }
}
