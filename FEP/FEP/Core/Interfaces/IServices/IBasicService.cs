using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEP.Core.Interfaces.IServices
{
    public interface IBasicService<T>
    {
        List<T> getAll();
        //T create();
        //void update(IList<T> list, string objUpdate, T infoUpdate);
        //void detete(IList<T> list, string code);
        //T search(IList<T> list, string code);
    }
}
