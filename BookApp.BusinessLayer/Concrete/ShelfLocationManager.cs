using AutoMapper;
using BookApp.BusinessLayer.Abstract;
using BookApp.BusinessLayer.Serilog;
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
        private readonly IAppLogger<ShelfLocation> _logger;
        

        public ShelfLocationManager(IShelfLocationDal shelfLocationDal, IUowDal uowDal, IAppLogger<ShelfLocation> logger)
        {
            _shelfLocationDal = shelfLocationDal;
            _uowDal = uowDal;
            _logger = logger;
        }

        public void TDelete(int id)
        {
            try
            {
                _shelfLocationDal.Delete(id);
                _uowDal.Save();
                _logger.LogInformation($"ShelfLocation with ID {id} deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting ShelfLocation with ID {id}: {ex.Message}");
                throw;
            }
        }

        public ShelfLocation TGetById(int id)
        {
            try
            {
                var shelfLocation = _shelfLocationDal.GetById(id);
                _logger.LogInformation($"ShelfLocation with ID {id} retrieved successfully.");
                return shelfLocation;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving ShelfLocation with ID {id}: {ex.Message}");
                throw;
            }
        }

        public List<ShelfLocation> TGetList()
        {
            try
            {
                var shelfLocations = _shelfLocationDal.GetList();
                _logger.LogInformation("ShelfLocation list retrieved successfully.");
                return shelfLocations;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving ShelfLocation list: {ex.Message}");
                throw;
            }
        }

        public void TInsert(ShelfLocation shelfLocation)
        {
            try
            {
                _shelfLocationDal.Insert(shelfLocation);
                _uowDal.Save();
                _logger.LogInformation($"ShelfLocation inserted successfully. ID: {shelfLocation.ShelfLocationId}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error inserting ShelfLocation: {ex.Message}");
                throw;
            }
        }

        public void TMultiUpdate(List<ShelfLocation> shelfLocation)
        {
            try
            {
                _shelfLocationDal.MultiUpdate(shelfLocation);
                _uowDal.Save();
                _logger.LogInformation("ShelfLocation list updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating ShelfLocation list: {ex.Message}");
                throw;
            }
        }

        public void TUpdate(ShelfLocation shelfLocation)
        {
            try
            {
                _shelfLocationDal.Update(shelfLocation);
                _uowDal.Save();
                _logger.LogInformation($"ShelfLocation with ID {shelfLocation.ShelfLocationId} updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating ShelfLocation with ID {shelfLocation.ShelfLocationId}: {ex.Message}");
                throw;
            }
        }

    }
}
