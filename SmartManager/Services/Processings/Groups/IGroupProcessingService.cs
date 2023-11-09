//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using SmartManager.Models.Groups;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartManager.Services.Processings.Groups
{
    public interface IGroupProcessingService
    {
        ValueTask<Group> EnsureGroupExistsByName(string groupName);
        ValueTask<Group> EnsureGroupExistsByNameForAdd(string groupName);
        ValueTask<Group> AddGroupAsync(Group group);
        ValueTask<Group> RetrieveGroupByIdAsync(Guid groupid);
        IQueryable<Group> RetrieveAllGroups();
        ValueTask<Group> ModifyGroupAsync(Group group);
        ValueTask<Group> RemoveGroupAsync(Guid groupid);
    }
}
