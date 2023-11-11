using Microsoft.AspNetCore.Mvc;
using SmartManager.Models;
using SmartManager.Models.Statistics;
using SmartManager.Services.Processings.Statistics;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SmartManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStatisticProcessingService statisticProcessingService;
        public HomeController(IStatisticProcessingService statisticProcessingService)
        {
            this.statisticProcessingService = statisticProcessingService;
        }

        public async ValueTask<IActionResult> Index()
        {
            await this.statisticProcessingService.AddOrUpdateStatisticAsync();

            IQueryable<Statistic> statistics = this.statisticProcessingService.RetrieveAllStatistics();

            return View(statistics);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
