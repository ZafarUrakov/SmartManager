//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using SmartManager.Models.Groups;
using System;

namespace SmartManager.Models.PaymentStatistics
{
    public class PaymentStatistic
    {
        public Guid Id { get; set; }
        public decimal PaidPercentage { get; set; }
        public decimal NotPaidPercentage { get; set; }

        public Guid GroupId { get; set; }
        public Group Group { get; set; }
    }
}
