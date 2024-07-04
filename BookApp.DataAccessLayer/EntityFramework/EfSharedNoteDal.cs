using BookApp.DataAccessLayer.Abstract;
using BookApp.DataAccessLayer.Context;
using BookApp.DataAccessLayer.Repositories;
using BookApp.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.DataAccessLayer.EntityFramework
{
    public class EfSharedNoteDal : GenericRepository<SharedNote>, ISharedNoteDal
    {
        public EfSharedNoteDal(BookContext context) : base(context)
        {
        }
    }
}
