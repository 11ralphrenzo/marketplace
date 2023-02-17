using marketplace.Models;

namespace marketplace.Resources;

public sealed record OrderRequest(CustomerRequest Customer, List<OrderDetailRequest> OrderDetails);
public sealed record OrderResponse(bool IsSuccess);

