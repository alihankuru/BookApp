using BookApp.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.DataAccessLayer.Abstract
{
    public interface IBookDal:IGenericDal<Book>
    {
    }
}
