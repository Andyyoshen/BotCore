using System;
using System.Net.Http;
using isRock.MsQnAMaker;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using LineBuyCart.Dtos.Line;

namespace LineBuyCart.Services
{
    public class HttpServices
    {

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public HttpServices(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;

        }
       
        public async Task<LineUserInfoDto> OnGet(string userId)
        {
                var resultModle = new LineUserInfoDto();
                var httpClient = _httpClientFactory.CreateClient();
                var ChannelAccessToken = _configuration.GetValue<string>("Chanel:ChannelAccessToken");
                //httpClient.BaseAddress = new Uri("");
            httpClient.DefaultRequestHeaders.Add("authorization", $"Bearer {ChannelAccessToken}");
                var httpResponseMessage = await httpClient.GetAsync($"https://api.line.me/v2/bot/profile/{userId}");
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                        var contentStream = await httpResponseMessage.Content.ReadAsStringAsync();
                        resultModle = JsonConvert.DeserializeObject<LineUserInfoDto>(contentStream);
                
                }
                return resultModle;


        }
    }
}

