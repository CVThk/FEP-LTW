using FEP.Core.Interfaces.IData;
using FEP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FEP.Data
{
    public class HelperData : IHelperData
    {
        #region design pattern
        private static HelperData instance;
        public static HelperData Instance
        {
            get { if (instance == null) instance = new HelperData(); return instance; }
        }
        private HelperData() { }
        #endregion
        public List<City> GetCities()
        {
            return ADOHelper.Instance.ExecuteReader<City>("select * from City");
        }

        public List<District> GetDistricts()
        {
            return ADOHelper.Instance.ExecuteReader<District>("select * from District");
        }

        public List<Ward> GetWards()
        {
            return ADOHelper.Instance.ExecuteReader<Ward>("select * from Ward");
        }
    }
}