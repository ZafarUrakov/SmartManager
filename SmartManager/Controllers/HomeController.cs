using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartManager.Models;
using SmartManager.Models.Statistics;
using SmartManager.Models.Students;
using SmartManager.Services.Processings.Spreadsheets;
using SmartManager.Services.Processings.Statistics;
using SmartManager.Services.Processings.StudentsStatistics;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SmartManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISpreadsheetsProcessingService spreadsheetProcessingService;
        private readonly IStudentsStatisticProccessingService groupStatisticProccessingService;

        public HomeController(
            ISpreadsheetsProcessingService spreadsheetProcessingService,
            IStudentsStatisticProccessingService groupStatisticProccessingService)
        {
            this.spreadsheetProcessingService = spreadsheetProcessingService;
            this.groupStatisticProccessingService = groupStatisticProccessingService;
        }

        [HttpPost]
        public async Task<IActionResult> ImportFile(IFormFile formFile)
        {
            IFormFile importFile = Request.Form.Files[0];
            List<Student> students = new List<Student>();

            using (MemoryStream stream = new MemoryStream())
            {
                importFile.CopyTo(stream);
                stream.Position = 0;
                students = await this.spreadsheetProcessingService
                    .ProcessImportRequest(stream);
            }

            await this.groupStatisticProccessingService
                .CheckStatisticOfList(students);

            return RedirectToAction("GetStudents", "Student");
        }

        private readonly ILogger<HomeController> _logger;

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
