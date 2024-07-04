using AutoMapper;
using BookApp.BusinessLayer.Abstract;
using BookApp.DtoLayer.Book;
using BookApp.EntityLayer.Concrete;
using BookApp.WebApi.Controllers;
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
    public class BookControllerTest
    {
        private readonly Mock<IBookService> _bookServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IValidator<CreateBookDto>> _createBookValidatorMock;
        private readonly Mock<IValidator<UpdateBookDto>> _updateBookValidatorMock;
        private readonly BooksController _controller;

        public BookControllerTest()
        {
            _bookServiceMock = new Mock<IBookService>();
            _mapperMock = new Mock<IMapper>();
            _createBookValidatorMock = new Mock<IValidator<CreateBookDto>>();
            _updateBookValidatorMock = new Mock<IValidator<UpdateBookDto>>();
            _controller = new BooksController(
                _bookServiceMock.Object,
                _mapperMock.Object,
                _createBookValidatorMock.Object,
                _updateBookValidatorMock.Object);
        }

        [Fact]
        public void BookList_ReturnsOkResult_WithListOfBooks()
        {
            // Arrange
            var books = new List<Book>();
            _bookServiceMock.Setup(service => service.TGetList()).Returns(books);
            _mapperMock.Setup(mapper => mapper.Map<List<ResultBookDto>>(books)).Returns(new List<ResultBookDto>());

            // Act
            var result = _controller.BookList();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<List<ResultBookDto>>(okResult.Value);
        }

        [Fact]
        public void CreateBook_ReturnsBadRequest_WhenValidationFails()
        {
            // Arrange
            var createBookDto = new CreateBookDto();
            var validationResult = new ValidationResult(new List<ValidationFailure> { new ValidationFailure("Title", "Title is required") });
            _createBookValidatorMock.Setup(validator => validator.Validate(createBookDto)).Returns(validationResult);

            // Act
            var result = _controller.CreateBook(createBookDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<List<ValidationFailure>>(badRequestResult.Value);
        }

        [Fact]
        public void CreateBook_ReturnsOkResult_WhenValidationSucceeds()
        {
            // Arrange
            var createBookDto = new CreateBookDto();
            var validationResult = new ValidationResult();
            _createBookValidatorMock.Setup(validator => validator.Validate(createBookDto)).Returns(validationResult);
            var book = new Book();
            _mapperMock.Setup(mapper => mapper.Map<Book>(createBookDto)).Returns(book);

            // Act
            var result = _controller.CreateBook(createBookDto);

            // Assert
            var okResult = Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void DeleteBook_ReturnsStatusCode200_WhenBookIsDeleted()
        {
            // Arrange
            var bookId = 1;
            _bookServiceMock.Setup(service => service.TDelete(bookId)).Verifiable();

            // Act
            var result = _controller.DeleteBook(bookId);

            // Assert
            var statusCodeResult = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal(200, statusCodeResult.StatusCode);
            _bookServiceMock.Verify(service => service.TDelete(bookId), Times.Once);
        }

        [Fact]
        public void GetBook_ReturnsNotFound_WhenBookDoesNotExist()
        {
            // Arrange
            var bookId = 1;
            _bookServiceMock.Setup(service => service.TGetById(bookId)).Returns((Book)null);

            // Act
            var result = _controller.GetBook(bookId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetBook_ReturnsOkResult_WithBook_WhenBookExists()
        {
            // Arrange
            var bookId = 1;
            var book = new Book();
            var getByIdBookDto = new GetByIdBookDto();
            _bookServiceMock.Setup(service => service.TGetById(bookId)).Returns(book);
            _mapperMock.Setup(mapper => mapper.Map<GetByIdBookDto>(book)).Returns(getByIdBookDto);

            // Act
            var result = _controller.GetBook(bookId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<GetByIdBookDto>(okResult.Value);
        }

        [Fact]
        public void UpdateBook_ReturnsBadRequest_WhenValidationFails()
        {
            // Arrange
            var updateBookDto = new UpdateBookDto();
            var validationResult = new ValidationResult(new List<ValidationFailure> { new ValidationFailure("Title", "Title is required") });
            _updateBookValidatorMock.Setup(validator => validator.Validate(updateBookDto)).Returns(validationResult);

            // Act
            var result = _controller.UpdateBook(updateBookDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<List<ValidationFailure>>(badRequestResult.Value);
        }

        [Fact]
        public void UpdateBook_ReturnsOkResult_WhenValidationSucceeds()
        {
            // Arrange
            var updateBookDto = new UpdateBookDto();
            var validationResult = new ValidationResult();
            _updateBookValidatorMock.Setup(validator => validator.Validate(updateBookDto)).Returns(validationResult);
            var book = new Book();
            _mapperMock.Setup(mapper => mapper.Map<Book>(updateBookDto)).Returns(book);

            // Act
            var result = _controller.UpdateBook(updateBookDto);

            // Assert
            var okResult = Assert.IsType<OkResult>(result);
        }
    }
}
