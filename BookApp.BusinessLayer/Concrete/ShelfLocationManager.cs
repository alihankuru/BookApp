using BookApp.BusinessLayer.Abstract;
using BookApp.DataAccessLayer.Abstract;
using BookApp.DataAccessLayer.UnitOfWork;
using BookApp.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.BusinessLayer.Concrete
{
    public class ShelfLocationManager : IShelfLocationService
    {
        private readonly IShelfLocationDal _shelfLocationDal;
        private readonly IUowDal _uowDal;

        public ShelfLocationManager(IShelfLocationDal shelfLocationDal, IUowDal uowDal)
        {
            _shelfLocationDal = shelfLocationDal;
            _uowDal = uowDal;
        }

        public void TDelete(int id)
        {
            _shelfLocationDal.Delete(id);
            _uowDal.Save();
        }

        public ShelfLocation TGetById(int id)
        {
            return _shelfLocationDal.GetById(id);
        }

        public List<ShelfLocation> TGetList()
        {
            return _shelfLocationDal.GetList();
        }

        public void TInsert(ShelfLocation t)
        {
            _shelfLocationDal.Insert(t);
            _uowDal.Save();
        }

        public void TMultiUpdate(List<ShelfLocation> t)
        {
            _shelfLocationDal.MultiUpdate(t);
            _uowDal.Save();
        }

        public void TUpdate(ShelfLocation t)
        {
            _shelfLocationDal.Update(t);
            _uowDal.Save();
        }
    }
}
