//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using Xeptions;

namespace SmartManager.Models.GroupsStatistics.Exceptions
{
    public class InvalidGroupsStatisticException : Xeption
    {
        public InvalidGroupsStatisticException()
            : base(message: "GroupsStatistic is invalid.")
        { }
    }
}
