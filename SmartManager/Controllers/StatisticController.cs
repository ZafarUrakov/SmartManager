//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using Microsoft.AspNetCore.Mvc;
using SmartManager.Models.GroupsStatistics;
using SmartManager.Models.Statistics;
using SmartManager.Services.Processings.GroupsStatistics;
using SmartManager.Services.Processings.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartManager.Controllers
{
    public class StatisticController : Controller
    {
        private readonly IStatisticProcessingService statisticProcessingService;
        private readonly IGroupsStatisticProccessingService groupsStatisticProccessingService;

        public StatisticController(IStatisticProcessingService statisticProcessingService, IGroupsStatisticProccessingService groupsStatisticProccessingService)
        {
            this.statisticProcessingService = statisticProcessingService;
            this.groupsStatisticProccessingService = groupsStatisticProccessingService;
        }

        public async ValueTask<IActionResult> GetStatistic()
        {
            await this.statisticProcessingService.AddOrUpdateStatisticAsync();

            IQueryable<Statistic> statistics = this.statisticProcessingService.RetrieveAllStatistics();

            return View(statistics);
        }

        public IActionResult Pie()
        {
            IQueryable<GroupsStatistic> statistics = this.groupsStatisticProccessingService.RetrieveAllGroupsStatistics();

            return View(statistics);
        }
    }
}
