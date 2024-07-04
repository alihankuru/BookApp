using BookApp.BusinessLayer.Abstract;
using BookApp.BusinessLayer.Serilog;
using BookApp.DataAccessLayer.Abstract;
using BookApp.DataAccessLayer.EntityFramework;
using BookApp.DataAccessLayer.UnitOfWork;
using BookApp.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.BusinessLayer.Concrete
{
    public class OrderManager : IOrderService
    {
        private readonly IOrderDal _orderDal;
        private readonly IUowDal _uowDal;
        private readonly IAppLogger<Order> _logger;

        public OrderManager(IOrderDal orderDal, IUowDal uowDal, IAppLogger<Order> logger)
        {
            _orderDal = orderDal;
            _uowDal = uowDal;
            _logger = logger;
        }

        public void TDelete(int id)
        {
            try
            {
                _orderDal.Delete(id);
                _uowDal.Save();
                _logger.LogInformation($"order with ID {id} deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting book with ID {id}: {ex.Message}");
                throw;
            }
        }

        public Order TGetById(int id)
        {
            try
            {
                var order = _orderDal.GetById(id);
                _logger.LogInformation($"order with ID {id} retrieved successfully.");
                return order;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Order retrieving book with ID {id}: {ex.Message}");
                throw;
            }
        }

        public List<Order> TGetList()
        {
            try
            {
                var order = _orderDal.GetList();
                _logger.LogInformation("order list retrieved successfully.");
                return order;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving book list: {ex.Message}");
                throw;
            }
        }

        public void TInsert(Order order)
        {
            try
            {
                _orderDal.Insert(order);
                _uowDal.Save();
                _logger.LogInformation($"book inserted successfully. ID: {order.OrderId}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error inserting book: {ex.Message}");
                throw;
            }
        }

        public void TMultiUpdate(List<Order> order)
        {
            try
            {
                _orderDal.MultiUpdate(order);
                _uowDal.Save();
                _logger.LogInformation("order list updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating book list: {ex.Message}");
                throw;
            }
        }

        public void TUpdate(Order order)
        {
            try
            {
                _orderDal.Update(order);
                _uowDal.Save();
                _logger.LogInformation($"book with ID {order.OrderId} updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating book with ID {order.OrderId}: {ex.Message}");
                throw;
            }
        }


    }
}
