//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using SmartManager.Models.GroupsStatistics;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartManager.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<GroupsStatistic> InsertGroupsStatisticAsync(GroupsStatistic groupsStatistic);
        IQueryable<GroupsStatistic> SelectAllGroupsStatistics();
        ValueTask<GroupsStatistic> SelectGroupsStatisticByIdAsync(Guid groupsStatisticId);
        ValueTask<GroupsStatistic> UpdateGroupsStatisticAsync(GroupsStatistic groupsStatistic);
        ValueTask<GroupsStatistic> DeleteGroupsStatisticAsync(GroupsStatistic groupsStatistic);
    }
}
