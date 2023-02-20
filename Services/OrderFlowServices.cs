using System;
using LineBuyCart.Dtos.Line;
using LineBuyCart.Dtos;
using LineBuyCart.Models;
using AutoMapper;

namespace LineBuyCart.Services
{
    public class OrderFlowServices
    {
        private readonly ShoopingContext _db;
        private readonly IMapper _mapper;
        public OrderFlowServices(ShoopingContext dbContext,IMapper mapper)
        {
            _db = dbContext;
            _mapper = mapper;
        }

        public List<OrderFlowDto> GetOrderFlow()
        {
            var outData = _db.OrderFlows.Select(x => new OrderFlowDto
            {
                OrderFlowId = x.OrderFlowId,
                FlowStatus = x.FlowStatus,
                Count = x.Count,
                LineId = x.LineId,
                OrderListId = x.OrderListId
            }).ToList();
            return outData;
        }
        public OrderFlowDto GetOrderFlow(string lineId)
        {
            var outData = _db.OrderFlows
                .Where(x=>x.LineId == lineId)
                .Select(x => new OrderFlowDto
            {
                OrderFlowId = x.OrderFlowId,
                FlowStatus = x.FlowStatus,
                Count = x.Count,
                LineId = x.LineId,
                OrderListId = x.OrderListId
            }).FirstOrDefault();
            return outData;
        }
        public bool  InnsertOrderFlow(UpdateOrderFlowDto updateOrederFlowDto)
        {
            try
            {

                _db.OrderFlows.Add(new OrderFlow()
                {
                    FlowStatus = updateOrederFlowDto.FlowStatus,
                    LineId = updateOrederFlowDto.LineId,
                    OrderListId = updateOrederFlowDto.OrderListId

                });
                _db.SaveChanges();
                return true;
            }
            catch (Exception err)
            {
                var errorMessage = err.Message;
                return false;
            }

        }
        public bool UpdateOrderFlow(OrderFlowDto orederFlowDto)
        {
            try
            {
                var result  = _db.OrderFlows.Where(x => x.LineId == orederFlowDto.LineId).FirstOrDefault();
                result.FlowStatus = orederFlowDto.FlowStatus;
                result.Count = orederFlowDto.Count;
                result.OrderListId = orederFlowDto.OrderListId;
                _db.SaveChanges();
                return true;
            }
            catch (Exception err)
            {
                var errorMessage = err.Message;
                return false;
            }

        }
        public OrderFlow Update(UpdateOrderFlowDto updateOrederFlowDto)
        {
            try
            {
                var result = _db.OrderFlows.Where(x => x.LineId == updateOrederFlowDto.LineId).FirstOrDefault();
                var resp = _mapper.Map(updateOrederFlowDto, result);
                
                return resp;
            }
            catch (Exception err)
            {
                var errorMessage = err.Message;
                return null;
            }

        }
        public bool Update(UpdateCountOrderFlowDto updateCountOrderFlowDto)
        {
                var result = _db.OrderFlows.Where(x => x.LineId == updateCountOrderFlowDto.LineId).FirstOrDefault();
                var resp = _mapper.Map(updateCountOrderFlowDto, result);
                _db.SaveChanges();
                return true;

        }



    }
}


