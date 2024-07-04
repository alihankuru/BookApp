using AutoMapper;
using BookApp.BusinessLayer.Abstract;
using BookApp.DtoLayer.OrderItem;
using BookApp.EntityLayer.Concrete;
using FluentValidation.Results;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OrderItemApp.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.Test
{
    public class OrderItemControllerTest
    {

        private readonly Mock<IOrderItemService> _orderItemServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IValidator<CreateOrderItemDto>> _createOrderItemValidatorMock;
        private readonly Mock<IValidator<UpdateOrderItemDto>> _updateOrderItemValidatorMock;
        private readonly OrderItemsController _controller;

        public OrderItemControllerTest()
        {
            _orderItemServiceMock = new Mock<IOrderItemService>();
            _mapperMock = new Mock<IMapper>();
            _createOrderItemValidatorMock = new Mock<IValidator<CreateOrderItemDto>>();
            _updateOrderItemValidatorMock = new Mock<IValidator<UpdateOrderItemDto>>();
            _controller = new OrderItemsController(
                _orderItemServiceMock.Object,
                _mapperMock.Object,
                _createOrderItemValidatorMock.Object,
                _updateOrderItemValidatorMock.Object
            );
        }

        [Fact]
        public void OrderItemList_ReturnsOkResult_WithListOfOrderItems()
        {
            // Arrange
            var orderItems = new List<OrderItem>();
            _orderItemServiceMock.Setup(service => service.TGetList()).Returns(orderItems);
            _mapperMock.Setup(mapper => mapper.Map<List<ResultOrderItemDto>>(orderItems)).Returns(new List<ResultOrderItemDto>());

            // Act
            var result = _controller.OrderItemList();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<List<ResultOrderItemDto>>(okResult.Value);
        }

        [Fact]
        public void CreateOrderItem_ReturnsBadRequest_WhenValidationFails()
        {
            // Arrange
            var createOrderItemDto = new CreateOrderItemDto();
            var validationResult = new ValidationResult(new List<ValidationFailure> { new ValidationFailure("ProductName", "Product name is required") });
            _createOrderItemValidatorMock.Setup(validator => validator.Validate(createOrderItemDto)).Returns(validationResult);

            // Act
            var result = _controller.CreateOrderItem(createOrderItemDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<List<ValidationFailure>>(badRequestResult.Value);
        }

        [Fact]
        public void CreateOrderItem_ReturnsOkResult_WhenValidationSucceeds()
        {
            // Arrange
            var createOrderItemDto = new CreateOrderItemDto();
            var validationResult = new ValidationResult();
            _createOrderItemValidatorMock.Setup(validator => validator.Validate(createOrderItemDto)).Returns(validationResult);
            var orderItem = new OrderItem();
            _mapperMock.Setup(mapper => mapper.Map<OrderItem>(createOrderItemDto)).Returns(orderItem);

            // Act
            var result = _controller.CreateOrderItem(createOrderItemDto);

            // Assert
            var okResult = Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void DeleteOrderItem_ReturnsStatusCode200_WhenOrderItemIsDeleted()
        {
            // Arrange
            var orderItemId = 1;
            _orderItemServiceMock.Setup(service => service.TDelete(orderItemId)).Verifiable();

            // Act
            var result = _controller.DeleteOrderItem(orderItemId);

            // Assert
            var statusCodeResult = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal(200, statusCodeResult.StatusCode);
            _orderItemServiceMock.Verify(service => service.TDelete(orderItemId), Times.Once);
        }

        [Fact]
        public void GetOrderItem_ReturnsNotFound_WhenOrderItemDoesNotExist()
        {
            // Arrange
            var orderItemId = 1;
            _orderItemServiceMock.Setup(service => service.TGetById(orderItemId)).Returns((OrderItem)null);

            // Act
            var result = _controller.GetOrderItem(orderItemId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetOrderItem_ReturnsOkResult_WithOrderItem_WhenOrderItemExists()
        {
            // Arrange
            var orderItemId = 1;
            var orderItem = new OrderItem();
            var getByIdOrderItemDto = new GetByIdOrderItemDto();
            _orderItemServiceMock.Setup(service => service.TGetById(orderItemId)).Returns(orderItem);
            _mapperMock.Setup(mapper => mapper.Map<GetByIdOrderItemDto>(orderItem)).Returns(getByIdOrderItemDto);

            // Act
            var result = _controller.GetOrderItem(orderItemId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<GetByIdOrderItemDto>(okResult.Value);
        }

        [Fact]
        public void UpdateOrderItem_ReturnsBadRequest_WhenValidationFails()
        {
            // Arrange
            var updateOrderItemDto = new UpdateOrderItemDto();
            var validationResult = new ValidationResult(new List<ValidationFailure> { new ValidationFailure("ProductName", "Product name is required") });
            _updateOrderItemValidatorMock.Setup(validator => validator.Validate(updateOrderItemDto)).Returns(validationResult);

            // Act
            var result = _controller.UpdateOrderItem(updateOrderItemDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<List<ValidationFailure>>(badRequestResult.Value);
        }

        [Fact]
        public void UpdateOrderItem_ReturnsOkResult_WhenValidationSucceeds()
        {
            // Arrange
            var updateOrderItemDto = new UpdateOrderItemDto();
            var validationResult = new ValidationResult();
            _updateOrderItemValidatorMock.Setup(validator => validator.Validate(updateOrderItemDto)).Returns(validationResult);
            var orderItem = new OrderItem();
            _mapperMock.Setup(mapper => mapper.Map<OrderItem>(updateOrderItemDto)).Returns(orderItem);

            // Act
            var result = _controller.UpdateOrderItem(updateOrderItemDto);

            // Assert
            var okResult = Assert.IsType<OkResult>(result);
        }
    }

}
