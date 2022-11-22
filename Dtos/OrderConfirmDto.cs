using System;
namespace LineBuyCart.Dtos
{
    public class OrderConfirmDto
    {
        public int OrderConfirmId { get; set; }
        public int UserInfoId { get; set; }
        public int Count { get; set; }
        public int OrderListId { get; set; }
        public int? Status { get; set; }
        public string? AccountName { get; set; }
        public string? AccountPhone { get; set; }
        public string? AccountAddress { get; set; }
    }
    public class CreateOrderConfirmDto
    {
        public int UserInfoId { get; set; }
        public int Count { get; set; }
        public int OrderListId { get; set; }
        public int Status { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }

    }
    public class UpdateOrderConfirmDto
    {
        public int OrderConfirmId { get; set; }
        public int Status { get; set; }
        public string? AccountName { get; set; }
        public string? AccountPhone { get; set; }
        public string? AccountAddress { get; set; }
        public DateTime ModifyDate { get; set; }

    }
    public class UpdateOrderConfirmDto2
    {
        public int OrderConfirmId { get; set; }
        public int Status { get; set; }
        public DateTime ModifyDate { get; set; }

    }

}

