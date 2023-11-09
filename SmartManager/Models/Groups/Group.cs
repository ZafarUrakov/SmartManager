//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using SmartManager.Models.PaymentStatistics;
using SmartManager.Models.Students;
using System;
using System.Collections.Generic;

namespace SmartManager.Models.Groups
{
    public class Group
    {
        public Guid Id { get; set; }
        public string GroupName { get; set; }
        public List<Student> Students { get; set; }
        public List<PaymentStatistic> PaymentStatistics { get; set; }
        public List<Models.GroupsStatistics.GroupsStatistic> GroupsStatistics { get; set; }
    }
}
