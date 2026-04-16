using Microsoft.AspNetCore.Mvc;
using OrderWebAPI.Models;
using OrderWebAPI.Services;

namespace OrderWebAPI.Controllers
{
    [ApiController]
    [Route("api/Order")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _service;

        public OrderController(IOrderService service)
        {
            _service = service;
        }

        [HttpGet("GetAllOrders")]
        public async Task<IActionResult> GetAllOrders()
        {
            var result = await _service.GetAllOrders();
            return Ok(result);
        }

        [HttpGet("GetOrderById/{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var result = await _service.GetOrderById(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost("AddOrder")]
        public async Task<IActionResult> AddOrder([FromBody] Order order)
        {
            //  IMPORTANT FIX: break model validation chain
            if (order.OrderItems != null)
            {
                foreach (var item in order.OrderItems)
                {
                    item.Order = null;
                }
            }

            var id = await _service.AddOrder(order);
            return Ok(id);
        }
    }
}