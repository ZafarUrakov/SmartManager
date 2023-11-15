//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using Xeptions;

namespace SmartManager.Models.GroupsStatistics.Exceptions
{
    public class GroupsStatisticValidationException : Xeption
    {
        public GroupsStatisticValidationException(Xeption innerException)
            : base(message: "GroupsStatistic validation error occurred, fix the errors and try again.", innerException)
        { }
    }
}
