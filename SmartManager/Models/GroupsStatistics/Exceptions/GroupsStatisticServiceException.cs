//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using System;
using Xeptions;

namespace SmartManager.Models.GroupsStatistics.Exceptions
{
    public class GroupsStatisticServiceException : Xeption
    {
        public GroupsStatisticServiceException(Exception innerException)
            : base(message: "GroupsStatistic service error occurred, contact support.", innerException)
        { }
    }
}
