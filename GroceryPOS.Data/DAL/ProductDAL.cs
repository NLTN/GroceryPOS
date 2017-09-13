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


//  DAL\ProductDAL.cs
//  https://github.com/NLTN/GroceryPOS.git
//  Version 0.0.1
//  Created by Nguyen Nguyen on 9/1/2017.

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
    /// Product Data Access Layer
    /// </summary>
    public class ProductDAL
    {
        #region Constructors
        public ProductDAL()
        {

        }
        #endregion

        #region Get
        /// <summary>
        /// <para>Get a product by ID</para>
        /// <para>Example: GetProductByID("FRUIT-001")</para>
        /// </summary>
        /// <param name="id">Product ID</param>
        /// <returns>A list of product, type of <Models.Product></returns>
        public IEnumerable<Models.Product> GetProductByID(string id)
        {
            // Query
            IEnumerable<Models.Product> items = from i in DB.xmlDB.doc.Element("products").Elements("product")
                                                where (string)i.Element("ID") == id
                                                select new Models.Product()
                                                {
                                                    ProductID = i.Element("ID").Value,
                                                    Name = i.Element("Name").Value,
                                                    Price = Double.Parse(i.Element("Price").Value),
                                                    ImagePath = i.Element("ImagePath").Value
                                                };
            // Debug
            Debug.WriteLine("Number of items found: {0}", items.Count());

            // Return
            return items;
        }

        /// <summary>
        /// <para>Search for products</para>
        /// <para>Example: GetProductsBySearchString("strawberry")</para>
        /// </summary>
        /// <param name="text">Text</param>
        /// <returns></returns>
        public IEnumerable<Models.Product> GetProductsBySearchString(string text)
        {
            // Query
            IEnumerable<Models.Product> items = from i in DB.xmlDB.doc.Element("products").Elements("product")
                                                where i.Element("Name").Value.ToLower().Contains(text.ToLower())
                                                select new Models.Product()
                                                {
                                                    ProductID = i.Element("ID").Value,
                                                    Name = i.Element("Name").Value,
                                                    Price = Double.Parse(i.Element("Price").Value),
                                                    ImagePath = i.Element("ImagePath").Value
                                                };

            // Debug
            Debug.WriteLine("GetProductsBySearchString: {0} results found.", items.Count());

            // Return
            return items;
        }
        #endregion

        #region Add/Update/Delete
        /// <summary>
        /// <para>Add a new product to database.</para>
        /// <para>Example: Add("P235", "Orange Juice", 25.00, "C:\Downloads\imageofOrangeJuice.jpg");</para>
        /// </summary>
        /// <param name="id">Product ID</param>
        /// <param name="name">Product name</param>
        /// <param name="price">The price</param>
        /// <param name="imgPath">Image path</param>
        public void Add(string id, string name, double price, string imgagePath)
        {
            // Create a new Element
            XElement product = new XElement("product");

            // Create and add sub elements
            product.Add(new XElement("ID", id));
            product.Add(new XElement("Name", name));
            product.Add(new XElement("Price", price));
            product.Add(new XElement("ImagePath", imgagePath));

            // Add the Element to the node
            DB.xmlDB.doc.Element("products").Add(product);

            // Save
            DB.xmlDB.SaveChanges();
        }

        /// <summary>
        /// <para>Delete a product from database by ID</para>
        /// <para>Example: Delete("FRUIT-001");</para>
        /// </summary>
        /// <param name="id">Product ID</param>
        public void Delete(string id)
        {
            var items = DB.xmlDB.doc.Element("products").Elements("product").Where(i => (string)i.Element("ID") == id);

            int count = items.Count();

            if (count != 0)
            {
                items.Remove();



                // Save
                DB.xmlDB.SaveChanges();
            }

            // Debug
            Debug.WriteLine("Number of items deleted: {0}", count);
        }
        #endregion
    }
}