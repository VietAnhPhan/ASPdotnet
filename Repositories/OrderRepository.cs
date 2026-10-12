using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Entities;

namespace WebApplication1.Repositories
{
    public class OrderRepository(AppDbContext db) : IOrderRepository
    {
        public Task<List<Order>> GetAllAsync()
        {
            return db.Orders
                .AsNoTracking()
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }
    }
}
