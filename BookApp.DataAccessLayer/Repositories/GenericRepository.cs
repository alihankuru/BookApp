﻿using BookApp.DataAccessLayer.Abstract;
using BookApp.DataAccessLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.DataAccessLayer.Repositories
{
    public class GenericRepository<T> : IGenericDal<T> where T : class
    {
        private readonly BookContext _context;

        public GenericRepository(BookContext context)
        {
            _context = context;
        }

        public void Delete(int id)
        {
            var value = _context.Set<T>().Find(id);
            _context.Set<T>().Remove(value);

        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public List<T> GetList()
        {
            return _context.Set<T>().ToList();
        }

        public void Insert(T t)
        {
            _context.Add(t);    
        }

        public void MultiUpdate(List<T> t)
        {
            _context.UpdateRange(t);
        }

        public void Update(T t)
        {
            _context.Update(t);
        }
    }
}
