//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using System;
using Xeptions;

namespace SmartManager.Models.GroupsStatistics.Exceptions
{
    public class AlreadyExistsGroupsStatisticException : Xeption
    {
        public AlreadyExistsGroupsStatisticException(Exception innerException)
            : base(message: "GroupsStatistic already exists.", innerException)
        { }
    }
}
