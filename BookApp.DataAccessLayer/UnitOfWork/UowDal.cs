using BookApp.DataAccessLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.DataAccessLayer.UnitOfWork
{
    public class UowDal : IUowDal
    {
        private readonly BookContext _context;

        public UowDal(BookContext context)
        {
            _context = context;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
