//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using System;

namespace SmartManager.Models.TelegramInformations
{
    public class TelegramInformation
    {
        public Guid Id { get; set; }
        public long TelegramId { get; set; }
        public string Message { get; set; }
        public Guid StudentId { get; set; }
    }
}
