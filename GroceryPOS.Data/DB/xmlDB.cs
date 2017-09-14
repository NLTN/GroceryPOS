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
    // Singleton xmlDB Class
    public sealed class xmlDB
    {
        private static volatile xmlDB instance;
        private static object syncRoot = new Object();

        private xmlDB() { Reload(); }

        public static xmlDB Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new xmlDB();
                    }
                }

                return instance;
            }
        }


        #region Public Variables
        public XDocument doc;
        public static string dbPath = Environment.CurrentDirectory + "/db.xml";
        #endregion


        #region Public Functions
        /// <summary>
        /// Load XML file
        /// </summary>
        public void Reload()
        {
            Console.WriteLine("DB path = " + dbPath);
            doc = XDocument.Load(dbPath);
            //Console.WriteLine("Number of elements: " + doc.Element("products").Elements().Count());
        }

        /// <summary>
        /// Save to XML file
        /// </summary>
        public void SaveChanges()
        {
            doc.Save(dbPath);
        }
        #endregion
    }
}
