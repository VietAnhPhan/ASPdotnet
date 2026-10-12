using WebApplication1.Entities;

namespace WebApplication1.Repositories
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAllAsync();
    }
}
