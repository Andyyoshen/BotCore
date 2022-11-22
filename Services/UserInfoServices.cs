using System;
using LineBuyCart.Dtos;
using LineBuyCart.Dtos.Line;
using LineBuyCart.Models;
namespace LineBuyCart.Service
{
    public class UserInfoService
    {
        private readonly ShoopingContext _db;
        public UserInfoService(ShoopingContext dbContext)
        {
            _db = dbContext;
        }

        public List<UserInfo> GetUserInfo()
        {
            var outData = _db.UserInfoies.ToList();
            return outData;
        }
        public UserInfo GetUserInfo(string lineId)
        {
            var outData = _db.UserInfoies.Where(x => x.LineId == lineId).FirstOrDefault();
            return outData;
        }
        public bool InnsertUserInfo(LineUserInfoDto lineUserInfoDto)
        {
            try
            {

                _db.UserInfoies.Add(new UserInfo()
                {
                    LineId = lineUserInfoDto.userId,
                    DisplayName = lineUserInfoDto.displayName,
                    PictureUrl = lineUserInfoDto.pictureUrl,
                    Language = lineUserInfoDto.language

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
        
    }
}