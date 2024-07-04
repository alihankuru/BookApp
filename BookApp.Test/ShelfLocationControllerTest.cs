using AutoMapper;
using BookApp.BusinessLayer.Abstract;
using BookApp.DtoLayer.ShelfLocation;
using BookApp.EntityLayer.Concrete;
using BookApp.WebApi.Controllers;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.Tests.Controllers
{
    public class ShelfLocationControllerTest
    {
        private readonly ShelfLocationsController _controller;
        private readonly Mock<IShelfLocationService> _mockShelfLocationService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IValidator<CreateShelfLocationDto>> _mockCreateValidator;
        private readonly Mock<IValidator<UpdateShelfLocationDto>> _mockUpdateValidator;


        public ShelfLocationControllerTest()
        {
            _mockShelfLocationService = new Mock<IShelfLocationService>();
            _mockMapper = new Mock<IMapper>();
            _mockCreateValidator = new Mock<IValidator<CreateShelfLocationDto>>();
            _mockUpdateValidator = new Mock<IValidator<UpdateShelfLocationDto>>();

            _controller = new ShelfLocationsController(
                _mockShelfLocationService.Object,
                _mockMapper.Object,
                _mockCreateValidator.Object,
                _mockUpdateValidator.Object
            );
        }

        [Fact]
        public void ShelfLocationList_ReturnsOk()
        {
            // Arrange
            var shelfLocations = new List<ShelfLocation>(); // Sample data
            _mockShelfLocationService.Setup(repo => repo.TGetList()).Returns(shelfLocations);

            // Act
            var result = _controller.ShelfLocationList();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<ShelfLocation>>(okResult.Value);
            Assert.Empty(model); // Add assertions based on expected data
        }

        [Fact]
        public void CreateShelfLocation_WithValidDto_ReturnsOk()
        {
            // Arrange
            var createDto = new CreateShelfLocationDto(); // Create a valid DTO
            var shelfLocation = new ShelfLocation(); // Create a valid ShelfLocation entity

            _mockCreateValidator.Setup(v => v.Validate(createDto))
                .Returns(new FluentValidation.Results.ValidationResult());

            _mockMapper.Setup(m => m.Map<ShelfLocation>(createDto))
                .Returns(shelfLocation);

            // Act
            var result = _controller.CreateShelfLocation(createDto);

            // Assert
            Assert.IsType<OkResult>(result);
        }


        [Fact]
        public void DeleteShelfLocation_ReturnsStatusCode200()
        {
            // Arrange
            int idToDelete = 1; // Sample ID
            _mockShelfLocationService.Setup(repo => repo.TDelete(idToDelete));

            // Act
            var result = _controller.DeleteShelfLocation(idToDelete);

            // Assert
            var statusCodeResult = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public void GetShelfLocation_ReturnsOkWithItem()
        {
            // Arrange
            int idToGet = 1; // Sample ID
            var shelfLocation = new ShelfLocation { ShelfLocationId = idToGet }; // Sample data
            _mockShelfLocationService.Setup(repo => repo.TGetById(idToGet)).Returns(shelfLocation);

            // Act
            var result = _controller.GetShelfLocation(idToGet);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsType<ShelfLocation>(okResult.Value);
            Assert.Equal(idToGet, model.ShelfLocationId);
        }


        [Fact]
        public void UpdateShelfLocation_WithValidDto_ReturnsOkResult()
        {
            // Arrange
            var updateShelfLocationDto = new UpdateShelfLocationDto
            {
                // Initialize with valid properties for your test scenario
                ShelfLocationId = 1,
                // Add other properties as needed
            };

            var mappedShelfLocation = new ShelfLocation
            {
                // Optionally, mock the mapped result for testing
                ShelfLocationId = 1,
                // Set other properties as needed
            };

            _mockMapper.Setup(m => m.Map<ShelfLocation>(updateShelfLocationDto))
                       .Returns(mappedShelfLocation);

            _mockUpdateValidator.Setup(v => v.Validate(updateShelfLocationDto))
                                .Returns(new FluentValidation.Results.ValidationResult());

            // Act
            var result = _controller.UpdateShelfLocation(updateShelfLocationDto);

            // Assert
            var okResult = Assert.IsType<OkResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public void UpdateShelfLocation_WithInvalidDto_ReturnsBadRequest()
        {
            // Arrange
            var updateShelfLocationDto = new UpdateShelfLocationDto
            {
                // Initialize with invalid properties for your test scenario
                ShelfLocationId = 0, // Example of an invalid scenario
            };

            var validationErrors = new List<FluentValidation.Results.ValidationFailure>
        {
            new FluentValidation.Results.ValidationFailure("ShelfLocationId", "ShelfLocationId must be greater than zero.")
        };

            _mockUpdateValidator.Setup(v => v.Validate(updateShelfLocationDto))
                                .Returns(new FluentValidation.Results.ValidationResult(validationErrors));

            // Act
            var result = _controller.UpdateShelfLocation(updateShelfLocationDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var errors = Assert.IsAssignableFrom<IEnumerable<FluentValidation.Results.ValidationFailure>>(badRequestResult.Value);
            Assert.Equal("ShelfLocationId", errors.First().PropertyName);
        }

    }
}
