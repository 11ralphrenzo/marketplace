using marketplace.Data;
using marketplace.Interfaces;
using marketplace.Models;
using Microsoft.EntityFrameworkCore;

namespace marketplace.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly AppDBContext context;

        public CustomerService(AppDBContext context)
        {
            this.context = context;
        }

        public async Task<Customer> AddCustomer(User user, CancellationToken cancellationToken, bool shouldSave = true)
        {
            var result = await context.Customers
                .AddAsync(
                    new Customer
                    {
                        User = user
                    }, 
                    cancellationToken);
            if(shouldSave)
                await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Customer?> GetCustomerById(int id)
        {
            return await context.Customers.FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
