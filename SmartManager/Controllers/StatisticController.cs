//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using Microsoft.AspNetCore.Mvc;
using SmartManager.Models.Statistics;
using SmartManager.Services.Processings.Statistics;
using System.Linq;
using System.Threading.Tasks;

namespace SmartManager.Controllers
{
    public class StatisticController : Controller
    {
        private readonly IStatisticProcessingService statisticProcessingService;

        public StatisticController(IStatisticProcessingService statisticProcessingService)
        {
            this.statisticProcessingService = statisticProcessingService;
        }

        public async ValueTask<IActionResult> GetStatistic()
        {
            await this.statisticProcessingService.AddOrUpdateStatisticAsync();

            IQueryable<Statistic> statistics = this.statisticProcessingService.RetrieveAllStatistics();

            return View(statistics);
        }
    }
}
