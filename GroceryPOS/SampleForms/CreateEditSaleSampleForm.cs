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


        #region Product List Methods
        void LoadData()
        {
            // First, remove all controls in the flow layout panel.
            flpProducts.Controls.Clear();

            // Next, get all the products
            foreach (var i in ProductBLL.GetProductsBySearchString(txtSearch.Text))
            {
                // Create a product user control
                ProductUserControl uc = new ProductUserControl(i.ProductID, i.Name, i.Price, i.ImagePath);

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
            // SUM
            _Total += quantity * price;

            // Display the total cost
            ShowSummary();

            // Create a ListViewItem, and add it to the ListView
            lvOrder.Items.Add(new ListViewItem(new string[] { name, quantity.ToString(), price.ToString("C"), (quantity * price).ToString("C") }));

            //// If you don't understand the code above, please take a look at this:
            //// Create a ListViewItem
            //ListViewItem li = new ListViewItem();

            //// Calculate sub total
            //double subTotal = quantity * price;

            //// Add some text values to ListViewItem
            //li.SubItems.Add(name);
            //li.SubItems.Add(quantity.ToString());
            //li.SubItems.Add(price.ToString("C"));
            //li.SubItems.Add(subTotal.ToString("C"));

            //// Add the ListViewItem to ListView
            //lvOrder.Items.Add(li);
        }

        private void ShowSummary()
        {
            lblTotal.Text = _Total.ToString("C");
            lblTax.Text = (_Total * _Tax).ToString("C");
            lblGrandTotal.Text = (_Total * (1 + _Tax)).ToString("C");
        }
        #endregion
        

        #region Group Box - Product List
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
            AddProductToOrder(puc.ID, puc.NameOfProduct, 1, puc.Price);
        }
        #endregion

    }
}