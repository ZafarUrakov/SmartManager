//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using SmartManager.Models.Students;
using System;

namespace SmartManager.Models.Payments
{
    public class Payment
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public DateTimeOffset Date { get; set; }
        public bool IsPaid { get; set; }

        public Guid StudentId { get; set; }
        public Student Student { get; set; }
    }
}
