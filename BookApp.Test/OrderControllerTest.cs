using AutoMapper;
using BookApp.BusinessLayer.Abstract;
using BookApp.DtoLayer.Order;
using BookApp.EntityLayer.Concrete;
using FluentValidation.Results;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OrderApp.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.Test
{
    public class OrdersControllerTest
    {
        private readonly Mock<IOrderService> _orderServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IValidator<CreateOrderDto>> _createOrderValidatorMock;
        private readonly Mock<IValidator<UpdateOrderDto>> _updateOrderValidatorMock;
        private readonly OrdersController _controller;

        public OrdersControllerTest()
        {
            _orderServiceMock = new Mock<IOrderService>();
            _mapperMock = new Mock<IMapper>();
            _createOrderValidatorMock = new Mock<IValidator<CreateOrderDto>>();
            _updateOrderValidatorMock = new Mock<IValidator<UpdateOrderDto>>();
            _controller = new OrdersController(
                _orderServiceMock.Object,
                _mapperMock.Object,
                _createOrderValidatorMock.Object,
                _updateOrderValidatorMock.Object);
        }

        [Fact]
        public void OrderList_ReturnsOkResult_WithListOfOrders()
        {
            // Arrange
            var orders = new List<Order>();
            _orderServiceMock.Setup(service => service.TGetList()).Returns(orders);
            _mapperMock.Setup(mapper => mapper.Map<List<ResultOrderDto>>(orders)).Returns(new List<ResultOrderDto>());

            // Act
            var result = _controller.OrderList();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<List<ResultOrderDto>>(okResult.Value);
        }

        [Fact]
        public void CreateOrder_ReturnsBadRequest_WhenValidationFails()
        {
            // Arrange
            var createOrderDto = new CreateOrderDto();
            var validationResult = new ValidationResult(new List<ValidationFailure> { new ValidationFailure("OrderNumber", "Order number is required") });
            _createOrderValidatorMock.Setup(validator => validator.Validate(createOrderDto)).Returns(validationResult);

            // Act
            var result = _controller.CreateOrder(createOrderDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<List<ValidationFailure>>(badRequestResult.Value);
        }

        [Fact]
        public void CreateOrder_ReturnsOkResult_WhenValidationSucceeds()
        {
            // Arrange
            var createOrderDto = new CreateOrderDto();
            var validationResult = new ValidationResult();
            _createOrderValidatorMock.Setup(validator => validator.Validate(createOrderDto)).Returns(validationResult);
            var order = new Order();
            _mapperMock.Setup(mapper => mapper.Map<Order>(createOrderDto)).Returns(order);

            // Act
            var result = _controller.CreateOrder(createOrderDto);

            // Assert
            var okResult = Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void DeleteOrder_ReturnsStatusCode200_WhenOrderIsDeleted()
        {
            // Arrange
            var orderId = 1;
            _orderServiceMock.Setup(service => service.TDelete(orderId)).Verifiable();

            // Act
            var result = _controller.DeleteOrder(orderId);

            // Assert
            var statusCodeResult = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal(200, statusCodeResult.StatusCode);
            _orderServiceMock.Verify(service => service.TDelete(orderId), Times.Once);
        }

        [Fact]
        public void GetOrder_ReturnsNotFound_WhenOrderDoesNotExist()
        {
            // Arrange
            var orderId = 1;
            _orderServiceMock.Setup(service => service.TGetById(orderId)).Returns((Order)null);

            // Act
            var result = _controller.GetOrder(orderId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetOrder_ReturnsOkResult_WithOrder_WhenOrderExists()
        {
            // Arrange
            var orderId = 1;
            var order = new Order();
            var getByIdOrderDto = new GetByIdOrderDto();
            _orderServiceMock.Setup(service => service.TGetById(orderId)).Returns(order);
            _mapperMock.Setup(mapper => mapper.Map<GetByIdOrderDto>(order)).Returns(getByIdOrderDto);

            // Act
            var result = _controller.GetOrder(orderId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<GetByIdOrderDto>(okResult.Value);
        }

        [Fact]
        public void UpdateOrder_ReturnsBadRequest_WhenValidationFails()
        {
            // Arrange
            var updateOrderDto = new UpdateOrderDto();
            var validationResult = new ValidationResult(new List<ValidationFailure> { new ValidationFailure("OrderNumber", "Order number is required") });
            _updateOrderValidatorMock.Setup(validator => validator.Validate(updateOrderDto)).Returns(validationResult);

            // Act
            var result = _controller.UpdateOrder(updateOrderDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<List<ValidationFailure>>(badRequestResult.Value);
        }

        [Fact]
        public void UpdateOrder_ReturnsOkResult_WhenValidationSucceeds()
        {
            // Arrange
            var updateOrderDto = new UpdateOrderDto();
            var validationResult = new ValidationResult();
            _updateOrderValidatorMock.Setup(validator => validator.Validate(updateOrderDto)).Returns(validationResult);
            var order = new Order();
            _mapperMock.Setup(mapper => mapper.Map<Order>(updateOrderDto)).Returns(order);

            // Act
            var result = _controller.UpdateOrder(updateOrderDto);

            // Assert
            var okResult = Assert.IsType<OkResult>(result);
        }
    }
}
