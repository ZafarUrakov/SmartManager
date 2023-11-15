//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using System;
using Xeptions;

namespace SmartManager.Models.GroupsStatistics.Exceptions
{
    public class GroupsStatisticDependencyException : Xeption
    {
        public GroupsStatisticDependencyException(Exception innerException)
            : base(message: "GroupsStatistic dependency error occurred, contact support.", innerException)
        { }
    }
}
