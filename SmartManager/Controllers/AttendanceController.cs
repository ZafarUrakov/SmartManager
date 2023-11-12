//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using Microsoft.AspNetCore.Mvc;
using SmartManager.Models.Attendances;
using SmartManager.Services.Processings.Attendances;
using SmartManager.Services.Processings.Students;
using SmartManager.Services.Processings.TelegramBots;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartManager.Controllers
{
    public class AttendanceController : Controller
    {
        private readonly IAttendanceProcessingService attendanceProcessingService;
        private readonly IStudentProcessingService studentProcessingService;
        private readonly ITelegramBotProcessingService telegramBotProcessingService;

        public AttendanceController(
            IAttendanceProcessingService attendanceProcessingService,
            IStudentProcessingService studentProcessingService,
            ITelegramBotProcessingService telegramBotProcessingService)
        {
            this.attendanceProcessingService = attendanceProcessingService;
            this.studentProcessingService = studentProcessingService;
            this.telegramBotProcessingService = telegramBotProcessingService;
        }

        [HttpPost]
        public async ValueTask<ActionResult> UpdateAttendance(Guid studentId, bool isPresent)
        {
            var student =
                await this.studentProcessingService.RetrieveStudentByIdAsync(studentId);

            await this.attendanceProcessingService.AddAttendanceAsync(student, isPresent);

            await this.telegramBotProcessingService.SendAttendanceMassageToStudents(student, isPresent);

            return RedirectToAction("GetStudentsWithGroup", "Student");
        }

        [HttpGet] 
        public ActionResult ReviewAttendance(Guid studentId)
        {
            IQueryable<Attendance> attendances =
                this.attendanceProcessingService.RetrieveAllAttendances()
                    .Where(a => a.StudentId == studentId);

            return View(attendances);
        }
    }
}
