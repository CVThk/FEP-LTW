using FEP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace FEP.Utility
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

        public bool CheckPhone(string phone)
        {
            if (new Regex(@"^([0-9]{10,11})$").IsMatch(phone))
                return true;
            return false;
        }
        public bool CheckUsername(string username)
        {
            if (new Regex(@"^([\d\w]{5,100})$").IsMatch(username))
                return true;
            return false;
        }
        public bool CheckInputPassword(string password)
        {
            if (new Regex(@"^([\x00-\x7F]{5,20})$").IsMatch(password))
                return true;
            return false;
        }
        public T[] Array<T>(List<T> list)
        {
            T[] cities = new T[list.Count];
            foreach (var item in list)
            {
                cities.Append(item);
            }
            return cities;
        }
        public string JsonString<T>(List<T> list)
        {
            return JsonConvert.SerializeObject(list).Replace('\'', ' ');
        }

        
    }
}