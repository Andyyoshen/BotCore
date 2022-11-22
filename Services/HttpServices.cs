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
        public HttpServices(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
       
        public async Task<LineUserInfoDto> OnGet(string userId)
        {
                var resultModle = new LineUserInfoDto();
                var httpClient = _httpClientFactory.CreateClient();
                //httpClient.BaseAddress = new Uri("");
                httpClient.DefaultRequestHeaders.Add("authorization", "Bearer WRQ1ZX1TnPfteOEHrGuaEi4CDRKU5JkyHYmGLw3uS3JWrYwU3DG0fpZSyg7Kt2h8yV4FlK9lVThEm6/EvoZFSxcx778TXa2wIMflTxVtDzk+xZa6NMeoM+rc9LWK40IUImEumLLCLPECS7Zvza7AsQdB04t89/1O/w1cDnyilFU=");
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

