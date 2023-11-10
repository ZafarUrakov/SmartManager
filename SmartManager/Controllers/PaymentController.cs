//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using Microsoft.AspNetCore.Mvc;
using SmartManager.Models.Payments;
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

        public PaymentController(
            IPaymentProcessingService paymentProcessingService,
            IStudentProcessingService studentProcessingService,
            IPaymentStatisticsProccessingService paymentStatisticsProccessingService)
        {
            this.paymentProcessingService = paymentProcessingService;
            this.studentProcessingService = studentProcessingService;
            this.paymentStatisticsProccessingService = paymentStatisticsProccessingService;
        }
        [HttpPost]
        public async ValueTask<ActionResult> UpdatePaymentAsync(Guid studentId, bool isPayed)
        {
            var payment = new Payment
            {
                Id = Guid.NewGuid(),
                Amount = 900000,
                Date = DateTime.Now,
                IsPaid = isPayed,
                StudentId = studentId
            };

            var persistedPayment =
                this.paymentProcessingService.RetrieveAllPayments()
                    .FirstOrDefault(s => s.StudentId == studentId);
            persistedPayment.IsPaid = isPayed;
            persistedPayment.StudentId = studentId;
            persistedPayment.Date = DateTime.Now;
            persistedPayment.Amount = 900000;

            var student = await this.studentProcessingService.RetrieveStudentByIdAsync(studentId);

            await this.paymentProcessingService.ModifyPaymentAsync(persistedPayment);

            await this.paymentStatisticsProccessingService.AddPaymentStatisticAsync(student);

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
