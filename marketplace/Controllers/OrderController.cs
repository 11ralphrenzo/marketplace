using marketplace.Interfaces;
using marketplace.Models;
using marketplace.Resources;
using marketplace.Responses;
using marketplace.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace marketplace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        // POST api/<OrderController>
        [HttpPost]
        public async Task<ActionResult<OrderResponse>> Post([FromBody] OrderRequest request, CancellationToken cancellationToken)
        {
            var errors = new ErrorResponse();
            try
            {
                var response = await orderService.AddOrder(request, cancellationToken);
                return Ok(response);
            }
            catch (Exception e)
            {
                errors.Messages.Add(e.Message);
                return BadRequest(errors);
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<Order>>> Get(CancellationToken cancellationToken)
        {
            var errors = new ErrorResponse();
            try
            {
                var response = await orderService.GetAllOrders(cancellationToken);
                return Ok(response);
            }
            catch (Exception e)
            {
                errors.Messages.Add(e.Message);
                return BadRequest(errors);
            }
        }
    }
}
