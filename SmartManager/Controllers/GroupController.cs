//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using Microsoft.AspNetCore.Mvc;
using SmartManager.Models.Groups;
using SmartManager.Services.Processings.Groups;
using SmartManager.Services.Processings.GroupsStatistics;
using SmartManager.Services.Processings.Students;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartManager.Controllers
{
    public class GroupController : Controller
    {
        private readonly IGroupProcessingService groupProcessingService;
        private readonly IStudentProcessingService studentProcessingService;
        private readonly IGroupsStatisticProccessingService groupsStatisticProccessingService;

        public GroupController(
            IGroupProcessingService groupProcessingService,
            IStudentProcessingService studentProcessingService,
            IGroupsStatisticProccessingService groupsStatisticProccessingService)
        {
            this.groupProcessingService = groupProcessingService;
            this.studentProcessingService = studentProcessingService;
            this.groupsStatisticProccessingService = groupsStatisticProccessingService;
        }

        public IActionResult PostGroup()
        {
            return View();
        }

        [HttpPost]
        public async ValueTask<IActionResult> PostGroup(Group group)
        {
            if (await this.groupProcessingService.EnsureGroupExistsByNameForAdd(group.GroupName) == null)
            {
                ModelState.AddModelError("GroupName", "This group already exists.");
                return View(group);
            }

            //await this.groupsStatisticProccessingService.AddGroupsStatisticAsync(group);

            return RedirectToAction("GetGroups");
        }

        public IActionResult GetGroups()
        {
            IQueryable<Group> groups = this.groupProcessingService.RetrieveAllGroups();

            return View(groups);
        }

        public IActionResult GetGroupsForAttendances()
        {
            IQueryable<Group> groups = this.groupProcessingService.RetrieveAllGroups();

            return View(groups);
        }

        public IActionResult GetGroupsForPayments()
        {
            IQueryable<Group> groups = this.groupProcessingService.RetrieveAllGroups();

            return View(groups);
        }

        [HttpGet]
        public async ValueTask<IActionResult> DeleteGroup(Guid groupId)
        {
            IQueryable<Group> groups = this.groupProcessingService.RetrieveAllGroups();

            Group group = groups.SingleOrDefault(a => a.Id == groupId);

            await this.groupProcessingService.RemoveGroupAsync(group.Id);

            return RedirectToAction("GetGroups");
        }
    }
}
