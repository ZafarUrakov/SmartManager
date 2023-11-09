//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using SmartManager.Brokers.Storages;
using SmartManager.Models.GroupsStatistics;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartManager.Services.Foundations.GroupsStatistics
{
    public class GroupsStatisticService : IGroupsStatisticService
    {
        private readonly IStorageBroker storageBroker;

        public GroupsStatisticService(IStorageBroker storageBroker)
        {
            this.storageBroker = storageBroker;
        }

        public async ValueTask<GroupsStatistic> AddGroupsStatisticAsync(GroupsStatistic groupsStatistic) =>
            await this.storageBroker.InsertGroupsStatisticAsync(groupsStatistic);

        public async ValueTask<GroupsStatistic> RetrieveGroupsStatisticByIdAsync(Guid groupsStatisticId) =>
            await this.storageBroker.SelectGroupsStatisticByIdAsync(groupsStatisticId);

        public IQueryable<GroupsStatistic> RetrieveAllGroupsStatistics() =>
            this.storageBroker.SelectAllGroupsStatistics();

        public async ValueTask<GroupsStatistic> ModifyGroupsStatisticAsync(GroupsStatistic groupsStatistic) =>
            await this.storageBroker.UpdateGroupsStatisticAsync(groupsStatistic);

        public async ValueTask<GroupsStatistic> RemoveGroupsStatisticAsync(Guid groupsStatisticId)
        {
            GroupsStatistic groupsStatistic =
                await this.storageBroker.SelectGroupsStatisticByIdAsync(groupsStatisticId);

            return await this.storageBroker.DeleteGroupsStatisticAsync(groupsStatistic);
        }
    }
}
