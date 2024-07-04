using AutoMapper;
using BookApp.BusinessLayer.Abstract;
using BookApp.DtoLayer.BookNote;
using BookApp.EntityLayer.Concrete;
using BookNoteApp.WebApi.Controllers;
using FluentValidation.Results;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.Test
{
    public class BookNoteControllerTest
    {
        private readonly Mock<IBookNoteService> _bookNoteServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IValidator<CreateBookNoteDto>> _createBookNoteValidatorMock;
        private readonly Mock<IValidator<UpdateBookNoteDto>> _updateBookNoteValidatorMock;
        private readonly BookNotesController _controller;

        public BookNoteControllerTest()
        {
            _bookNoteServiceMock = new Mock<IBookNoteService>();
            _mapperMock = new Mock<IMapper>();
            _createBookNoteValidatorMock = new Mock<IValidator<CreateBookNoteDto>>();
            _updateBookNoteValidatorMock = new Mock<IValidator<UpdateBookNoteDto>>();
            _controller = new BookNotesController(
                _bookNoteServiceMock.Object,
                _mapperMock.Object,
                _createBookNoteValidatorMock.Object,
                _updateBookNoteValidatorMock.Object);
        }

        [Fact]
        public void BookNoteList_ReturnsOkResult_WithListOfBookNotes()
        {
            // Arrange
            var bookNotes = new List<BookNote>();
            _bookNoteServiceMock.Setup(service => service.TGetList()).Returns(bookNotes);
            _mapperMock.Setup(mapper => mapper.Map<List<ResultBookNoteDto>>(bookNotes)).Returns(new List<ResultBookNoteDto>());

            // Act
            var result = _controller.BookNoteList();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<List<ResultBookNoteDto>>(okResult.Value);
        }

        [Fact]
        public void CreateBookNote_ReturnsBadRequest_WhenValidationFails()
        {
            // Arrange
            var createBookNoteDto = new CreateBookNoteDto();
            var validationResult = new ValidationResult(new List<ValidationFailure> { new ValidationFailure("Title", "Title is required") });
            _createBookNoteValidatorMock.Setup(validator => validator.Validate(createBookNoteDto)).Returns(validationResult);

            // Act
            var result = _controller.CreateBookNote(createBookNoteDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<List<ValidationFailure>>(badRequestResult.Value);
        }

        [Fact]
        public void CreateBookNote_ReturnsOkResult_WhenValidationSucceeds()
        {
            // Arrange
            var createBookNoteDto = new CreateBookNoteDto();
            var validationResult = new ValidationResult();
            _createBookNoteValidatorMock.Setup(validator => validator.Validate(createBookNoteDto)).Returns(validationResult);
            var bookNote = new BookNote();
            _mapperMock.Setup(mapper => mapper.Map<BookNote>(createBookNoteDto)).Returns(bookNote);

            // Act
            var result = _controller.CreateBookNote(createBookNoteDto);

            // Assert
            var okResult = Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void DeleteBookNote_ReturnsStatusCode200_WhenBookNoteIsDeleted()
        {
            // Arrange
            var bookNoteId = 1;
            _bookNoteServiceMock.Setup(service => service.TDelete(bookNoteId)).Verifiable();

            // Act
            var result = _controller.DeleteBookNote(bookNoteId);

            // Assert
            var statusCodeResult = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal(200, statusCodeResult.StatusCode);
            _bookNoteServiceMock.Verify(service => service.TDelete(bookNoteId), Times.Once);
        }

        [Fact]
        public void GetBookNote_ReturnsNotFound_WhenBookNoteDoesNotExist()
        {
            // Arrange
            var bookNoteId = 1;
            _bookNoteServiceMock.Setup(service => service.TGetById(bookNoteId)).Returns((BookNote)null);

            // Act
            var result = _controller.GetBookNote(bookNoteId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetBookNote_ReturnsOkResult_WithBookNote_WhenBookNoteExists()
        {
            // Arrange
            var bookNoteId = 1;
            var bookNote = new BookNote();
            var getByIdBookNoteDto = new GetByIdBookNoteDto();
            _bookNoteServiceMock.Setup(service => service.TGetById(bookNoteId)).Returns(bookNote);
            _mapperMock.Setup(mapper => mapper.Map<GetByIdBookNoteDto>(bookNote)).Returns(getByIdBookNoteDto);

            // Act
            var result = _controller.GetBookNote(bookNoteId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<GetByIdBookNoteDto>(okResult.Value);
        }

        [Fact]
        public void UpdateBookNote_ReturnsBadRequest_WhenValidationFails()
        {
            // Arrange
            var updateBookNoteDto = new UpdateBookNoteDto();
            var validationResult = new ValidationResult(new List<ValidationFailure> { new ValidationFailure("Title", "Title is required") });
            _updateBookNoteValidatorMock.Setup(validator => validator.Validate(updateBookNoteDto)).Returns(validationResult);

            // Act
            var result = _controller.UpdateBookNote(updateBookNoteDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<List<ValidationFailure>>(badRequestResult.Value);
        }

        [Fact]
        public void UpdateBookNote_ReturnsOkResult_WhenValidationSucceeds()
        {
            // Arrange
            var updateBookNoteDto = new UpdateBookNoteDto();
            var validationResult = new ValidationResult();
            _updateBookNoteValidatorMock.Setup(validator => validator.Validate(updateBookNoteDto)).Returns(validationResult);
            var bookNote = new BookNote();
            _mapperMock.Setup(mapper => mapper.Map<BookNote>(updateBookNoteDto)).Returns(bookNote);

            // Act
            var result = _controller.UpdateBookNote(updateBookNoteDto);

            // Assert
            var okResult = Assert.IsType<OkResult>(result);
        }
    }
}
