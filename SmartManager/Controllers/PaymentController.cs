//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using Microsoft.AspNetCore.Mvc;
using SmartManager.Services.Processings.Payments;
using SmartManager.Services.Processings.PaymentStatistics;
using SmartManager.Services.Processings.Students;
using SmartManager.Services.Processings.TelegramBots;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartManager.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPaymentProcessingService paymentProcessingService;
        private readonly IStudentProcessingService studentProcessingService;
        private readonly IPaymentStatisticsProccessingService paymentStatisticsProccessingService;
        private readonly ITelegramBotProcessingService telegramBotProcessingService;


        public PaymentController(
            IPaymentProcessingService paymentProcessingService,
            IStudentProcessingService studentProcessingService,
            IPaymentStatisticsProccessingService paymentStatisticsProccessingService,
            ITelegramBotProcessingService telegramBotProcessingService)
        {
            this.paymentProcessingService = paymentProcessingService;
            this.studentProcessingService = studentProcessingService;
            this.paymentStatisticsProccessingService = paymentStatisticsProccessingService;
            this.telegramBotProcessingService = telegramBotProcessingService;
        }
        [HttpPost]
        public async ValueTask<ActionResult> UpdatePaymentAsync(Guid studentId, bool isPaid = true, decimal amount = 0)
        {
            var student = await this.studentProcessingService.RetrieveStudentByIdAsync(studentId);

            var persistedPayment =
                this.paymentProcessingService.RetrieveAllPayments()
                    .FirstOrDefault(s => s.StudentId == studentId);

            persistedPayment.IsPaid = isPaid;
            persistedPayment.StudentId = studentId;
            persistedPayment.Student = student;
            persistedPayment.Date = DateTime.Now;

            persistedPayment.Amount = amount > 0 ? amount : 900000;


            await this.paymentProcessingService.ModifyPaymentAsync(persistedPayment);

            await this.paymentStatisticsProccessingService.AddPaymentStatisticAsync(student);

            await this.telegramBotProcessingService.SendPaymentMessageToStudents(student, isPaid);

            return RedirectToAction("GetPayment", "Student");
        }



        [HttpPost]
        public async ValueTask<ActionResult> RemainderToStudents(Guid studentId, bool remainder = true)
        {
            await this.telegramBotProcessingService.SendReminderMessageToStudents(studentId, remainder);

            return RedirectToAction("GetPayment", "Student");
        }
    }
}
