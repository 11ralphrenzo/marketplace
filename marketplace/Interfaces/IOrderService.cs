using marketplace.Models;
using marketplace.Resources;

namespace marketplace.Interfaces
{
    public interface IOrderService
    {
        Task<OrderResponse> AddOrder(OrderRequest request, CancellationToken cancellationToken);
        Task<List<Order>> GetAllOrders(CancellationToken cancellationToken);
    }
}
