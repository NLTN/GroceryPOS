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

        public string Name
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
        #endregion

        public ProductUserControl()
        {
            InitializeComponent();
        }

        public ProductUserControl(string id, string name, double price, string imagePath)
        {
            InitializeComponent();

            ID = id;
            Name = name;
            Price = price;
            ImagePath = imagePath;
        }
    }
}
