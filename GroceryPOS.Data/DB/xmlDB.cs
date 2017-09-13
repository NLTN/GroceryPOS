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

//  DB\xmlDB.cs
//  https://github.com/NLTN/GroceryPOS.git
//  Version 0.0.1
//  Created by Nguyen Nguyen on 9/1/2017.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GroceryPOS.Data.DB
{
    public class xmlDB
    {
        #region Constructor - Singleton
        private static xmlDB _instance;

        private xmlDB() { loadDatabase(); }

        public static xmlDB Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new xmlDB();
                }
                return _instance;
            }
        }
        #endregion

        #region Public Variables
        public static XDocument doc;
        #endregion

        #region Private Variables
        /// <summary>
        /// Database Path
        /// </summary>
        public static string dbPath = Environment.CurrentDirectory + "\\db.xml";
        #endregion

        #region Private Functions
        /// <summary>
        /// Load XML file
        /// </summary>
        public static void loadDatabase()
        {
            Console.WriteLine("DB path = " + dbPath);
            doc = XDocument.Load(dbPath);
            //Console.WriteLine("Number of elements: " + doc.Element("products").Elements().Count());
        }

        /// <summary>
        /// Save to XML file
        /// </summary>
        public static void SaveChanges()
        {
            doc.Save(dbPath);
        }
        #endregion
    }
}
