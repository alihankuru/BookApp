using AutoMapper;
using BookApp.BusinessLayer.Abstract;
using BookApp.BusinessLayer.Validators.OrderItemValidators;
using BookApp.DtoLayer.OrderItem;
using BookApp.DtoLayer.OrderItem;
using BookApp.EntityLayer.Concrete;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OrderItemApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemsController : ControllerBase
    {
        private readonly IOrderItemService _orderItemService;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateOrderItemDto> _createOrderItemValidator;
        private readonly IValidator<UpdateOrderItemDto> _updateOrderItemValidator;

        public OrderItemsController(IOrderItemService orderItemService, IMapper mapper, IValidator<CreateOrderItemDto> createOrderItemValidator, IValidator<UpdateOrderItemDto> updateOrderItemValidator)
        {
            _orderItemService = orderItemService;
            _mapper = mapper;
            _createOrderItemValidator = createOrderItemValidator;
            _updateOrderItemValidator = updateOrderItemValidator;
        }

        [HttpGet]
        public IActionResult OrderItemList()
        {
            var orderItems = _orderItemService.TGetList();
            var resultOrderItemDtos = _mapper.Map<List<ResultOrderItemDto>>(orderItems);
            return Ok(resultOrderItemDtos);
        }

        [HttpPost]
        public IActionResult CreateOrderItem(CreateOrderItemDto createOrderItemDto)
        {
            var validatorResult = _createOrderItemValidator.Validate(createOrderItemDto);

            if (!validatorResult.IsValid)
            {
                // If validation fails, return BadRequest with validation errors
                return BadRequest(validatorResult.Errors);
            }

            // Mapping DTO to entity
            var orderItem = _mapper.Map<OrderItem>(createOrderItemDto);

            // Assuming _shelfLocationService.TInsert() method handles insertion
            _orderItemService.TInsert(orderItem);

            // Return Ok response
            return Ok();
        }


        [HttpDelete("DeleteOrderItem/{id}")]
        public IActionResult DeleteOrderItem(int id)
        {
            _orderItemService.TDelete(id);
            return StatusCode(200);
        }

        [HttpGet("GetOrderItem/{id}")]
        public IActionResult GetOrderItem(int id)
        {
            var orderItem = _orderItemService.TGetById(id);
            if (orderItem == null)
            {
                return NotFound();
            }
            var getByIdOrderItemDto = _mapper.Map<GetByIdOrderItemDto>(orderItem);
            return Ok(getByIdOrderItemDto);
        }

        [HttpPut]
        public IActionResult UpdateOrderItem(UpdateOrderItemDto updateOrderItemDto)
        {
            var validatorResult = _updateOrderItemValidator.Validate(updateOrderItemDto);

            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors);
            }
            var values = _mapper.Map<OrderItem>(updateOrderItemDto);
            _orderItemService.TUpdate(values);

            return Ok();
        }
    }
}
