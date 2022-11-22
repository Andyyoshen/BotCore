using System;
using LineBuyCart.Dtos;
using LineBuyCart.Models;
using LineBuyCart.Providers;
using LineBuyCart.Service;

namespace LineBuyCart.Services
{
    public class ApplicationServices
    {
        private readonly JsonProvider _jsonProvider = new JsonProvider();
        private readonly OrderListServices _orderListServices;
        public ApplicationServices(OrderListServices _orderList)
        {
            _orderListServices = _orderList;
        }

        /// <summary>
        /// 詢問會員資訊
        /// </summary>
        /// <param name="accountName"></param>
        /// <param name="accountPhone"></param>
        /// <param name="accountAddress"></param>
        /// <returns></returns>
        public string AccountInfo(string accountName, string accountPhone, string accountAddress, int OrderConfirmId)
        {
            var Contents = new List<FlexBubbleContainerDto>();
            Contents.Add(new FlexBubbleContainerDto()
            {
                Type = "bubble",
                Hero = new FlexComponentDto
                {
                    Type = "image",
                    Url = "https://scdn.line-apps.com/n/channel_devcenter/img/fx/01_1_cafe.png",
                    Size = "full",
                    AspectRatio = "20:13",
                    AspectMode = "cover"
                },
                Body = new FlexComponentDto
                {
                    Type = "box",
                    Layout = "vertical",
                    Contents = new List<FlexComponentDto>
                    {
                        new FlexComponentDto()
                        {
                             Type = "text",
                                Text ="請確認以下聯絡資訊是否正確",
                                Weight = "bold",
                                Size = "xl",
                        },
                        new FlexComponentDto()
                        {
                            Type ="box",
                            Layout = "vertical",
                            Margin = "lg",
                            Spacing = "sm",
                            Contents = new List<FlexComponentDto>
                            {
                                new FlexComponentDto()
                                {
                                    Type ="box",
                                    Layout = "baseline",
                                    Spacing = "sm",
                                    Contents =new List<FlexComponentDto>
                                    {
                                        new FlexComponentDto()
                                        {
                                            Type ="text",
                                            Text = "姓名",
                                            Color = "#aaaaaa",
                                            Size = "sm",
                                            Flex = 1
                                        },
                                        new FlexComponentDto()
                                        {
                                            Type ="text",
                                            Text = accountName,
                                            Wrap = true,
                                            Color = "#666666",
                                            Size = "sm",
                                            Flex = 5
                                        }
                                    }
                                },
                                new FlexComponentDto()
                                {
                                    Type ="box",
                                    Layout = "baseline",
                                    Spacing = "sm",
                                    Contents =new List<FlexComponentDto>
                                    {
                                        new FlexComponentDto()
                                        {
                                            Type ="text",
                                            Text = "地址",
                                            Color = "#aaaaaa",
                                            Size = "sm",
                                            Flex = 1
                                        },
                                        new FlexComponentDto()
                                        {
                                            Type ="text",
                                            Text = accountAddress,
                                            Wrap = true,
                                            Color = "#666666",
                                            Size = "sm",
                                            Flex = 5
                                        }
                                    }
                                },
                                new FlexComponentDto()
                                {
                                    Type ="box",
                                    Layout = "baseline",
                                    Spacing = "sm",
                                    Contents =new List<FlexComponentDto>
                                    {
                                        new FlexComponentDto()
                                        {
                                            Type ="text",
                                            Text = "電話",
                                            Color = "#aaaaaa",
                                            Size = "sm",
                                            Flex = 1
                                        },
                                        new FlexComponentDto()
                                        {
                                            Type ="text",
                                            Text = accountPhone,
                                            Wrap = true,
                                            Color = "#666666",
                                            Size = "sm",
                                            Flex = 5
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                Footer = new FlexComponentDto
                {
                    Type = "box",
                    Layout = "vertical",
                    Spacing = "sm",
                    Contents = new List<FlexComponentDto>
                    {
                        new FlexComponentDto()
                        {
                            Type = "button",
                            Style ="link",
                            Height ="sm",
                            Action = new ActionDto
                            {
                                Type = "postback",
                                Label = "確定",
                                Data = $"action=AccountConfiem&OrderConfirmId={OrderConfirmId}"
                            }
                        },
                        new FlexComponentDto()
                        {
                            Type = "button",
                            Style ="link",
                            Height ="sm",
                            Action = new ActionDto
                            {
                                Type = "postback",
                                Label = "取消",
                                Data = $"action=AccountCancel&OrderConfirmId={OrderConfirmId}"
                            }
                        }

                    },
                    Flex = 0
                }
            });
            var Menu = new List<FlexMessageDto<FlexCarouselContainerDto>>();
            Menu.Add(new FlexMessageDto<FlexCarouselContainerDto>
            {


                Type = "flex",
                AltText = "This is flex message",
                Contents = new FlexCarouselContainerDto
                {
                    Type = "carousel",
                    Contents = Contents
                }
            });
            var json = _jsonProvider.Serialize(Menu);
            return json;
        }

        /// <summary>
        /// 詢問是否訂單
        /// </summary>
        /// <param name="name"></param>
        /// <param name="count"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string Confiem(string name, int count, string userId,string pictureUrl)
        {
            var Contents = new List<FlexBubbleContainerDto>();
            Contents.Add(new FlexBubbleContainerDto()
            {
                Type = "bubble",
                Hero = new FlexComponentDto
                {
                    Type = "image",
                    Url = pictureUrl,
                    Size = "full",
                    AspectRatio = "20:13",
                    AspectMode = "cover"
                },
                Body = new FlexComponentDto
                {
                    Type = "box",
                    Layout = "vertical",
                    Contents = new List<FlexComponentDto>
                    {
                        new FlexComponentDto()
                        {
                             Type = "text",
                                Text ="Brown Cafe",
                                Weight = "bold",
                                Size = "xl",
                        },
                        new FlexComponentDto()
                        {
                            Type ="box",
                            Layout = "vertical",
                            Margin = "lg",
                            Spacing = "sm",
                            Contents = new List<FlexComponentDto>
                            {
                                new FlexComponentDto()
                                {
                                    Type ="box",
                                    Layout = "baseline",
                                    Spacing = "sm",
                                    Contents =new List<FlexComponentDto>
                                    {
                                        new FlexComponentDto()
                                        {
                                            Type ="text",
                                            Text = "place",
                                            Color = "#aaaaaa",
                                            Size = "sm",
                                            Flex = 1
                                        },
                                        new FlexComponentDto()
                                        {
                                            Type ="text",
                                            Text = $"妳想要夠買{name}{count}個，對吧？",
                                            Wrap = true,
                                            Color = "#666666",
                                            Size = "sm",
                                            Flex = 5
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                Footer = new FlexComponentDto
                {
                    Type = "box",
                    Layout = "vertical",
                    Spacing = "sm",
                    Contents = new List<FlexComponentDto>
                    {
                        new FlexComponentDto()
                        {
                            Type = "button",
                            Style ="link",
                            Height ="sm",
                            Action = new ActionDto
                            {
                                Type = "postback",
                                Label = "確定",
                                Data = $"action=confirm&orderListId={userId}"
                            }
                        },
                        new FlexComponentDto()
                        {
                            Type = "button",
                            Style ="link",
                            Height ="sm",
                            Action = new ActionDto
                            {
                                Type = "postback",
                                Label = "取消",
                                Data = $"action=cancel&orderListId={userId}"
                            }
                        }

                    },
                    Flex = 0
                }
            });
            var Menu = new List<FlexMessageDto<FlexCarouselContainerDto>>();
            Menu.Add(new FlexMessageDto<FlexCarouselContainerDto>
            {


                Type = "flex",
                AltText = "This is flex message",
                Contents = new FlexCarouselContainerDto
                {
                    Type = "carousel",
                    Contents = Contents
                }
            });
            var json = _jsonProvider.Serialize(Menu);
            return json;
        }

        /// <summary>
        /// 顯示商品資訊
        /// </summary>
        /// <returns></returns>
        public string   MargeJson()
        {
            var Lists = _orderListServices.GettOrder();
            var Contents = new List<FlexBubbleContainerDto>();

            foreach (var item in Lists)
            {
                Contents.Add(new FlexBubbleContainerDto()
                {
                    Type = "bubble",
                    Size = "micro",
                    Hero = new FlexComponentDto
                    {
                        Type = "image",
                        Url = item.PictureUrl,
                        Size = "full",
                        AspectMode = "cover",
                        AspectRatio = "320:213"
                    },
                    Body = new FlexComponentDto
                    {
                        Type = "box",
                        Layout = "vertical",
                        Contents = new List<FlexComponentDto>
                        {
                            new FlexComponentDto
                            {
                                Type = "text",
                                Text =item.Name,
                                Weight = "bold",
                                Size = "sm",
                                Wrap = true
                            },
                            new FlexComponentDto
                            {
                                Type = "box",
                                Layout = "baseline",
                                Contents = new List<FlexComponentDto>
                                {
                                    new FlexComponentDto
                                    {
                                        Type = "icon",
                                        Size ="xs",
                                        Url = "https://scdn.line-apps.com/n/channel_devcenter/img/fx/review_gold_star_28.png"
                                    },
                                    new FlexComponentDto
                                    {
                                        Type ="text",
                                          Text = "4.0",
                                          Size = "xs",
                                          Color = "#8c8c8c",
                                          Margin = "md",
                                          Flex = 0
                                     }
                                }
                            },
                            new FlexComponentDto
                            {
                                 Type ="box",
                                 Layout = "vertical",
                                 Contents = new List<FlexComponentDto>
                                 {
                                                        new FlexComponentDto
                                                        {
                                                            Type ="box",
                                                            Layout = "baseline",
                                                            Spacing ="sm",
                                                            Contents = new List<FlexComponentDto>
                                                            {
                                                                new FlexComponentDto
                                                                {
                                                                    Type = "text",
                                                                    Text = item.Ｄescribe,
                                                                    Wrap = true,
                                                                    Color  ="#8c8c8c",
                                                                    Size = "xs",
                                                                    Flex = 5
                                                                }
                                                            }
                                                        }
                                                    }
                            }

                        },
                    },
                    Footer = new FlexComponentDto
                    {
                        Type = "box",
                        Layout = "horizontal",
                        Contents = new List<FlexComponentDto>
                                            {
                                                new FlexComponentDto
                                                {
                                                    Type = "button",
                                                    Style = "primary",
                                                    Action = new ActionDto
                                                    {
                                                        Type = "postback",
                                                        Label = "我要下訂單",
                                                        Data  = $"action=buy&orderListId={item.OrderListId}"
                                                    }

                                                    }

                                                }
                    }

                });
            }



            var Menu = new List<FlexMessageDto<FlexCarouselContainerDto>>();
            Menu.Add(new FlexMessageDto<FlexCarouselContainerDto>
            {


                Type = "flex",
                AltText = "This is flex message",
                Contents = new FlexCarouselContainerDto
                {
                    Type = "carousel",
                    Contents = Contents
                }
            });
            var json = _jsonProvider.Serialize(Menu);
            return json;
        }
    }
}

