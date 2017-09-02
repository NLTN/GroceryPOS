using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GroceryPOS.Models
{
    [XmlRoot("product")]
    public class Product
    {
        [XmlElement("ID")]
        public string ProductID { get; set; }
        [XmlElement("Name")]
        public string Name { get; set; }
        [XmlElement("Price")]
        public double Price { get; set; }
        [XmlElement("ImagePath")]
        public string ImagePath { get; set; }

    }
}
