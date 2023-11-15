//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using System;
using Xeptions;

namespace SmartManager.Models.GroupsStatistics.Exceptions
{
    public class FailedGroupsStatisticServiceException : Xeption
    {
        public FailedGroupsStatisticServiceException(Exception innerException)
            : base(message: "Failed groupsStatistic service error occurred, please contact support.", innerException)
        { }
    }
}
