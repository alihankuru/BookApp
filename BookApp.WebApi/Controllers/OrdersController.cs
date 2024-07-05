using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BookApp.BusinessLayer.Abstract;
using BookApp.DtoLayer.Order;
using BookApp.BusinessLayer.Validators.BookValidators;
using BookApp.DtoLayer.Book;
using BookApp.EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;

namespace OrderApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateOrderDto> _createOrderValidator;
        private readonly IValidator<UpdateOrderDto> _updateOrderValidator;

        public OrdersController(IOrderService orderService, IMapper mapper, IValidator<CreateOrderDto> createOrderValidator, IValidator<UpdateOrderDto> updateOrderValidator)
        {
            _orderService = orderService;
            _mapper = mapper;
            _createOrderValidator = createOrderValidator;
            _updateOrderValidator = updateOrderValidator;
        }

        [HttpGet]
        [Authorize(Roles ="User")]
        public IActionResult OrderList()
        {
            var orders = _orderService.TGetList();
            var resultOrderDtos = _mapper.Map<List<ResultOrderDto>>(orders);
            return Ok(resultOrderDtos);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateOrder(CreateOrderDto createOrderDto)
        {
            var validatorResult = _createOrderValidator.Validate(createOrderDto);

            if (!validatorResult.IsValid)
            {
                // If validation fails, return BadRequest with validation errors
                return BadRequest(validatorResult.Errors);
            }

            // Mapping DTO to entity
            var order = _mapper.Map<Order>(createOrderDto);

            // Assuming _shelfLocationService.TInsert() method handles insertion
            _orderService.TInsert(order);

            // Return Ok response
            return Ok();
        }

        [HttpDelete("DeleteOrder/{id}")]
        public IActionResult DeleteOrder(int id)
        {
            _orderService.TDelete(id);
            return StatusCode(200);
        }

        [HttpGet("GetOrder/{id}")]
        public IActionResult GetOrder(int id)
        {
            var order = _orderService.TGetById(id);
            if (order == null)
            {
                return NotFound();
            }
            var getByIdOrderDto = _mapper.Map<GetByIdOrderDto>(order);
            return Ok(getByIdOrderDto);
        }


        [HttpPut]
        public IActionResult UpdateOrder(UpdateOrderDto updateOrderDto)
        {
            var validatorResult = _updateOrderValidator.Validate(updateOrderDto);

            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors);
            }
            var values = _mapper.Map<Order>(updateOrderDto);
            _orderService.TUpdate(values);

            return Ok();
        }



    }
}
