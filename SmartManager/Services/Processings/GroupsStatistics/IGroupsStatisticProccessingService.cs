//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using SmartManager.Models.Groups;
using SmartManager.Models.GroupsStatistics;
using SmartManager.Models.Students;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartManager.Services.Processings.GroupsStatistics
{
    public interface IGroupsStatisticProccessingService
    {
        ValueTask<GroupsStatistic> AddGroupsStatisticAsync(Group group);
        ValueTask<GroupsStatistic> AddGroupsStatisticsWithStudentsAsync(Student student);
        ValueTask<GroupsStatistic> RetrieveGroupsStatisticByIdAsync(Guid groupsStatisticId);
        IQueryable<GroupsStatistic> RetrieveAllGroupsStatistics();
        void ModifyGroupsStatisticAsync(Student student);
        ValueTask<GroupsStatistic> RemoveGroupsStatisticAsync(Guid groupsStatisticId);
    }
}
