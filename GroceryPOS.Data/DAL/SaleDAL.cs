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


//  DAL\SaleDAL.cs
//  https://github.com/NLTN/GroceryPOS.git
//  Version 0.0.1
//  Created by Nguyen Nguyen on 9/27/2017.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Data;
using System.Diagnostics;

namespace GroceryPOS.Data.DAL
{
    /// <summary>
    /// Order Data Access Layer
    /// </summary>
    public class SaleDAL
    {
        #region Private Fields - XML Element Name
        const string _saleRootXName = "sales";
        const string _saleNodeXName = "sale";
        const string _saleItemsXName = "items";

        const string _itemXName = "item";
        const string _productIDXName = "productID";
        const string _idXName = "id";
        const string _datetimeXName = "datetime";
        const string _taxXName = "Tax";
        const string _priceXName = "price";
        const string _quantityXName = "quantity";
        const string _sortorderXName = "sortorder";
        #endregion

        #region Add/Edit/Delete Methods
        public void Delete(string id)
        {
            // Get xmlDB Instance
            var xmlDBInstance = DB.xmlDB.Instance;

            var items = xmlDBInstance.doc.Root.Element(_saleRootXName).Elements(_saleNodeXName).Where(i => (string)i.Attribute(_idXName) == id);

            // counter (Just for debugging)
            int count = items.Count();

            // If has items
            if (count != 0)
            {
                // Remove the items
                items.Remove();

                // Save
                xmlDBInstance.SaveChanges();
            }

            // Debug
            Debug.WriteLine("Number of items deleted: {0}", count);
        }
        public bool Add(Models.Sale newSale)
        {
            try
            {
                // Create a new Element
                XElement element = new XElement(_saleNodeXName);

                // Set SaleID attribute.
                element.SetAttributeValue(_idXName, newSale.SaleID);

                // Create a datetime element.
                element.Add(new XElement(_datetimeXName, newSale.Datetime.ToString()));

                // Create a Tax element
                element.Add(new XElement(_taxXName, newSale.Tax));

                // Create a SaleItems Node
                XElement items = new XElement(_saleItemsXName);

                foreach (var i in newSale.SaleItems)
                {
                    // Create an item element
                    XElement item = new XElement(_itemXName);

                    // Product ID
                    item.Add(new XElement(_productIDXName, i.ProductID));

                    // Sort Order
                    item.Add(new XElement(_sortorderXName, i.SortOrder));

                    // Quantity
                    item.Add(new XElement(_quantityXName, i.Quantity));

                    // Price
                    item.Add(new XElement(_priceXName, i.Price));

                    // Add the item to items
                    items.Add(item);
                }

                // Add Items to Sale Element
                element.Add(items);

                // Get xmlDB Instance
                var xmlDBInstance = DB.xmlDB.Instance;

                // Add the Sale Element to the sales root.
                xmlDBInstance.doc.Root.Element(_saleRootXName).Add(element);

                // Save
                xmlDBInstance.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                // Debug. Write error message
                Debug.WriteLine(e.Message);

                // Return False because there is an Error
                return false;
            }

        }
        #endregion

        #region Get
        public IEnumerable<Models.Sale> GetAllSales()
        {
            // Get xmlDB Instance
            var xmlDBInstance = DB.xmlDB.Instance;
            
            // Query
            IEnumerable<Models.Sale> sales = from i in xmlDBInstance.doc.Root.Element(_saleRootXName).Elements(_saleNodeXName)
                                             select new Models.Sale()
                                             {
                                                 SaleID = i.Attribute(_idXName).Value,
                                                 Datetime = DateTime.Parse(i.Element(_datetimeXName).Value),
                                                 Tax = double.Parse(i.Element(_taxXName).Value),
                                                 SaleItems = from item in i.Element(_saleItemsXName).Elements(_itemXName)
                                                             select new Models.SaleItem()
                                                             {
                                                                 ProductID = item.Element(_productIDXName).Value,
                                                                 SortOrder = int.Parse(item.Element(_sortorderXName).Value),
                                                                 Quantity = double.Parse(item.Element(_quantityXName).Value),
                                                                 Price = decimal.Parse(item.Element(_priceXName).Value),
                                                                 ProductName = BLL.ProductBLL.GetProductByID(item.Element(_productIDXName).Value).Name
                                                             }
                                                };

            return sales;
        }

        #endregion
    }
}
