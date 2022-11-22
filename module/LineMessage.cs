using System;
namespace LineBuyCart.module
{
    public class LineMessage
    {
        public string type { get; set; }
        public string altText { get; set; }
        public string contents { get; set; }
    }

    public class RootObject
    {
       public List<LineMessage> items { get; set; }
    }
}


