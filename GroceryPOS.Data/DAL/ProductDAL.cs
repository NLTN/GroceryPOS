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
        #region Private Fields - XML Element Name
        const string _nameXName = "name";
        const string _idXName = "id";
        const string _priceXName = "price";
        const string _imageXName = "image";
        #endregion

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
            // Get xmlDB Instance
            var xmlDBInstance = DB.xmlDB.Instance;

            // Query
            IEnumerable<Models.Product> items = from i in xmlDBInstance.doc.Root.Element("products").Elements("product")
                                                where (string)i.Element("ID") == id
                                                select new Models.Product()
                                                {
                                                    ProductID = i.Attribute(_idXName).Value,
                                                    Name = i.Element(_nameXName).Value,
                                                    Price = Double.Parse(i.Element(_priceXName).Value),
                                                    ImagePath = Settings.StorageDirectory + "/" + i.Element(_imageXName).Value
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
            // Get xmlDB Instance
            var xmlDBInstance = DB.xmlDB.Instance;

            // Query
            IEnumerable<Models.Product> items = from i in xmlDBInstance.doc.Root.Element("products").Elements("product")
                                                where i.Element(_nameXName).Value.ToLower().Contains(text.ToLower())
                                                select new Models.Product()
                                                {
                                                    ProductID = i.Attribute(_idXName).Value,
                                                    Name = i.Element(_nameXName).Value,
                                                    Price = Double.Parse(i.Element(_priceXName).Value),
                                                    ImagePath = Settings.StorageDirectory + "/" + i.Element(_imageXName).Value
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
        public void Add(string id, string name, double price, string imagePath)
        {
            // Create a new Element
            XElement product = new XElement("product");

            // Add an attribute.
            product.SetAttributeValue(_idXName, id);

            // Create and add sub elements
            product.Add(new XElement(_nameXName, name));
            product.Add(new XElement(_priceXName, price));
            product.Add(new XElement(_imageXName, imagePath));

            // Get xmlDB Instance
            var xmlDBInstance = DB.xmlDB.Instance;

            // Add the Element to the node
            xmlDBInstance.doc.Root.Element("products").Add(product);

            // Save
            xmlDBInstance.SaveChanges();
        }

        /// <summary>
        /// <para>Delete a product from database by ID</para>
        /// <para>Example: Delete("FRUIT-001");</para>
        /// </summary>
        /// <param name="id">Product ID</param>
        public void Delete(string id)
        {
            // Get xmlDB Instance
            var xmlDBInstance = DB.xmlDB.Instance;

            var items = xmlDBInstance.doc.Root.Element("products").Elements("product").Where(i => (string)i.Attribute(_idXName) == id);

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
        #endregion
    }
}