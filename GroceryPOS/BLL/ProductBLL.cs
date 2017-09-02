﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Diagnostics;

namespace GroceryPOS.BLL
{
    public class ProductBLL
    {
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
        public static void Add(string id, string name, double price, string imgagePath)
        {
            new DAL.ProductDAL().Add(id, name, price, imgagePath);
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
    }
}
