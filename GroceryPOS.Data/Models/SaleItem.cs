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

//  Models\OrderDetail.cs
//  https://github.com/NLTN/GroceryPOS.git
//  Version 0.0.1
//  Created by Nguyen Nguyen on 9/1/2017.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryPOS.Data.Models
{
    public class SaleItem
    {
        
        public int SortOrder { get; set; }
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public double Quantity { get; set; }
    }
}
