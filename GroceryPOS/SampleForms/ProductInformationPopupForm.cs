using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GroceryPOS.SampleForms
{
    public partial class ProductInformationPopupForm : Form
    {
        #region Properties
        double _Quantity = 1;
        public double Quantity
        {
            get
            {
                return _Quantity;
            }
            set
            {
                _Quantity = value;
                txtQuantity.Text = value.ToString();
            }
        }
        #endregion

        #region Constructor
        public ProductInformationPopupForm(string productID, string name, double price, string imagePath)
        {
            InitializeComponent();

            lblProductName.Text = name;
            lblPrice.Text = price.ToString("C");
            picProductImage.Image = Image.FromFile(imagePath);
        }
        #endregion

        #region Event Handlers
        private void btnIncreaseQuantity_Click(object sender, EventArgs e)
        {
            // Increase the Quantity by 1
            Quantity++;
        }

        private void btnDecreaseQuantity_Click(object sender, EventArgs e)
        {
            // Decrease the Quantity by 1
            Quantity--;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        #endregion
    }
}
