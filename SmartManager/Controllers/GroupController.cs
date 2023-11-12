//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using Microsoft.AspNetCore.Mvc;
using SmartManager.Models.Groups;
using SmartManager.Models.PaymentStatistics;
using SmartManager.Services.Processings.Groups;
using SmartManager.Services.Processings.GroupsStatistics;
using SmartManager.Services.Processings.Payments;
using SmartManager.Services.Processings.PaymentStatistics;
using SmartManager.Services.Processings.Statistics;
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
        private readonly IPaymentProcessingService paymentProcessingService;
        private readonly IPaymentStatisticsProccessingService paymentStatisticsProccessingService;
        private readonly IStatisticProcessingService statisticProcessingService;

        public GroupController(
            IGroupProcessingService groupProcessingService,
            IStudentProcessingService studentProcessingService,
            IGroupsStatisticProccessingService groupsStatisticProccessingService,
            IPaymentProcessingService paymentProcessingService,
            IPaymentStatisticsProccessingService paymentStatisticsProccessingService,
            IStatisticProcessingService statisticProcessingService)
        {
            this.groupProcessingService = groupProcessingService;
            this.studentProcessingService = studentProcessingService;
            this.groupsStatisticProccessingService = groupsStatisticProccessingService;
            this.paymentProcessingService = paymentProcessingService;
            this.paymentStatisticsProccessingService = paymentStatisticsProccessingService;
            this.statisticProcessingService = statisticProcessingService;
        }

        public IActionResult PostGroup()
        {
            return View();
        }

        [HttpPost]
        public async ValueTask<IActionResult> PostGroup(Group group)
        {
            var specificGroup = await this.groupProcessingService.EnsureGroupExistsByNameForAdd(group.GroupName);
            if (specificGroup == null)
            {
                ModelState.AddModelError("GroupName", "This group already exists.");
                return View(group);
            }

            await this.groupsStatisticProccessingService.AddGroupsStatisticAsync(specificGroup);

            return RedirectToAction("GetGroups");
        }

        public IActionResult GetGroups()
        {
            IQueryable<Group> groups = this.groupProcessingService.RetrieveAllGroups();
            IQueryable<PaymentStatistic> paymentStatistics = this.paymentStatisticsProccessingService.RetrieveAllPaymentStatistics();

            var model = new Tuple<IQueryable<Group>, IQueryable<PaymentStatistic>>(groups, paymentStatistics);

            return View(model);
        }


        public IActionResult GetGroupsForAttendances()
        {
            IQueryable<Group> groups = this.groupProcessingService.RetrieveAllGroups();

            return View(groups);
        }

        public IActionResult GetGroupsForPayments()
        {
            IQueryable<Group> groups = this.groupProcessingService.RetrieveAllGroups();
            IQueryable<PaymentStatistic> paymentStatistics = this.paymentStatisticsProccessingService.RetrieveAllPaymentStatistics();

            var model = new Tuple<IQueryable<Group>, IQueryable<PaymentStatistic>>(groups, paymentStatistics);

            return View(model);
        }

        [HttpGet]
        public async ValueTask<IActionResult> DeleteGroup(Guid groupId)
        {
            IQueryable<Group> groups = this.groupProcessingService.RetrieveAllGroups();

            Group group = groups.SingleOrDefault(a => a.Id == groupId);

            var deletedGroup = await this.groupProcessingService.RemoveGroupAsync(group.Id);

            await this.groupsStatisticProccessingService.UpdateOtherGroupsStatistics();

            await this.statisticProcessingService.AddOrUpdateStatisticAsync();

            return RedirectToAction("GetGroups");
        }
    }
}
