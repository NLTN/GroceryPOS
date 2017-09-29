//  ******************************************************
//  ** Orange Coast College                             **
//  ** Fall Semester 2017                               **
//  ** CS A140 - Introduction to C# .NET Programming    **
//  ** CRN          :   26637                           **
//  ** Project name :   Grocery POS                     **
//  ** Group members:   Brendan Carpio, Youlim Kim,     **
//  **                  Christian Ayala, Ben Nguyen,    **
//  **                  Nguyen Nguyen                   **
//  ******************************************************

//  Models\Product.cs
//  https://github.com/NLTN/GroceryPOS.git
//  Version 0.0.1
//  Created by Nguyen Nguyen on 9/1/2017.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GroceryPOS.Data.Models
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
