//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using Microsoft.EntityFrameworkCore;
using SmartManager.Models.GroupsStatistics;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartManager.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<GroupsStatistic> GroupsStatistics { get; set; }

        public async ValueTask<GroupsStatistic> InsertGroupsStatisticAsync(GroupsStatistic groupsStatistic) =>
           await InsertAsync(groupsStatistic);

        public IQueryable<GroupsStatistic> SelectAllGroupsStatistics() =>
            SelectAll<GroupsStatistic>().AsQueryable();

        public async ValueTask<GroupsStatistic> SelectGroupsStatisticByIdAsync(Guid groupsStatisticId) =>
            await SelectAsync<GroupsStatistic>(groupsStatisticId);

        public async ValueTask<GroupsStatistic> UpdateGroupsStatisticAsync(GroupsStatistic groupsStatistic) =>
            await UpdateAsync(groupsStatistic);

        public async ValueTask<GroupsStatistic> DeleteGroupsStatisticAsync(GroupsStatistic groupsStatistic) =>
            await DeleteAsync(groupsStatistic);
    }
}
