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
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            // Create an OpenFileDialog
            OpenFileDialog dialog = new OpenFileDialog();

            // Show the dialog
            if (dialog.ShowDialog() == DialogResult.OK) // If the user selected a file
            {
                // Show the selected image in the picture box
                picBoxProductImage.Image = Image.FromFile(dialog.FileName);

                // Set the file path to the picturebox's tag because we will use it later.
                picBoxProductImage.Tag = dialog.FileName;
            }
        }
        #endregion

        #region Group Box - Product List

        #endregion

        #region Product List Methods
        void LoadData()
        {
            ProductUserControl uc = new ProductUserControl("123", "Tomato", 2.55, "32131");
            flpProducts.Controls.Add(uc);
        }
        #endregion
    }
}
