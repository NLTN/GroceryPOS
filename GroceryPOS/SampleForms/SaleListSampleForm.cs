﻿using System;
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
        #region Fields
        string SelectedSaleID = "";
        #endregion

        #region Constructor
        public SaleListSampleForm()
        {
            InitializeComponent();
        }
        #endregion      

        #region Form Event Handlers
        private void SaleListSampleForm_Load(object sender, EventArgs e)
        {
            LoadData();

            // Register a Database Event Handler
            Data.DB.xmlDB.Instance.DataChanged += DataChanged;
        }

        private void SaleListSampleForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Unregister a Database Event Handler
            Data.DB.xmlDB.Instance.DataChanged -= DataChanged;
        }
        #endregion

        #region Database Event Handler
        private void DataChanged(object sender, EventArgs e)
        {
            LoadData();
        }
        #endregion      

        #region Other Event Handlers
        private void lvSales_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((ListView)sender).SelectedItems.Count == 0)
            {
                return;
            }

            // Set selected sale ID.
            SelectedSaleID = ((ListView)sender).SelectedItems[0].Text;

            // Remove all items in lvOrder
            lvOrder.Items.Clear();

            var sale = SaleBLL.GetSaleByID(SelectedSaleID);
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
                LoadData();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Show Confirmation Box
            if (MessageBox.Show(string.Format("Are you sure to delete '{0}'?", SelectedSaleID), "Confirmation", MessageBoxButtons.YesNoCancel) == DialogResult.Yes)
            {
                // Delete the sale record from DB.
                SaleBLL.Delete(SelectedSaleID);
            }
        }
        #endregion

        #region Methods
        private void LoadData()
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
    }
}
