//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using Microsoft.AspNetCore.Mvc;
using SmartManager.Models.Payments;
using SmartManager.Services.Foundations.TelegramBots;
using SmartManager.Services.Processings.Payments;
using SmartManager.Services.Processings.PaymentStatistics;
using SmartManager.Services.Processings.Students;
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
        private readonly ITelegramBotService telegramBotService;


        public PaymentController(
            IPaymentProcessingService paymentProcessingService,
            IStudentProcessingService studentProcessingService,
            IPaymentStatisticsProccessingService paymentStatisticsProccessingService,
            ITelegramBotService telegramBotService)
        {
            this.paymentProcessingService = paymentProcessingService;
            this.studentProcessingService = studentProcessingService;
            this.paymentStatisticsProccessingService = paymentStatisticsProccessingService;
            this.telegramBotService = telegramBotService;
        }
        [HttpPost]
        public async ValueTask<ActionResult> UpdatePaymentAsync(Guid studentId, bool isPayed = true)
        {
            var persistedPayment =
                this.paymentProcessingService.RetrieveAllPayments()
                    .FirstOrDefault(s => s.StudentId == studentId);
            persistedPayment.IsPaid = isPayed;
            persistedPayment.StudentId = studentId;
            persistedPayment.Date = DateTime.Now;
            persistedPayment.Amount = 900000;
            //hi

            var student = await this.studentProcessingService.RetrieveStudentByIdAsync(studentId);

            await this.paymentProcessingService.ModifyPaymentAsync(persistedPayment);

            await this.paymentStatisticsProccessingService.AddPaymentStatisticAsync(student);

            await this.telegramBotService.SendPaymentMessageToStudents(student, isPayed);

            return RedirectToAction("GetPayment", "Student");
        }

        [HttpPost]
        public async ValueTask<ActionResult> PostPayment([FromForm] Payment payment)
        {
            await this.paymentProcessingService.AddPaymentAsync(payment);

            return RedirectToAction("GetPayment", "Student");
        }
    }
}
