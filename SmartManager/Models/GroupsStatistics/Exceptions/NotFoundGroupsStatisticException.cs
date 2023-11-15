//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using System;
using Xeptions;

namespace SmartManager.Models.GroupsStatistics.Exceptions
{
    public class NotFoundGroupsStatisticException : Xeption
    {
        public NotFoundGroupsStatisticException(Guid GroupsStatisticId)
            : base(message: $"Couldn't find groupsStatistic with id {GroupsStatisticId}.")
        { }
    }
}
