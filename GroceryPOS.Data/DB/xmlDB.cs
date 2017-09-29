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
using System.Diagnostics;

namespace GroceryPOS.Data.DB
{
    // Singleton xmlDB Class
    public sealed class xmlDB
    {
        #region singleton class
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
        #endregion
        
        #region Public Fields
        public XDocument doc;
        public static string dbPath = Environment.CurrentDirectory + "/db.xml";
        #endregion
        
        #region Public Methods
        /// <summary>
        /// Load XML file
        /// </summary>
        public void Reload()
        {
            // Checking if DB file exists
            if (!DBExists()) {
                // DB file doesn't exist.
                // Create a new one.
                CreateXmlFile();
            } else {
                // Print out DB Path
                Console.WriteLine("DB path = " + dbPath);

                // Reload XML file.
                doc = XDocument.Load(dbPath);
            }
        }

        /// <summary>
        /// Save changes to XML file
        /// </summary>
        public void SaveChanges()
        {
            // Save
            doc.Save(dbPath);
        }
        #endregion

        #region Private Methods
        private bool DBExists() {
            try {
                // Debug Output
                Debug.WriteLine("DB exists = {0}", System.IO.File.Exists(dbPath));

                // Return True/False. It depends on if DB exists or not.
                return System.IO.File.Exists(dbPath);
            } catch (Exception e) {
                // Debug Output
                Debug.WriteLine(e.Message);

                // Return false because there is an error
                return false;
            }
        }
        private bool CreateXmlFile() {
            try {
                // Create an XML document and save to file
                new XDocument(
                    new XElement("root", 
                        new XElement("products"), new XElement("sales")  
                    )
                )
                .Save(dbPath);

                // Debug Output
                Debug.WriteLine("DB file has been created - {0}", dbPath);

                // Return success
                return true;
            } catch (Exception e) {
                // Print out error
                Debug.WriteLine(e.Message);

                // Return false because there is an error.
                return false;
            }
        }
        #endregion
    }
}
