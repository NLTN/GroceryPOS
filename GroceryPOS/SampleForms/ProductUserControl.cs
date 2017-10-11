using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using GroceryPOS.Data.BLL;

namespace GroceryPOS.SampleForms
{
    public partial class ProductUserControl : UserControl
    {
        #region Properties
        // The ID property
        string _ID = "";        
        public string ID
        {
            get
            {
                return _ID;
            }
            set
            {
                _ID = value;
            }
        }

        public string NameOfProduct
        {
            get
            {
                return lblName.Text;
            }
            set
            {
                lblName.Text = value;
            }
        }

        double _Price = -1;
        public double Price
        {
            get
            {
                return _Price;
            }
            set
            {
                _Price = value;

                if (_Price <0)
                {
                    lblPrice.Text = "Not Available";
                } else
                {
                    lblPrice.Text = _Price.ToString("C");
                }
                
            }
        }

        string _ImagePath = string.Empty;
        public string ImagePath
        {
            get
            {
                return _ImagePath;
            }
            set
            {
                _ImagePath = value;

                if (_ImagePath != string.Empty)
                {
                    if (File.Exists(_ImagePath)) {
                        pictureBox1.Image = Image.FromFile(_ImagePath);
                    }                    
                }
            }
        }

        // Show/Hide Delete Button
        bool _ShowDeleteButton = true;
        public bool ShowDeleteButton
        {
            get
            {
                return _ShowDeleteButton;
            }
            set
            {
                btnDelete.Visible = _ShowDeleteButton = value;
            }
        }
        #endregion

        public ProductUserControl()
        {
            InitializeComponent();
        }

        public ProductUserControl(string id, string name, double price, string imagePath)
        {
            InitializeComponent();

            ID = id;
            NameOfProduct = name;
            Price = price;
            ImagePath = imagePath;

            btnDelete.Visible = ShowDeleteButton;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Show Confirmation Box
            if (MessageBox.Show(string.Format("Are you sure to delete the product '{0}'?", ID), "Confirmation", MessageBoxButtons.YesNoCancel) == DialogResult.Yes)
            {
                // Delete the product record from DB
                ProductBLL.Delete(ID);

                // Reload the list view
            }
            
        }
    }
}
