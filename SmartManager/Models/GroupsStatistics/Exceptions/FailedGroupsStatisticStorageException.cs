//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using System;
using Xeptions;

namespace SmartManager.Models.GroupsStatistics.Exceptions
{
    public class FailedGroupsStatisticStorageException : Xeption
    {
        public FailedGroupsStatisticStorageException(Exception innerException)
            : base(message: "Failed groupsStatistic storage error occurred, contact support.", innerException)
        { }
    }
}
