﻿using System;
namespace LineBuyCart.Dtos.Messages.Request
{
    public class BroadcastMessageRequestDto<T>
    {
        public List<T> Messages { get; set; }
        public bool? NotificationDisabled { get; set; }
    }
}

