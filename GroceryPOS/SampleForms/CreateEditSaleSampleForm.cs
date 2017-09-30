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
                // Create and add a product user control to the flow layout panel
                flpProducts.Controls.Add(new ProductUserControl(i.ProductID, i.Name, i.Price, i.ImagePath));
            }
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
            LoadData();
        }
        #endregion

        private void SaleSampleForm_Load(object sender, EventArgs e)
        {
            // ListViewItem li = new ListViewItem();

            string[] row = { "sda", "3212", "321" };
            var listViewItem = new ListViewItem(row);
            lvOrder.Items.Add(listViewItem);
        }
    }
}
