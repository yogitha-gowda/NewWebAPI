using OrderWebAPI.Models;

namespace OrderWebAPI.Services
{
    public interface IOrderService
    {
        Task<List<Order>> GetAllOrders();
        Task<Order?> GetOrderById(int id);
        Task<int> AddOrder(Order order);
    }
}
