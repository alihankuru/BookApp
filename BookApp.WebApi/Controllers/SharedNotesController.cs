using AutoMapper;
using BookApp.BusinessLayer.Abstract;
using BookApp.BusinessLayer.Validators.SharedNoteValidators;
using BookApp.DtoLayer.SharedNote;
using BookApp.DtoLayer.SharedNote;
using BookApp.EntityLayer.Concrete;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SharedNoteApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SharedNotesController : ControllerBase
    {
        private readonly ISharedNoteService _sharedNoteService;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateSharedNoteDto> _createSharedNoteValidator;
        private readonly IValidator<UpdateSharedNoteDto> _updateSharedNoteValidator;

        public SharedNotesController(ISharedNoteService sharedNoteService, IMapper mapper, IValidator<CreateSharedNoteDto> createSharedNoteValidator, IValidator<UpdateSharedNoteDto> updateSharedNoteValidator)
        {
            _sharedNoteService = sharedNoteService;
            _mapper = mapper;
            _createSharedNoteValidator = createSharedNoteValidator;
            _updateSharedNoteValidator = updateSharedNoteValidator;
        }


        [HttpGet]
        public IActionResult SharedNoteList()
        {
            var sharedNotes = _sharedNoteService.TGetList();
            var resultSharedNoteDtos = _mapper.Map<List<ResultSharedNoteDto>>(sharedNotes);
            return Ok(resultSharedNoteDtos);
        }

        [HttpPost]
        public IActionResult CreateSharedNote(CreateSharedNoteDto createSharedNoteDto)
        {
            var validatorResult = _createSharedNoteValidator.Validate(createSharedNoteDto);

            if (!validatorResult.IsValid)
            {
                // If validation fails, return BadRequest with validation errors
                return BadRequest(validatorResult.Errors);
            }

            // Mapping DTO to entity
            var sharedNote = _mapper.Map<SharedNote>(createSharedNoteDto);

            // Assuming _shelfLocationService.TInsert() method handles insertion
            _sharedNoteService.TInsert(sharedNote);

            // Return Ok response
            return Ok();
        }


        [HttpDelete("DeleteSharedNote/{id}")]
        public IActionResult DeleteSharedNote(int id)
        {
            _sharedNoteService.TDelete(id);
            return StatusCode(200);
        }

        [HttpGet("GetSharedNote/{id}")]
        public IActionResult GetSharedNote(int id)
        {
            var sharedNote = _sharedNoteService.TGetById(id);
            if (sharedNote == null)
            {
                return NotFound();
            }
            var getByIdSharedNoteDto = _mapper.Map<GetByIdSharedNoteDto>(sharedNote);
            return Ok(getByIdSharedNoteDto);
        }

        [HttpPut]
        public IActionResult UpdateSharedNote(UpdateSharedNoteDto updateSharedNoteDto)
        {
            var validatorResult = _updateSharedNoteValidator.Validate(updateSharedNoteDto);

            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors);
            }
            var values = _mapper.Map<SharedNote>(updateSharedNoteDto);
            _sharedNoteService.TUpdate(values);

            return Ok();
        }





    }
}
