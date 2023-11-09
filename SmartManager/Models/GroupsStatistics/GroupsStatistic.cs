//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using SmartManager.Models.Groups;
using System;

namespace SmartManager.Models.GroupsStatistics
{
    public class GroupsStatistic
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Percentage { get; set; }

        public Guid GroupId { get; set; }
        public Group Group { get; set; }
    }
}
