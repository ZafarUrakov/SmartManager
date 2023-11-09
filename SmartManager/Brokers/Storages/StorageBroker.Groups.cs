//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using Microsoft.EntityFrameworkCore;
using SmartManager.Models.Groups;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartManager.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Group> Groups { get; set; }

        public async ValueTask<Group> InsertGroupAsync(Group group) =>
           await InsertAsync(group);

        public IQueryable<Group> SelectAllGroups() =>
            SelectAll<Group>().AsQueryable();

        public async ValueTask<Group> SelectGroupByIdAsync(Guid groupId) =>
            await SelectAsync<Group>(groupId);

        public async ValueTask<Group> UpdateGroupAsync(Group group) =>
            await UpdateAsync(group);

        public async ValueTask<Group> DeleteGroupAsync(Group group)
        {
            return await DeleteAsync(group);
        }
    }
}
