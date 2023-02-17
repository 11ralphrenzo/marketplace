using marketplace.Models;

namespace marketplace.Interfaces
{
    public interface ICustomerService
    {
        Task<Customer> AddCustomer(User user, CancellationToken cancellationToken, bool shouldSave = true);
        Task<Customer?> GetCustomerById(int id);
    }
}
