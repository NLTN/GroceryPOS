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

        #region Add a Product Group
        private void btnSave_Click(object sender, EventArgs e)
        {
            // Save to database.
            ProductBLL.Add(txtProductID.Text, txtName.Text, (double)numPrice.Value, picBoxProductImage.Tag.ToString());
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            // Create an OpenFileDialog
            OpenFileDialog dialog = new OpenFileDialog();

            // Show the dialog
            if (dialog.ShowDialog() == DialogResult.OK) // If user selected a file
            {
                // Show the image that user selected in the picture box
                picBoxProductImage.Image = Image.FromFile(dialog.FileName);

                // Set the file path to the picturebox's tag for using later.
                picBoxProductImage.Tag = dialog.FileName;
            }
        }
        #endregion
    }
}
