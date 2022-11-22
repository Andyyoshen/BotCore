using System;
namespace LineBuyCart.Dtos
{
    public class OrderFlowDto
    {
        public int OrderFlowId { get; set; }
        public int FlowStatus { get; set; }
        public int Count { get; set; }
        public string LineId { get; set; }
        public int OrderListId { get; set; }
    }
    public class UpdateOrderFlowDto
    {
        public int FlowStatus { get; set; }
        public string LineId { get; set; }
        public int OrderListId { get; set; }
    }

    public class UpdateCountOrderFlowDto
    {
        public int FlowStatus { get; set; }
        public int Count { get; set; }
        public string LineId { get; set; }
        public int OrderListId { get; set; }
    }
}

