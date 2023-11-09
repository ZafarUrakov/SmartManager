//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using SmartManager.Models.Groups;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartManager.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Group> InsertGroupAsync(Group group);
        IQueryable<Group> SelectAllGroups();
        ValueTask<Group> SelectGroupByIdAsync(Guid groupId);
        ValueTask<Group> UpdateGroupAsync(Group group);
        ValueTask<Group> DeleteGroupAsync(Group group);
    }
}
