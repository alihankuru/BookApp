using AutoMapper;
using BookApp.BusinessLayer.Abstract;
using BookApp.DtoLayer.ShelfLocation;
using BookApp.EntityLayer.Concrete;
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

        public ShelfLocationsController(IShelfLocationService shelfLocationService, IMapper mapper)
        {
            _shelfLocationService = shelfLocationService;
            _mapper = mapper;
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
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var values = _mapper.Map<ShelfLocation>(createShelfLocationDto);
            _shelfLocationService.TInsert(values);
            return StatusCode(200, new { message = "Create successful" });
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
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var values = _mapper.Map<ShelfLocation>(updateShelfLocationDto);
            _shelfLocationService.TUpdate(values);

            return StatusCode(200, new { message = "Update successful" });
        }

    }
}
