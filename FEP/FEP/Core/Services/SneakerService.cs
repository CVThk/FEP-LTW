using FEP.Core.Interfaces.IData;
using FEP.Core.Interfaces.IServices;
using FEP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FEP.Core.Services
{
    public class SneakerService : ISneakerService
    {
        private ISneakerData _data;
        public SneakerService(ISneakerData sneakerData)
        {
            this._data = sneakerData;
        }
        public List<Sneaker> getAll()
        {
            return _data.GetSneakers();
        }

       
    }
}