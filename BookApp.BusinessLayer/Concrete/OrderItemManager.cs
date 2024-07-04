using BookApp.EntityLayer.Concrete;
using BookApp.BusinessLayer.Abstract;
using BookApp.BusinessLayer.Serilog;
using BookApp.DataAccessLayer.Abstract;
using BookApp.DataAccessLayer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.BusinessLayer.Concrete
{
    public class OrderItemManager : IOrderItemService
    {
        private readonly IOrderItemDal _orderItemDal;
        private readonly IUowDal _uowDal;
        private readonly IAppLogger<OrderItem> _logger;

        public OrderItemManager(IOrderItemDal orderItemDal, IUowDal uowDal, IAppLogger<OrderItem> logger)
        {
            _orderItemDal = orderItemDal;
            _uowDal = uowDal;
            _logger = logger;
        }

        public void TDelete(int id)
        {
            try
            {
                _orderItemDal.Delete(id);
                _uowDal.Save();
                _logger.LogInformation($"orderItem with ID {id} deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting orderItem with ID {id}: {ex.Message}");
                throw;
            }
        }

        public OrderItem TGetById(int id)
        {
            try
            {
                var orderItem = _orderItemDal.GetById(id);
                _logger.LogInformation($"orderItem with ID {id} retrieved successfully.");
                return orderItem;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving orderItem with ID {id}: {ex.Message}");
                throw;
            }
        }

        public List<OrderItem> TGetList()
        {
            try
            {
                var orderItems = _orderItemDal.GetList();
                _logger.LogInformation("orderItem list retrieved successfully.");
                return orderItems;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving orderItem list: {ex.Message}");
                throw;
            }
        }

        public void TInsert(OrderItem orderItem)
        {
            try
            {
                _orderItemDal.Insert(orderItem);
                _uowDal.Save();
                _logger.LogInformation($"orderItem inserted successfully. ID: {orderItem.OrderItemId}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error inserting orderItem: {ex.Message}");
                throw;
            }
        }

        public void TMultiUpdate(List<OrderItem> orderItem)
        {
            try
            {
                _orderItemDal.MultiUpdate(orderItem);
                _uowDal.Save();
                _logger.LogInformation("orderItem list updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating orderItem list: {ex.Message}");
                throw;
            }
        }

        public void TUpdate(OrderItem orderItem)
        {
            try
            {
                _orderItemDal.Update(orderItem);
                _uowDal.Save();
                _logger.LogInformation($"orderItem with ID {orderItem.OrderItemId} updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating orderItem with ID {orderItem.OrderItemId}: {ex.Message}");
                throw;
            }
        }

    }
}
