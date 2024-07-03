using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.DataAccessLayer.Abstract
{
    public interface IGenericDal<T> where T : class
    {
        void Insert(T t);
        void Update(T t);
        void Delete(int id);
        List<T> GetList();
        T GetById(int id);
        void MultiUpdate(List<T> t);

    }
}
