using Microsoft.EntityFrameworkCore;
using OrderWebAPI.Models;

namespace OrderWebAPI.Services
{
    public class OrderService : IOrderService
    {
        private readonly OrderDbContext _context;

        public OrderService(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetAllOrders()
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .ToListAsync();
        }

        public async Task<Order?> GetOrderById(int id)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.OrderId == id);
        }

        public async Task<int> AddOrder(Order order)
        {
            //  IMPORTANT FIX (break circular reference)
            if (order.OrderItems != null)
            {
                foreach (var item in order.OrderItems)
                {
                    item.Order = null;
                }
            }

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return order.OrderId;
        }
    }
}