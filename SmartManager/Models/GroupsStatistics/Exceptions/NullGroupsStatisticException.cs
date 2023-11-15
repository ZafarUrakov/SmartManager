//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using Xeptions;

namespace SmartManager.Models.GroupsStatistics.Exceptions
{
    public class NullGroupsStatisticException : Xeption
    {
        public NullGroupsStatisticException()
            : base(message: "GroupsStatistic is null.")
        { }
    }
}
