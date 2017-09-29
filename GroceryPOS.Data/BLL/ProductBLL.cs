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

//  BLL\ProductBLL.cs
//  https://github.com/NLTN/GroceryPOS.git
//  Version 0.0.1
//  Created by Nguyen Nguyen on 9/1/2017.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Diagnostics;
using System.IO;

namespace GroceryPOS.Data.BLL
{
    /// <summary>
    /// Product Business Logic Layer
    /// </summary>
    public class ProductBLL
    {
        #region Private Fields
        private static string StorageDirectory = System.Environment.CurrentDirectory + "/Storage";
        #endregion

        #region Get
        /// <summary>
        /// <para>Get a product by ID</para>
        /// <para>Example: GetProductByID("FRUIT-001")</para>
        /// </summary>
        /// <param name="id">Product ID</param>
        /// <returns>A list of product, type of <Models.Product></returns>
        public static IEnumerable<Models.Product> GetProductByID(string ID)
        {
            try
            {
                return new DAL.ProductDAL().GetProductByID(ID);
            }
            catch (Exception e)
            {
                // Debug
                Debug.WriteLine("{0} Exception caught.", e);

                return new Models.Product[0];
            }
        }

        /// <summary>
        /// <para>Search for products</para>
        /// <para>Example: GetProductsBySearchString("strawberry")</para>
        /// </summary>
        /// <param name="text">Text</param>
        /// <returns></returns>
        public static IEnumerable<Models.Product> GetProductsBySearchString(string text)
        {
            try
            {
                return new DAL.ProductDAL().GetProductsBySearchString(text);
            }
            catch (Exception e)
            {
                // Debug
                Debug.WriteLine("{0} Exception caught.", e);

                return new Models.Product[0];
            }
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
        public static void Add(string id, string name, double price, string imagePath)
        {
            try
            {
                // The filename to be stored in StorageDirectory
                string _filenameToBeStored = "";

                // Auto generate an ProductID if the id is empty
                if (id == string.Empty)
                {
                    id = GenerateID();
                }

                // If the user wants to add an image
                if (imagePath != string.Empty)
                {
                    // Check if the file exists
                    if (File.Exists(imagePath))
                    {
                        // Create an StorageDirectory if needed.
                        if (!Directory.Exists(StorageDirectory))
                        {
                            Directory.CreateDirectory(StorageDirectory);
                        }

                        // Check if there is a file with the same filename already exists in StorageDirectory                        
                        if (File.Exists(StorageDirectory + "/" + Path.GetFileName(imagePath))) {
                            // If so, add a random string to the end of the filename.
                            _filenameToBeStored = Path.GetFileNameWithoutExtension(imagePath) + GenerateID() + 
                                Path.GetExtension(imagePath);                            
                        } else {
                            // Just use the same filename
                            _filenameToBeStored = Path.GetFileName(imagePath);
                        }

                        // Copy the file user selected to StorageDirectory
                        File.Copy(imagePath, StorageDirectory + "/" + _filenameToBeStored);
                    }
                }

                // Execute ProductDAL.Add
                new DAL.ProductDAL().Add(id, name, price, _filenameToBeStored);
            }
            catch (Exception e)
            {
                // Debug Output
                Debug.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// <para>Delete a product from database by ID</para>
        /// <para>Example: Delete("FRUIT-001");</para>
        /// </summary>
        /// <param name="id">Product ID</param>
        public static void Delete(string id)
        {
            new DAL.ProductDAL().Delete(id);
        }
        #endregion

        #region Others
        public static string GenerateID()
        {
            return DateTime.UtcNow.ToBinary().ToString();
        }
        #endregion
    }
}
