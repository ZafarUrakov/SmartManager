//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using SmartManager.Models.GroupsStatistics;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartManager.Services.Foundations.GroupsStatistics
{
    public interface IGroupsStatisticService
    {
        ValueTask<GroupsStatistic> AddGroupsStatisticAsync(GroupsStatistic groupsStatistic);
        ValueTask<GroupsStatistic> RetrieveGroupsStatisticByIdAsync(Guid groupsStatisticId);
        IQueryable<GroupsStatistic> RetrieveAllGroupsStatistics();
        ValueTask<GroupsStatistic> ModifyGroupsStatisticAsync(GroupsStatistic groupsStatistic);
        ValueTask<GroupsStatistic> RemoveGroupsStatisticAsync(Guid groupsStatisticId);
    }
}
