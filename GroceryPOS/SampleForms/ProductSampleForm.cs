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

namespace GroceryPOS.SampleForms
{
    public partial class ProductSampleForm : Form
    {
        #region Constructor
        public ProductSampleForm()
        {
            InitializeComponent();
        }
        #endregion

        #region Form Events
        private void ProductSampleForm_Load(object sender, EventArgs e)
        {
            // Load Products from DB
            LoadData();
        }
        #endregion

        #region Group Box - Add a Product 
        private void btnSave_Click(object sender, EventArgs e)
        {
            // Save to database.
            // ProductBLL.Add(productID, name, price, image path);
            ProductBLL.Add(txtProductID.Text, txtName.Text, (double)numPrice.Value, picBoxProductImage.Tag.ToString());

            // Clear the search text
            txtSearch.Text = string.Empty;

            // Load data from DB
            LoadData();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            // Create an OpenFileDialog
            OpenFileDialog dialog = new OpenFileDialog();

            // Show the dialog
            if (dialog.ShowDialog() == DialogResult.OK) // If the user selected a file
            {
                try
                {
                    // Show the selected image in the picture box
                    picBoxProductImage.Image = Image.FromFile(dialog.FileName);

                    // Set the file path to the picturebox's tag because we will use it later.
                    picBoxProductImage.Tag = dialog.FileName;
                } catch (Exception error)
                {
                    Console.WriteLine(error.Message);
                }
                 
            }
        }
        #endregion

        #region Group Box - Product List

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

    }
}
