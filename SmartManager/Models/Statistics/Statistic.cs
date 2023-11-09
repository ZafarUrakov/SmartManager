//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using SmartManager.Models.Students;
using System;
using System.Collections.Generic;

namespace SmartManager.Models.Statistics
{
    public class Statistic
    {
        public Guid Id { get; set; }

        public int MaleStudentsCount { get; set; }
        public int FemaleStudentsCount { get; set; }
        public int OtherStudentsCount { get; set; }
        public int UnknownStudentsCount { get; set; }

        public int TotalStudentsCount { get; set; }
        public int PaidStudentsCount { get; set; }
        public decimal PaidStudentsPercentage { get; set; }

        public decimal TotalPayment { get; set; }

        public List<Student> Students { get; set; }
    }
}
