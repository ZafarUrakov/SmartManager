//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using System;

namespace SmartManager.Models.Bots
{
    public class StudentTelegramInformation
    {
        public Guid Id { get; set; }
        public long TelegramId {  get; set; }
        public string Message { get; set; }
    }
}
