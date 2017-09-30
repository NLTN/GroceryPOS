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


//  DAL\SaleBLL.cs
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

namespace GroceryPOS.Data.BLL {
    /// <summary>
    /// Order Data Access Layer
    /// </summary>
    public class SaleBLL
    {
        #region Add/Edit/Delete Methods
        public static void Delete(string id) {
            new DAL.SaleDAL().Delete(id);
        }

        public static bool Add(List<Models.SaleItem> saleItems, double Tax) {
            try {
                // Create a new sale object
                Models.Sale newSale = new Models.Sale();
                
                // Set Sale ID & Datetime
                newSale.SaleID = GenerateID();
                newSale.Datetime = DateTime.Now;
                newSale.Tax = Tax;

                // Set sale items
                newSale.SaleItems = saleItems;

                // Create SaleDAL object and call the Add function. 
                // Then, Return true if success
                return new DAL.SaleDAL().Add(newSale);
            } catch (Exception e) {
                // Debug. Print out error message.
                Debug.WriteLine(e.Message);

                // Return False because there was an error.
                return false; 
            }
        }
        #endregion
        #region Get Methods
        public static IEnumerable<Models.Sale> GetAllSales()
        {
            return new DAL.SaleDAL().GetAllSales();
        }

        public static Models.Sale GetSaleByID(string id)
        {
            try
            {
                return new DAL.SaleDAL().GetSaleByID(id);
            }
            catch (Exception e)
            {
                // Debug
                Debug.WriteLine("{0} Exception caught.", e);

                return new Models.Sale();
            }
        }

        public static IEnumerable<Models.Sale> GetSalesByDatetime(DateTime from, DateTime to) {
            return new Models.Sale[1];
        }
        #endregion

        #region Others
        public static string GenerateID() {
            return DateTime.UtcNow.ToBinary().ToString();
        }
        #endregion        
    }
}