using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FEP.SingletonPattern
{
    public class Utilities
    {
        #region Singleton Pattern
        private static Utilities instance;
        public static Utilities Instance
        {
            get { if (instance == null) instance = new Utilities(); return instance; }
            private set { instance = value; }
        }
        private Utilities() { }
        #endregion
        public List<string> ListLink(string linkPictureString) // phân list
        {
            List<string> link = new List<string>();
            link = linkPictureString.Split(';').ToList();
            return link;
        }


    }
}