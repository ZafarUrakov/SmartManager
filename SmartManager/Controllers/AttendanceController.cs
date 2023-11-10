//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using Microsoft.AspNetCore.Mvc;
using SmartManager.Models.Attendances;
using SmartManager.Services.Foundations.TelegramBots;
using SmartManager.Services.Processings.Attendances;
using SmartManager.Services.Processings.Students;
using System;
using System.Threading.Tasks;

namespace SmartManager.Controllers
{
    public class AttendanceController : Controller
    {
        private readonly IAttendanceProcessingService attendanceProcessingService;
        private readonly IStudentProcessingService studentProcessingService;
        private readonly ITelegramBotService telegramBotService;

        public AttendanceController(
            IAttendanceProcessingService attendanceProcessingService,
            IStudentProcessingService studentProcessingService,
            ITelegramBotService telegramBotService)
        {
            this.attendanceProcessingService = attendanceProcessingService;
            this.studentProcessingService = studentProcessingService;
            this.telegramBotService = telegramBotService;
        }

        [HttpPost]
        public async ValueTask<ActionResult> UpdateAttendance(Guid studentId, bool isPresent)
        {
            var student =
                await this.studentProcessingService.RetrieveStudentByIdAsync(studentId);

            var attendance = new Attendance
            {
                Id = Guid.NewGuid(),
                Student = student,
                Date = DateTimeOffset.Now,
                IsPresent = isPresent
            };

            await this.attendanceProcessingService.AddAttendanceAsync(attendance);

            await this.telegramBotService.SendAttendanceMassageToStudents(student, isPresent);

            return RedirectToAction("GetStudentsWithGroup", "Student");
        }

        [HttpPost]
        public async ValueTask<ActionResult> PostAttendance([FromForm] Attendance attendance)
        {
            await this.attendanceProcessingService.AddAttendanceAsync(attendance);

            return RedirectToAction("GetStudents", "Student");
        }
    }
}
