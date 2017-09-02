// CS 140 
// 2017
// Nguyen Nguyen
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GroceryPOS.DB
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
        private static string dbPath = Environment.CurrentDirectory + "\\db.xml";
        #endregion

        #region Private Functions
        /// <summary>
        /// Load XML file
        /// </summary>
        public static void loadDatabase()
        {
            Console.WriteLine("DB path = " + dbPath);
            doc = XDocument.Load(dbPath);
            Console.WriteLine("Number of elements: " + doc.Element("products").Elements().Count());
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
