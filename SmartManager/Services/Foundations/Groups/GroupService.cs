//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using SmartManager.Brokers.Storages;
using SmartManager.Models.Groups;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartManager.Services.Foundations.Groups
{
    public class GroupService : IGroupService
    {
        private readonly IStorageBroker storageBroker;

        public GroupService(IStorageBroker storageBroker)
        {
            this.storageBroker = storageBroker;
        }

        public async ValueTask<Group> AddGroupAsync(Group group) =>
            await this.storageBroker.InsertGroupAsync(group);

        public async ValueTask<Group> RetrieveGroupByIdAsync(Guid groupid) =>
            await this.storageBroker.SelectGroupByIdAsync(groupid);

        public IQueryable<Group> RetrieveAllGroups() =>
            this.storageBroker.SelectAllGroups();

        public async ValueTask<Group> ModifyGroupAsync(Group group) =>
            await this.storageBroker.UpdateGroupAsync(group);

        public async ValueTask<Group> RemoveGroupAsync(Guid groupid)
        {
            var maybeGroup = await this.storageBroker.SelectGroupByIdAsync(groupid);

            return await this.storageBroker.DeleteGroupAsync(maybeGroup);
        }
    }
}
