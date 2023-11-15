//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using Xeptions;

namespace SmartManager.Models.GroupsStatistics.Exceptions
{
    public class GroupsStatisticDependencyValidationException : Xeption
    {
        public GroupsStatisticDependencyValidationException(Xeption innerException)
           : base(message: "GroupsStatistic dependency validation error occurred, fix the errors and try again.",
                 innerException)
        { }
    }
}
