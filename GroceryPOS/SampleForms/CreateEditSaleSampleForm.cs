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
    public partial class CreateEditSaleSampleForm : Form
    {
        #region Private Fields
        const double _Tax = 0.09;
        double _Total = 0;
        #endregion

        #region Constructor
        public CreateEditSaleSampleForm()
        {
            InitializeComponent();
        }
        #endregion

        #region Form Event Handlers
        private void CreateEditSaleSampleForm_Load(object sender, EventArgs e)
        {
            LoadData();

            ShowSummary();

            // Register a Database Event Handler
            Data.DB.xmlDB.Instance.DataChanged += DataChanged;
        }

        private void CreateEditSaleSampleForm_FormClosing(object sender, FormClosingEventArgs e)
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

        #region Group Box 1 - Product List
        void LoadData()
        {
            // First, remove all controls in the flow layout panel.
            flpProducts.Controls.Clear();

            // Next, get all the products
            foreach (var i in ProductBLL.GetProductsBySearchString(txtSearch.Text))
            {
                // Create a product user control
                ProductUserControl uc = new ProductUserControl(i.ProductID, i.Name, i.Price, i.ImagePath);

                // Hide the delete button.
                uc.ShowDeleteButton = false;

                // Add an Event handler for each controls in the product user control.
                // This is a workaround. I will find a better way to deal with this problem later.
                foreach (Control c in uc.Controls)
                {
                    c.Click += ProductUserControl_Click;
                }

                // Add the user control to the Flow Layout Panel
                flpProducts.Controls.Add(uc);
            }
        }

        private void AddProductToOrder(string productID, string name, double quantity, double price)
        {
            // Create a ListViewItem
            ListViewItem li = new ListViewItem(name);

            // Save the product ID in the tag. We will use it later.
            li.Tag = productID;

            // Calculate sub total
            double subTotal = quantity * price;

            // Increase _Total
            _Total += subTotal;

            // Add some text values to ListViewItem
            li.SubItems.Add(quantity.ToString());
            li.SubItems.Add(price.ToString("N2"));
            li.SubItems.Add(subTotal.ToString("C"));

            // Add the ListViewItem to ListView
            lvOrder.Items.Add(li);

            // Display the total cost
            ShowSummary();
        }

        private void ShowSummary()
        {
            lblTotal.Text = _Total.ToString("C");
            lblTax.Text = (_Total * _Tax).ToString("C");
            lblGrandTotal.Text = (_Total * (1 + _Tax)).ToString("C");
        }

        // Search products when enter key pressed
        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Detect Enter Key pressed
            if (e.KeyChar == (char)13) // The number 13 means Keys.Enter
            {
                LoadData();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            // Load data from DB
            LoadData();
        }

        private void ProductUserControl_Click(object sender, EventArgs e)
        {
            // Get the ProductUserControl
            var puc = (ProductUserControl)((Control)sender).Parent;

            // Add product to the order
            // AddProductToOrder(puc.ID, puc.NameOfProduct, 1, puc.Price);

            // Create a Form instance
            var popup = new ProductInformationPopupForm(puc.ID, puc.NameOfProduct, puc.Price, puc.ImagePath);

            // Set the starting position of the form
            popup.StartPosition = FormStartPosition.CenterParent;

            // Detect if the form closes
            if (popup.ShowDialog() == DialogResult.OK)
            {
                // Add product to the order
                AddProductToOrder(puc.ID, puc.NameOfProduct, popup.Quantity, puc.Price);
            }
        }

        #endregion

        #region Group Box 2 - Order List
        private void btnReset_Click(object sender, EventArgs e)
        {
            // Remove all items in the ListView
            lvOrder.Items.Clear();

            // Reset Total = 0
            _Total = 0;

            // Update the summary
            ShowSummary();
        }

        private void btnSaveAndClose_Click(object sender, EventArgs e)
        {

            if (lvOrder.Items.Count == 0)
            {
                // If there is nothing in the list, the program will do nothing.
                return;
            }

            // Create a List of _saleItems
            var ListOfSaleItems = new List<Data.Models.SaleItem>();

            // Get all items in the ListView
            foreach (ListViewItem li in lvOrder.Items)
            {
                // Create a SaleItem.
                var saleItem = new Data.Models.SaleItem();

                // Set values
                saleItem.ProductID = li.Tag.ToString();
                saleItem.SortOrder = li.Index;
                saleItem.Quantity = double.Parse(li.SubItems[1].Text.ToString());
                saleItem.Price = decimal.Parse(li.SubItems[2].Text.ToString());

                // Add the sale item to the ListOfSaleItems
                ListOfSaleItems.Add(saleItem);                
            }

            // Save to Database
            SaleBLL.Add(ListOfSaleItems, _Tax);

            // Reset the form
            //btnReset.PerformClick();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        #endregion
        
    }
}