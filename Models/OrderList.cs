using System;
using System.Collections.Generic;

namespace LineBuyCart.Models
{
    public partial class OrderList
    {
        public int OrderListId { get; set; }
        public string Name { get; set; } = null!;
        public int Price { get; set; }
        public string? Ｄescribe { get; set; }
        public string? PictureUrl { get; set; }
    }
}
