using AutoMapper;
using BookApp.BusinessLayer.Abstract;
using BookApp.BusinessLayer.Validators.ShelfLocationValidators;
using BookApp.DtoLayer.Book;
using BookApp.DtoLayer.ShelfLocation;
using BookApp.EntityLayer.Concrete;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateBookDto> _createbookValidator;
        private readonly IValidator<UpdateBookDto> _updateBookValidator;

        public BooksController(IBookService bookService, IMapper mapper, IValidator<CreateBookDto> createbookValidator, IValidator<UpdateBookDto> updateBookValidator)
        {
            _bookService = bookService;
            _mapper = mapper;
            _createbookValidator = createbookValidator;
            _updateBookValidator = updateBookValidator;
        }


        [HttpGet]
        public IActionResult BookList()
        {
            var books = _bookService.TGetList();
            var resultBookDtos = _mapper.Map<List<ResultBookDto>>(books);
            return Ok(resultBookDtos);
        }

        [HttpPost]
        public IActionResult CreateBook(CreateBookDto createBookDto)
        {
            var validatorResult = _createbookValidator.Validate(createBookDto);

            if (!validatorResult.IsValid)
            {
                // If validation fails, return BadRequest with validation errors
                return BadRequest(validatorResult.Errors);
            }

            // Mapping DTO to entity
            var book = _mapper.Map<Book>(createBookDto);

            // Assuming _shelfLocationService.TInsert() method handles insertion
            _bookService.TInsert(book);

            // Return Ok response
            return Ok();
        }


        [HttpDelete("DeleteBook/{id}")]
        public IActionResult DeleteBook(int id)
        {
            _bookService.TDelete(id);
            return StatusCode(200);
        }

        [HttpGet("GetBook/{id}")]
        public IActionResult GetBook(int id)
        {
            var book = _bookService.TGetById(id);
            if (book == null)
            {
                return NotFound();
            }
            var getByIdBookDto = _mapper.Map<GetByIdBookDto>(book);
            return Ok(getByIdBookDto);
        }

        [HttpPut]
        public IActionResult UpdateBook(UpdateBookDto updateBookDto)
        {
            var validatorResult = _updateBookValidator.Validate(updateBookDto);

            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors);
            }
            var values = _mapper.Map<Book>(updateBookDto);
            _bookService.TUpdate(values);

            return Ok();
        }



    }
}
