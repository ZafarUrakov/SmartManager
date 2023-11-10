using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartManager.Models.Payments;
using SmartManager.Models.Students;
using SmartManager.Services.Processings.GroupsStatistics;
using SmartManager.Services.Processings.Payments;
using SmartManager.Services.Processings.PaymentStatistics;
using SmartManager.Services.Processings.Spreadsheets;
using SmartManager.Services.Processings.Statistics;
using SmartManager.Services.Processings.Students;
using SmartManager.Services.Processings.StudentsStatistics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SmartManager.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentProcessingService studentProcessingService;
        private readonly IPaymentStatisticsProccessingService paymentStatisticsProccessingService;
        private readonly IStudentsStatisticProccessingService groupStatisticProccessingService;
        private readonly IGroupsStatisticProccessingService groupsStatisticProccessingService;
        private readonly IStatisticProcessingService statisticProcessingService;
        private readonly ISpreadsheetsProcessingService spreadsheetsProcessingService;
        private readonly IPaymentProcessingService paymentProcessingService;

        public StudentController(
            IStudentProcessingService studentProcessingService,
            IPaymentStatisticsProccessingService paymentStatisticsProccessingService,
            IStudentsStatisticProccessingService groupStatisticProccessingService,
            IGroupsStatisticProccessingService groupsStatisticProccessingService,
            IStatisticProcessingService statisticProcessingService,
            ISpreadsheetsProcessingService spreadsheetsProcessingService,
            IPaymentProcessingService paymentProcessingService)
        {
            this.studentProcessingService = studentProcessingService;
            this.paymentStatisticsProccessingService = paymentStatisticsProccessingService;
            this.groupStatisticProccessingService = groupStatisticProccessingService;
            this.groupsStatisticProccessingService = groupsStatisticProccessingService;
            this.statisticProcessingService = statisticProcessingService;
            this.spreadsheetsProcessingService = spreadsheetsProcessingService;
            this.paymentProcessingService = paymentProcessingService;
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
                students = await this.spreadsheetsProcessingService
                    .ProcessImportRequest(stream);
            }

            await this.groupStatisticProccessingService
                .CheckStatisticOfList(students);

            return RedirectToAction(nameof(GetStudents));
        }

        public IActionResult PostStudent()
        {
            return View();
        }

        [HttpPost]
        public async ValueTask<IActionResult> PostStudent(Student student)
        {
            var newStudent = await this.studentProcessingService.AddStudentAsync(student);

            await this.groupStatisticProccessingService
                 .UpdateStatisticsByStudentAsync(student);

            await this.paymentStatisticsProccessingService.AddPaymentStatisticAsync(student);

            await this.groupsStatisticProccessingService.AddGroupsStatisticsWithStudentsAsync(student);

            await this.statisticProcessingService.AddOrUpdateStatisticAsync();

            this.groupsStatisticProccessingService.ModifyGroupsStatisticAsync(newStudent);
            await this.paymentProcessingService.UpdatePaymentAsync(student);

            return RedirectToAction("GetStudents");
        }

        public IActionResult GetStudents()
        {
            IQueryable<Student> students = this.studentProcessingService.RetrieveAllStudents();

            return View(students);
        }

        public IActionResult GetStudentsWithGroup(Guid groupId)
        {
            IQueryable<Student> students =
                this.studentProcessingService.RetrieveAllStudents().Where(a => a.GroupId == groupId);

            return View(students);
        }

        public IActionResult GetStudentsWithAttendances(Guid groupId)
        {
            IQueryable<Student> students =
                this.studentProcessingService.RetrieveAllStudents()
                    .Where(student => student.GroupId == groupId);

            return View(students);
        }

        public IActionResult GetStudentsWithPayments(Guid groupId)
        {
            IQueryable<Student> students =
                this.studentProcessingService.RetrieveAllStudents().Where(s => s.GroupId == groupId);

            return View(students);
        }

        public IActionResult GetNotPaidStudents(Guid groupId, bool isPaid)
        {
            IQueryable<Student> students =
                this.studentProcessingService.RetrieveAllStudents().Where(s => s.GroupId == groupId);
            List<Payment> payments = new List<Payment>();
            List<Payment> notPaidStudents = new List<Payment>();


            foreach (var item in students)
            {
                var payment = this.paymentProcessingService.RetrieveAllPayments().FirstOrDefault(p => p.StudentId == item.Id);
                payments.Add((Payment)payment);
            }

            foreach (var item in payments)
            {
                if(!item.IsPaid)
                {
                    notPaidStudents.Add(item);
                }
            }
            return View(students);
        }

        public async ValueTask<ActionResult> GetStudentAsync(Guid Id)
        {
            var student =
                await this.studentProcessingService.RetrieveStudentByIdAsync(Id);

            return Ok(student);
        }


        [HttpGet]
        public IActionResult PutStudent(Guid studentId)
        {
            IQueryable<Student> students = this.studentProcessingService.RetrieveAllStudents();

            Student student = students.FirstOrDefault(a => a.Id == studentId);

            return View(student);
        }

        [HttpPost]
        public async ValueTask<IActionResult> PutStudent(Student student)
        {
            var oldStudent = await this.studentProcessingService.RetrieveStudentByIdAsync(student.Id);

            var updatedStudent = await studentProcessingService.ModifyStudentAsync(student);

            this.groupsStatisticProccessingService.ModifyGroupsStatisticAsync(oldStudent);

            await this.paymentStatisticsProccessingService.AddPaymentStatisticAsync(updatedStudent);

            await this.statisticProcessingService.AddOrUpdateStatisticAsync();

            return RedirectToAction("GetStudents");
        }

        [HttpGet]
        public async ValueTask<IActionResult> DeleteStudent(Guid studentId)
        {
            try
            {
                var students = this.studentProcessingService.RetrieveAllStudents();

                var student = students.FirstOrDefault(a => a.Id == studentId);

                var removedStudent = await this.studentProcessingService.RemoveStudentAsync(student.Id);

                this.groupsStatisticProccessingService.ModifyGroupsStatisticAsync(removedStudent);

                await this.statisticProcessingService.AddOrUpdateStatisticAsync();

                await this.paymentStatisticsProccessingService.AddPaymentStatisticAsync(removedStudent);

                return RedirectToAction("GetStudents");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
