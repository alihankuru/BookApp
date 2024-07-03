using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.BusinessLayer.Abstract
{
    public interface IGenericService<T> where T : class
    {
        void TInsert(T t);
        void TUpdate(T t);
        void TDelete(int id);
        List<T> TGetList();
        T TGetById(int id);
        void TMultiUpdate(List<T> t);
    }
}
