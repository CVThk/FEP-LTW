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
        public List<string> ListLink(string linkPictureString)
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

        public string ChuanHoaChuoi(string str)
        {
            while(str.Contains("  "))
            {
                str = str.Replace("  ", " ");
            }    
            string[] s = str.Trim().ToLower().Split(' ');
            str = "";
            for(int i = 0; i < s.Length; i++)
            {
                string first = s[i].Substring(0, 1);
                string another = s[i].Substring(1, s[i].Length - 1);
                str += first.ToUpper() + another + " ";
            }
            str = str.Trim();
            return str;
        }
        public string CreateIDSneaker(string str)
        {
            str = str.Replace('(', ' ');
            str = str.Replace(')', ' ');
            str = ChuanHoaChuoi(str);
            str = ChuyenKhongDau(str);
            string[] s = str.Split(' ');
            string result = "";
            for(int i = 0; i < s.Length; i++)
            {
                int z = 0;
                if (s[i].Substring(0, 1) == "(")
                    z = 1;
                result += s[i].Substring(z, 1).ToUpper();
            }
            return result;
        }

        public string ChuyenKhongDau(string str)
        {
            string[] VietnameseSigns = new string[]
            {
                "aAeEoOuUiIdDyY",
                "áàạảãâấầậẩẫăắằặẳẵ",
                "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
                "éèẹẻẽêếềệểễ",
                "ÉÈẸẺẼÊẾỀỆỂỄ",
                "óòọỏõôốồộổỗơớờợởỡ",
                "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
                "úùụủũưứừựửữ",
                "ÚÙỤỦŨƯỨỪỰỬỮ",
                "íìịỉĩ",
                "ÍÌỊỈĨ",
                "đ",
                "Đ",
                "ýỳỵỷỹ",
                "ÝỲỴỶỸ"
            };

            for (int i = 1; i < VietnameseSigns.Length; i++)
            {
                for (int j = 0; j < VietnameseSigns[i].Length; j++)
                    str = str.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);
            }
            return str;
        }
    }
}