//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using System;
using Xeptions;

namespace SmartManager.Models.GroupsStatistics.Exceptions
{
    public class LockedGroupsStatisticException : Xeption
    {
        public LockedGroupsStatisticException(Exception innerException)
            : base(message: "GroupsStatistic is locked, please try again.", innerException)
        { }
    }
}
