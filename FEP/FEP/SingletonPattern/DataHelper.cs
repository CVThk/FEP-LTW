using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FEP.SingletonPattern
{
    public class DataHelper
    {
        #region Singleton Patter
        private static DataHelper instance;
        public static DataHelper Instance
        {
            get { if (instance == null) instance = new DataHelper(); return instance; }
            private set { instance = value; }
        }
        private DataHelper() { }
        #endregion
        #region ConnectionString
        public string getConnectionString(string databaseName)
        {
            return @"Data Source=./SQLEXPRESS;Initial Catalog=" + databaseName + ";Integrated Security=True";
        }
        public string getConnectionString(string serverName, string databaseName, string id, string pass)
        {
            return @"Data Source=" + serverName + ";Initial Catalog=" + databaseName + ";User ID=" + id + ";Password=" + pass;
        }

        public string getConnectionString(string serverName, string databaseName)
        {
            return "Data Source=" + serverName + ";Initial Catalog=" + databaseName + ";Integrated Security=True";
        }
        #endregion
    }
}