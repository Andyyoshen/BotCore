using System;
using AutoMapper;
using LineBuyCart.Models;
using LineBuyCart.Dtos;
namespace LineBuyCart.Handler
{
    public class AutoMapperHandler:Profile
    {
        public AutoMapperHandler()
        {
            CreateMap<OrderFlow, OrderFlowDto>();
            CreateMap<OrderFlowDto, OrderFlow>();

            CreateMap<OrderFlow, UpdateOrderFlowDto>();
            CreateMap<UpdateOrderFlowDto, OrderFlow>();

            CreateMap<OrderFlow, UpdateCountOrderFlowDto>();
            CreateMap<UpdateCountOrderFlowDto, OrderFlow>();

            

            CreateMap<OrderConfirm, OrderConfirmDto>();
            CreateMap<OrderConfirmDto, OrderConfirm>();

            CreateMap<OrderConfirm, CreateOrderConfirmDto>();
            CreateMap<CreateOrderConfirmDto, OrderConfirm>();

            CreateMap<OrderConfirm, UpdateOrderConfirmDto>();
            CreateMap<UpdateOrderConfirmDto, OrderConfirm>();

            CreateMap<OrderConfirm, UpdateOrderConfirmDto2>();
            CreateMap<UpdateOrderConfirmDto2, OrderConfirm>();



        }
    }
}

