using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using LineBuyCart.module;
using Newtonsoft.Json;
using System.Text.Json;
using LineBuyCart.Dtos;
using LineBuyCart.Dtos.Messages.Request;
using Newtonsoft.Json.Linq;
using isRock.LineBot;
using LineBuyCart.Providers;
using Microsoft.VisualBasic;
using LineBuyCart.Service;
using LineBuyCart.Controllers;
using LineBuyCart.Services;
using System.Web;
using System.Net.Http;
using isRock.MsQnAMaker;
using System.Text;
using LineBuyCart.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace isRock.Template
{
    public class StickerMessage
    {
        public string type { get; set; }
        public int packageId { get; set; }
        public int stickerId { get; set; }
    }
    
    public class LineWebHookController : isRock.LineBot.LineWebHookControllerBase
    {
        private readonly JsonProvider _jsonProvider = new JsonProvider();
        
        private readonly OrderListServices _orderListServices;
        private readonly ApplicationServices _applicationServices;
        private readonly UserInfoService _userInfoService;
        private readonly OrderFlowServices _orderFlowServices;
        private readonly OrderConfirmServices _orderConfirmServices;
        private readonly HttpServices _httpServices;
        private readonly ShoopingContext _db;
        public LineWebHookController(HttpServices httpServices, ShoopingContext dbContext, OrderConfirmServices orderConfirmServices,OrderFlowServices orderFlowServices, UserInfoService userInfoService,OrderListServices orderList, ApplicationServices applicationServices)
        {
            _httpServices = httpServices;
            _orderListServices = orderList;
            _applicationServices = applicationServices;
            _orderFlowServices = orderFlowServices;
            _userInfoService = userInfoService;
            _orderConfirmServices = orderConfirmServices;
            _db = dbContext;
        }

        [Route("api/LineBotWebHook")]
        [HttpPost]
        
        public async Task<IActionResult> POST()
        {

            
            var AdminUserId = "U317ef0f8f8b009e49bafcc60aa7b76cb";
            var now = DateTime.Now;
            try
            {
                //設定ChannelAccessToken
                this.ChannelAccessToken = "WRQ1ZX1TnPfteOEHrGuaEi4CDRKU5JkyHYmGLw3uS3JWrYwU3DG0fpZSyg7Kt2h8yV4FlK9lVThEm6/EvoZFSxcx778TXa2wIMflTxVtDzk+xZa6NMeoM+rc9LWK40IUImEumLLCLPECS7Zvza7AsQdB04t89/1O/w1cDnyilFU=";
                //配合Line Verify
                if (ReceivedMessage.events == null || ReceivedMessage.events.Count() <= 0 ||
                    ReceivedMessage.events.FirstOrDefault().replyToken == "00000000000000000000000000000000") return Ok();
                //取得Line Event

                var responseMsg = "";
                var LineEvent = this.ReceivedMessage.events.FirstOrDefault();

                var lineUserInfo = await _httpServices.OnGet(LineEvent.source.userId);
                var userInfo =_userInfoService.GetUserInfo(LineEvent.source.userId);
                if (userInfo == null)
                {
                    _userInfoService.InnsertUserInfo(lineUserInfo);
                }

                
                 
                
                //準備回覆訊息
                if (LineEvent.type.ToLower() == "message" && LineEvent.message.type == "text")
                {
                   
                    var orderFlow = _orderFlowServices.GetOrderFlow(LineEvent.source.userId);
                    
                    if (orderFlow != null && orderFlow.FlowStatus == 1)
                    {
                        var regexMatches = new Regex(@"^\d+$");
                        if (!regexMatches.IsMatch(LineEvent.message.text))
                        {
                            responseMsg = $"不好意思,測試紳只聽得阿拉伯數字";
                            this.ReplyMessage(LineEvent.replyToken, responseMsg);
                        }
                        else
                        {
                            var updateCountOrderFlowDto = new UpdateCountOrderFlowDto()
                            {
                                FlowStatus = 2,//暫時寫1應該要寫2
                                Count = Convert.ToInt32(LineEvent.message.text),
                                LineId = LineEvent.source.userId,
                                OrderListId = orderFlow.OrderListId

                            };
                            _orderFlowServices.Update(updateCountOrderFlowDto);
                            var orderListData = _orderListServices.GettOrder(orderFlow.OrderListId);
                            responseMsg = _applicationServices.Confiem(orderListData.Name, Convert.ToInt32(LineEvent.message.text), LineEvent.source.userId, orderListData.PictureUrl);

                            this.ReplyMessageWithJSON(LineEvent.replyToken, responseMsg);
                        }
                        
                    }
                    else if(orderFlow != null && orderFlow.FlowStatus == 3)
                    {
                        var orderConfirm = _orderConfirmServices.GetData(userInfo.UserInfoId);
                        if(orderConfirm.Status == 1)
                        {
                            var updateOrderConfirm = new UpdateOrderConfirmDto()
                            {
                                OrderConfirmId = orderConfirm.OrderConfirmId,
                                Status = 2,
                                AccountName = LineEvent.message.text,
                                ModifyDate = now
                            };
                            if (_orderConfirmServices.Update(updateOrderConfirm))
                            {
                                responseMsg = $"姓名:{LineEvent.message.text}以設定完成，接下來請輸入手機";

                            }
                            else
                            {
                                responseMsg = $"資訊有誤請聯絡管理員";
                            }

                            this.ReplyMessage(LineEvent.replyToken, responseMsg);
                            //姓名
                        }
                        if (orderConfirm.Status == 2)
                        {
                            var regexMatches = new Regex(@"^\d+$");
                            if (!regexMatches.IsMatch(LineEvent.message.text))
                            {
                                responseMsg = $"不好意思,測試紳只聽得阿拉伯數字";
                                this.ReplyMessage(LineEvent.replyToken, responseMsg);
                            }
                            else
                            {
                                var updateOrderConfirm = new UpdateOrderConfirmDto()
                                {
                                    OrderConfirmId = orderConfirm.OrderConfirmId,
                                    Status = 3,
                                    AccountName = orderConfirm.AccountName,
                                    AccountPhone = LineEvent.message.text,
                                    ModifyDate = now
                                };
                                if (_orderConfirmServices.Update(updateOrderConfirm))
                                {
                                    responseMsg = $"手機:{LineEvent.message.text}已設定完成，接下來請輸入地址";

                                }
                                else
                                {
                                    responseMsg = $"資訊有誤請聯絡管理員";
                                }

                                this.ReplyMessage(LineEvent.replyToken, responseMsg);

                            }
                            //手機
                        }
                        if (orderConfirm.Status == 3)
                        {
                            var updateOrderConfirm = new UpdateOrderConfirmDto()
                            {
                                OrderConfirmId = orderConfirm.OrderConfirmId,
                                Status = 4,
                                AccountName = orderConfirm.AccountName,
                                AccountPhone = orderConfirm.AccountPhone,
                                AccountAddress = LineEvent.message.text,
                                ModifyDate = now
                            };
                            if (_orderConfirmServices.Update(updateOrderConfirm))
                            {
                                responseMsg = _applicationServices.AccountInfo(orderConfirm.AccountName, orderConfirm.AccountPhone, LineEvent.message.text, orderConfirm.OrderConfirmId);

                                this.ReplyMessageWithJSON(LineEvent.replyToken, responseMsg);

                            }
                            else
                            {
                                responseMsg = $"資訊有誤請聯絡管理員";
                                this.ReplyMessage(LineEvent.replyToken, responseMsg);
                            }

                            
                            //地址
                        }
                    }
                    
                }
                    
                //responseMsg = $"你說了: {LineEvent.message.text}";
                if (LineEvent.type.ToLower() == "postback") {

                    var orderFlow = _orderFlowServices.GetOrderFlow(LineEvent.source.userId);
                    var orderConfirm = _orderConfirmServices.GetByOrderConfirmId(Convert.ToInt32(HttpUtility.ParseQueryString(LineEvent.postback.data)["OrderConfirmId"]));

                    if (HttpUtility.ParseQueryString(LineEvent.postback.data)["flow"] == "shopping")
                    {
                        responseMsg = _applicationServices.MargeJson();
                        this.ReplyMessageWithJSON(LineEvent.replyToken, responseMsg);
                    }
                    else if (HttpUtility.ParseQueryString(LineEvent.postback.data)["action"] == "buy")
                    {

                        var result = _orderListServices.GettOrder(Convert.ToInt32(HttpUtility.ParseQueryString(LineEvent.postback.data)["orderListId"]));
                        

                        var updateOrderFlowDto = new UpdateOrderFlowDto()
                        {
                            FlowStatus = 1,
                            LineId = LineEvent.source.userId,
                            OrderListId = result.OrderListId

                        };
                        
                        


                        if (orderFlow == null)
                        {
                            _orderFlowServices.InnsertOrderFlow(updateOrderFlowDto);
                        }
                        else
                        {
                            var update = _orderFlowServices.Update(updateOrderFlowDto);
                            _db.Entry(update).State = EntityState.Modified;
                            _db.SaveChanges();
                        }

                        responseMsg = $"想買{result.Name}，好的，您想要買幾個呢？（請輸入阿拉伯數字）";
                        this.ReplyMessage(LineEvent.replyToken, responseMsg);
                    }
                    
                    else if (HttpUtility.ParseQueryString(LineEvent.postback.data)["action"] == "confirm" && orderFlow.FlowStatus == 2)
                    {
                        var updateOederFlowDto = new UpdateOrderFlowDto()
                        {
                            FlowStatus = 3,
                            LineId = LineEvent.source.userId,
                            OrderListId = orderFlow.OrderListId

                        };
                        var orderFlowResult = _orderFlowServices.Update(updateOederFlowDto);
                        _db.Entry(orderFlowResult).State = EntityState.Modified;


                        var orderResult = _orderListServices.GettOrder(orderFlow.OrderListId);
                        var createOrderConfirmDto = new CreateOrderConfirmDto()
                        {
                            UserInfoId = userInfo.UserInfoId,
                            Count = orderFlow.Count,
                            OrderListId = orderFlow.OrderListId,
                            Status = 1,
                            CreateDate = now,
                            ModifyDate = now
                        };
                        var  insert = _orderConfirmServices.Innsert(createOrderConfirmDto);

                        _db.OrderConfirms.Add(insert);
                        _db.SaveChanges();

                        responseMsg = $"您買的{orderFlow.Count}個{orderResult.Name}已成功完成訂購，接下來請填入姓名";
                        this.ReplyMessage(LineEvent.replyToken, responseMsg);

                    }
                    else if (HttpUtility.ParseQueryString(LineEvent.postback.data)["action"] == "cancel" && orderFlow.FlowStatus == 2)
                    {
                        var updateOederFlowDto = new UpdateOrderFlowDto()
                        {
                            FlowStatus = 0,
                            LineId = LineEvent.source.userId,
                            OrderListId = orderFlow.OrderListId

                        };
                        var orderFlowResult = _orderFlowServices.Update(updateOederFlowDto);
                        if (orderFlowResult != null)
                        {
                            _db.Entry(orderFlowResult).State = EntityState.Modified;
                            _db.SaveChanges();
                            var orderResult = _orderListServices.GettOrder(orderFlow.OrderListId);
                            responseMsg = $"您買的{orderFlow.Count}個{orderResult.Name}已取消訂單";
                        }
                        else
                        {
                            responseMsg = "發生錯誤";
                        }


                        this.ReplyMessage(LineEvent.replyToken, responseMsg);

                    }
                    else if(HttpUtility.ParseQueryString(LineEvent.postback.data)["action"] == "AccountConfiem" && orderConfirm.Status == 4)
                    {
                        var updateOrderConfirm2 = new UpdateOrderConfirmDto2()
                        {
                            OrderConfirmId = orderConfirm.OrderConfirmId,
                            Status = 5,
                            ModifyDate = now
                        };
                        if (_orderConfirmServices.Update(updateOrderConfirm2))
                        {
                            responseMsg = $"聯絡資料已全部設定完成";
                            this.ReplyMessage(LineEvent.replyToken, responseMsg);
                        }
                        else
                        {
                            responseMsg = $"資訊錯誤請聯絡管理員";
                            this.ReplyMessage(LineEvent.replyToken, responseMsg);
                        }
                        
                        

                    }
                    else if (HttpUtility.ParseQueryString(LineEvent.postback.data)["action"] == "AccountCancel" && orderConfirm.Status == 4)
                    {
                        var updateOrderConfirm2 = new UpdateOrderConfirmDto2()
                        {
                            OrderConfirmId = orderConfirm.OrderConfirmId,
                            Status = 1,
                            ModifyDate = now
                        };
                        if (_orderConfirmServices.Update(updateOrderConfirm2))
                        {
                            responseMsg = $"已清除聯絡資訊，請重新填入姓名";
                            this.ReplyMessage(LineEvent.replyToken, responseMsg);
                        }
                        else
                        {
                            responseMsg = $"資訊錯誤請聯絡管理員";
                            this.ReplyMessage(LineEvent.replyToken, responseMsg);
                        }



                    }

                }



                else if (LineEvent.type.ToLower() == "message" && LineEvent.message.type == "sticker")
                {
                    var stickMsg2 = new StickerMessage
                    {
                        type = "sticker",
                        packageId = 1,
                        stickerId = 1
                    };
                    responseMsg = JsonConvert.SerializeObject(stickMsg2);

                }



                else
                {
                    responseMsg = $"收到 event : {LineEvent.type} ";
                }
                    
                var  stickMsg = new StickerMessage{
                        type = "sticker",
                        packageId = 1,
                        stickerId = 1
                    };
                       
                //回覆訊息
                
                //response OK
                return Ok();
            }
            catch (Exception ex)
            {
                //回覆訊息
                var nn = ex.Message;
                var LineEvent = this.ReceivedMessage.events.FirstOrDefault();
                //this.PushMessage(AdminUserId, "發生錯誤:\n" + ex.Message);
                //response OK
                var responseMsg = $"資訊錯誤請聯絡管理員";
                this.ReplyMessage(LineEvent.replyToken, responseMsg);
		Console.Write(nn+"ErrorMessage");
                return Ok();
            }
        }
    }
}


