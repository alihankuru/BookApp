using AutoMapper;
using BookApp.BusinessLayer.Abstract;
using BookApp.BusinessLayer.Validators.ShelfLocationValidators;
using BookApp.DtoLayer.ShelfLocation;
using BookApp.EntityLayer.Concrete;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShelfLocationsController : ControllerBase
    {
        private readonly IShelfLocationService _shelfLocationService;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateShelfLocationDto> _createShelfLocationValidator;
        private readonly IValidator<UpdateShelfLocationDto> _updateShelfLocationValidator;

        public ShelfLocationsController(IShelfLocationService shelfLocationService, IMapper mapper, IValidator<CreateShelfLocationDto> createShelfLocationValidator, IValidator<UpdateShelfLocationDto> updateShelfLocationValidator)
        {
            _shelfLocationService = shelfLocationService;
            _mapper = mapper;
            _createShelfLocationValidator = createShelfLocationValidator;
            _updateShelfLocationValidator = updateShelfLocationValidator;
        }

        [HttpGet]
        public IActionResult ShelfLocationList()
        {
            var values = _shelfLocationService.TGetList();
            return Ok(values);
        }


        [HttpPost]
        public IActionResult CreateShelfLocation(CreateShelfLocationDto createShelfLocationDto)
        {
            var validatorResult = _createShelfLocationValidator.Validate(createShelfLocationDto);

            if (!validatorResult.IsValid)
            {
                // If validation fails, return BadRequest with validation errors
                return BadRequest(validatorResult.Errors);
            }

            // Mapping DTO to entity
            var shelfLocation = _mapper.Map<ShelfLocation>(createShelfLocationDto);

            // Assuming _shelfLocationService.TInsert() method handles insertion
            _shelfLocationService.TInsert(shelfLocation);

            // Return Ok response
            return Ok(new { message = "Create successful" });
        }



        [HttpDelete("DeleteDestination/{id}")]
        public IActionResult DeleteShelfLocation(int id)
        {
            _shelfLocationService.TDelete(id);
            return StatusCode(200);
        }

        [HttpGet("ShelfLocation/{id}")]
        public IActionResult GetShelfLocation(int id)
        {
            var value = _shelfLocationService.TGetById(id);
            return Ok(value);
        }

        [HttpPut]
        public IActionResult UpdateShelfLocation(UpdateShelfLocationDto updateShelfLocationDto)
        {
            var validatorResult = _updateShelfLocationValidator.Validate(updateShelfLocationDto);

            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors);
            }
            var values = _mapper.Map<ShelfLocation>(updateShelfLocationDto);
            _shelfLocationService.TUpdate(values);

            return StatusCode(200, new { message = "Update successful" });
        }

    }
}
