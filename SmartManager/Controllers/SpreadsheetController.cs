using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartManager.Models.Students;
using SmartManager.Services.Processings.Spreadsheets;
using SmartManager.Services.Processings.StudentsStatistics;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SmartManager.Controllers
{
    public class SpreadsheetController : Controller
    {
        private readonly ISpreadsheetsProcessingService spreadsheetProcessingService;
        private readonly IStudentsStatisticProccessingService groupStatisticProccessingService;

        public SpreadsheetController(
            ISpreadsheetsProcessingService spreadsheetProcessingService,
            IStudentsStatisticProccessingService groupStatisticProccessingService)
        {
            this.spreadsheetProcessingService = spreadsheetProcessingService;
            this.groupStatisticProccessingService = groupStatisticProccessingService;
        }

        public IActionResult Import()
        {
            return View();
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
    }
}