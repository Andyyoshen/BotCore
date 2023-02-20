using System;
using LineBuyCart.Models;
namespace LineBuyCart.Service
{
    public class OrderListServices
    {
        private readonly ShoopingContext _db;
        public OrderListServices(ShoopingContext dbContext)
        {
            _db = dbContext;
        }

        public List<OrderList> GettOrder()
        {
            var outData = _db.OrderLists.ToList();
            return outData;
        }
        public OrderList GettOrder(int orderListId)
        {
               var outData = new OrderList();
                outData = _db.OrderLists.Where(x => x.OrderListId == orderListId).FirstOrDefault();
                return outData;
            
        }
    }
}

