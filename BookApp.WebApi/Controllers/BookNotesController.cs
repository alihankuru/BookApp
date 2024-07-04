using AutoMapper;
using BookApp.BusinessLayer.Abstract;
using BookApp.DtoLayer.BookNote;
using BookApp.EntityLayer.Concrete;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookNoteApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookNotesController : ControllerBase
    {
        private readonly IBookNoteService _bookNoteService;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateBookNoteDto> _createbookNoteValidator;
        private readonly IValidator<UpdateBookNoteDto> _updateBookNoteValidator;

        public BookNotesController(IBookNoteService bookNoteService, IMapper mapper, IValidator<CreateBookNoteDto> createbookNoteValidator, IValidator<UpdateBookNoteDto> updateBookNoteValidator)
        {
            _bookNoteService = bookNoteService;
            _mapper = mapper;
            _createbookNoteValidator = createbookNoteValidator;
            _updateBookNoteValidator = updateBookNoteValidator;
        }


        [HttpGet]
        public IActionResult BookNoteList()
        {
            var bookNotes = _bookNoteService.TGetList();
            var resultBookNoteDtos = _mapper.Map<List<ResultBookNoteDto>>(bookNotes);
            return Ok(resultBookNoteDtos);
        }

        [HttpPost]
        public IActionResult CreateBookNote(CreateBookNoteDto createBookNoteDto)
        {
            var validatorResult = _createbookNoteValidator.Validate(createBookNoteDto);

            if (!validatorResult.IsValid)
            {
                // If validation fails, return BadRequest with validation errors
                return BadRequest(validatorResult.Errors);
            }

            // Mapping DTO to entity
            var bookNote = _mapper.Map<BookNote>(createBookNoteDto);

            // Assuming _shelfLocationService.TInsert() method handles insertion
            _bookNoteService.TInsert(bookNote);

            // Return Ok response
            return Ok();
        }


        [HttpDelete("DeleteBookNote/{id}")]
        public IActionResult DeleteBookNote(int id)
        {
            _bookNoteService.TDelete(id);
            return StatusCode(200);
        }

        [HttpGet("GetBookNote/{id}")]
        public IActionResult GetBookNote(int id)
        {
            var bookNote = _bookNoteService.TGetById(id);
            if (bookNote == null)
            {
                return NotFound();
            }
            var getByIdBookNoteDto = _mapper.Map<GetByIdBookNoteDto>(bookNote);
            return Ok(getByIdBookNoteDto);
        }

        [HttpPut]
        public IActionResult UpdateBookNote(UpdateBookNoteDto updateBookNoteDto)
        {
            var validatorResult = _updateBookNoteValidator.Validate(updateBookNoteDto);

            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors);
            }
            var values = _mapper.Map<BookNote>(updateBookNoteDto);
            _bookNoteService.TUpdate(values);

            return Ok();
        }


    }
}
