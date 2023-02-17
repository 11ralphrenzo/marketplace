using marketplace.Data;
using marketplace.Interfaces;
using marketplace.Models;
using marketplace.Resources;
using Microsoft.EntityFrameworkCore;

namespace marketplace.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDBContext context;
        private readonly ICustomerService customerService;

        public OrderService(AppDBContext context, ICustomerService customerService)
        {
            this.context = context;
            this.customerService = customerService;
        }

        public async Task<OrderResponse> AddOrder(OrderRequest request, CancellationToken cancellationToken)
        {
            var newOrder = new Order
            {
                OrderPlaced = DateTime.Now,
                CustomerId = request.Customer.Id,
            };

            if (request.OrderDetails.Any())
            {
                newOrder.OrderDetails = new List<OrderDetail>();
                foreach (var orderDetail in request.OrderDetails)
                {
                    newOrder.OrderDetails
                        .Add(new OrderDetail
                        {
                            ProductId = orderDetail.ProductId,
                            Quantity = orderDetail.Quantity
                        });
                }
            }

            var result = await context.Orders
                .AddAsync(newOrder);
            await context.SaveChangesAsync();

            return new OrderResponse(result.Entity is not null);
        }

        public async Task<List<Order>> GetAllOrders(CancellationToken cancellationToken)
        {
            return await context.Orders
                .OrderByDescending(o => o.OrderPlaced)
                .ToListAsync();
        }
    }
}
