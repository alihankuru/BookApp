using AutoMapper;
using BookApp.BusinessLayer.Abstract;
using BookApp.DtoLayer.SharedNote;
using BookApp.EntityLayer.Concrete;
using FluentValidation.Results;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SharedNoteApp.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.Test
{
    public class SharedNoteControllerTest
    {
        private readonly Mock<ISharedNoteService> _sharedNoteServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IValidator<CreateSharedNoteDto>> _createSharedNoteValidatorMock;
        private readonly Mock<IValidator<UpdateSharedNoteDto>> _updateSharedNoteValidatorMock;
        private readonly SharedNotesController _controller;

        public SharedNoteControllerTest()
        {
            _sharedNoteServiceMock = new Mock<ISharedNoteService>();
            _mapperMock = new Mock<IMapper>();
            _createSharedNoteValidatorMock = new Mock<IValidator<CreateSharedNoteDto>>();
            _updateSharedNoteValidatorMock = new Mock<IValidator<UpdateSharedNoteDto>>();
            _controller = new SharedNotesController(
                _sharedNoteServiceMock.Object,
                _mapperMock.Object,
                _createSharedNoteValidatorMock.Object,
                _updateSharedNoteValidatorMock.Object
            );
        }

        [Fact]
        public void SharedNoteList_ReturnsOkResult_WithListOfSharedNotes()
        {
            // Arrange
            var sharedNotes = new List<SharedNote>();
            _sharedNoteServiceMock.Setup(service => service.TGetList()).Returns(sharedNotes);
            _mapperMock.Setup(mapper => mapper.Map<List<ResultSharedNoteDto>>(sharedNotes)).Returns(new List<ResultSharedNoteDto>());

            // Act
            var result = _controller.SharedNoteList();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<List<ResultSharedNoteDto>>(okResult.Value);
        }

        [Fact]
        public void CreateSharedNote_ReturnsBadRequest_WhenValidationFails()
        {
            // Arrange
            var createSharedNoteDto = new CreateSharedNoteDto();
            var validationResult = new ValidationResult(new List<ValidationFailure> { new ValidationFailure("Content", "Content is required") });
            _createSharedNoteValidatorMock.Setup(validator => validator.Validate(createSharedNoteDto)).Returns(validationResult);

            // Act
            var result = _controller.CreateSharedNote(createSharedNoteDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<List<ValidationFailure>>(badRequestResult.Value);
        }

        [Fact]
        public void CreateSharedNote_ReturnsOkResult_WhenValidationSucceeds()
        {
            // Arrange
            var createSharedNoteDto = new CreateSharedNoteDto();
            var validationResult = new ValidationResult();
            _createSharedNoteValidatorMock.Setup(validator => validator.Validate(createSharedNoteDto)).Returns(validationResult);
            var sharedNote = new SharedNote();
            _mapperMock.Setup(mapper => mapper.Map<SharedNote>(createSharedNoteDto)).Returns(sharedNote);

            // Act
            var result = _controller.CreateSharedNote(createSharedNoteDto);

            // Assert
            var okResult = Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void DeleteSharedNote_ReturnsStatusCode200_WhenSharedNoteIsDeleted()
        {
            // Arrange
            var sharedNoteId = 1;
            _sharedNoteServiceMock.Setup(service => service.TDelete(sharedNoteId)).Verifiable();

            // Act
            var result = _controller.DeleteSharedNote(sharedNoteId);

            // Assert
            var statusCodeResult = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal(200, statusCodeResult.StatusCode);
            _sharedNoteServiceMock.Verify(service => service.TDelete(sharedNoteId), Times.Once);
        }

        [Fact]
        public void GetSharedNote_ReturnsNotFound_WhenSharedNoteDoesNotExist()
        {
            // Arrange
            var sharedNoteId = 1;
            _sharedNoteServiceMock.Setup(service => service.TGetById(sharedNoteId)).Returns((SharedNote)null);

            // Act
            var result = _controller.GetSharedNote(sharedNoteId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetSharedNote_ReturnsOkResult_WithSharedNote_WhenSharedNoteExists()
        {
            // Arrange
            var sharedNoteId = 1;
            var sharedNote = new SharedNote();
            var getByIdSharedNoteDto = new GetByIdSharedNoteDto();
            _sharedNoteServiceMock.Setup(service => service.TGetById(sharedNoteId)).Returns(sharedNote);
            _mapperMock.Setup(mapper => mapper.Map<GetByIdSharedNoteDto>(sharedNote)).Returns(getByIdSharedNoteDto);

            // Act
            var result = _controller.GetSharedNote(sharedNoteId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<GetByIdSharedNoteDto>(okResult.Value);
        }

        [Fact]
        public void UpdateSharedNote_ReturnsBadRequest_WhenValidationFails()
        {
            // Arrange
            var updateSharedNoteDto = new UpdateSharedNoteDto();
            var validationResult = new ValidationResult(new List<ValidationFailure> { new ValidationFailure("Content", "Content is required") });
            _updateSharedNoteValidatorMock.Setup(validator => validator.Validate(updateSharedNoteDto)).Returns(validationResult);

            // Act
            var result = _controller.UpdateSharedNote(updateSharedNoteDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<List<ValidationFailure>>(badRequestResult.Value);
        }

        [Fact]
        public void UpdateSharedNote_ReturnsOkResult_WhenValidationSucceeds()
        {
            // Arrange
            var updateSharedNoteDto = new UpdateSharedNoteDto();
            var validationResult = new ValidationResult();
            _updateSharedNoteValidatorMock.Setup(validator => validator.Validate(updateSharedNoteDto)).Returns(validationResult);
            var sharedNote = new SharedNote();
            _mapperMock.Setup(mapper => mapper.Map<SharedNote>(updateSharedNoteDto)).Returns(sharedNote);

            // Act
            var result = _controller.UpdateSharedNote(updateSharedNoteDto);

            // Assert
            var okResult = Assert.IsType<OkResult>(result);
        }
    }

}
