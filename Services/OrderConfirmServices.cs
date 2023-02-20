using System;
using LineBuyCart.Dtos.Line;
using LineBuyCart.Dtos;
using LineBuyCart.Models;
using AutoMapper;
using LineBuyCart.Enum;

namespace LineBuyCart.Services
{
    public class OrderConfirmServices
    {
        private readonly ShoopingContext _db;
        private readonly IMapper _mapper;
        public OrderConfirmServices(ShoopingContext dbContext, IMapper mapper)
        {
            _db = dbContext;
            _mapper = mapper;
        }

        public OrderConfirmDto GetByOrderConfirmId(int orderConfirmId)
        {
            
            return _mapper.Map<OrderConfirmDto>(_db.OrderConfirms.Where(x => x.OrderConfirmId == orderConfirmId).FirstOrDefault());
        }
        public OrderConfirmDto GetData(int userInfoId)
        {
            try
            {
                var result = _db.OrderConfirms.Where(x => x.UserInfoId == userInfoId)
                    .OrderByDescending(x=>x.OrderConfirmId)
                    .Take(1).FirstOrDefault();
                return _mapper.Map<OrderConfirmDto>(result);
            }
            catch(Exception err)
            {
                var message = err.Message;
                return null;
            }
        }
        public OrderConfirm Innsert(CreateOrderConfirmDto createOrderConfirmDto)
        {
            try
            {
                
                var orderconfirm = new OrderConfirm();
                var result = _mapper.Map(createOrderConfirmDto, orderconfirm);
                
                return result;
            }
            catch(Exception err)
            {
                var errorMessage = err.Message;
                return null ;
            }
        }

        public bool Update(UpdateOrderConfirmDto updateOrderConfirmDto)
        {
                var orderConfirmData = _db.OrderConfirms.Where(x => x.OrderConfirmId == updateOrderConfirmDto.OrderConfirmId).FirstOrDefault();
                var resp = _mapper.Map(updateOrderConfirmDto,orderConfirmData);
                _db.SaveChanges();
                return true;
        }
        public bool Update(UpdateOrderConfirmDto2 updateOrderConfirmDto2)
        {
            try
            {
                var orderConfirmData = _db.OrderConfirms.Where(x => x.OrderConfirmId == updateOrderConfirmDto2.OrderConfirmId).FirstOrDefault();
                var resp = _mapper.Map(updateOrderConfirmDto2, orderConfirmData);
                _db.SaveChanges();
                return true;
            }
            catch (Exception err)
            {
                var errorMessage = err.Message;
                return false;
            }
        }
    }
}

