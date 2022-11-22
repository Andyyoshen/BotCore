using System;
namespace LineBuyCart.Dtos
{
    public class UserInfoDto
    {
        public int UserInfoId { get; set; }
        public string LineId { get; set; }
        public string DisplayName { get; set; }
        public string PictureUrl { get; set; }
        public string Language { get; set; }
    }
}

