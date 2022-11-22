using System;
namespace LineBuyCart.Models
{
    public class OrderFlow
    {
        public int OrderFlowId { get; set; }
        public int FlowStatus { get; set; }
        public int Count { get; set; }
        public string LineId { get; set; }
        public int OrderListId { get; set; }
    }
}

