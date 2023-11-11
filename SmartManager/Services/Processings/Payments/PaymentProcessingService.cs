//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using SmartManager.Models.Payments;
using SmartManager.Models.Students;
using SmartManager.Services.Foundations.Payments;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartManager.Services.Processings.Payments
{
    public class PaymentProcessingService : IPaymentProcessingService
    {
        private readonly IPaymentService paymentService;

        public PaymentProcessingService(IPaymentService paymentService)
        {
            this.paymentService = paymentService;
        }

        public async ValueTask<Payment> UpdatePaymentAsync(Student student)
        {
            Payment payment = new Payment();
            payment.StudentId = student.Id;
            payment.Date = DateTimeOffset.UtcNow;
            payment.Amount = 0;
            payment.IsPaid = false;

            return await AddPaymentAsync(payment);
        }

        public async Task UpdatePaymentStatusForOverduePaymentsAsync(IQueryable<Student> students)
        {
            await Task.Run(() =>
            {
                var payments = this.paymentService.RetrieveAllPayments();

                var currentDate = DateTime.Now;

                foreach (var student in students)
                {
                    var lastPayment = payments
                        .Where(p => p.StudentId == student.Id)
                        .OrderByDescending(p => p.Date)
                        .FirstOrDefault();

                    if (lastPayment != null && (currentDate - lastPayment.Date).Days > 30)
                    {
                        lastPayment.IsPaid = false;
                    }
                }
            });
        }


        public async ValueTask<Payment> AddPaymentAsync(Payment Payment) =>
           await this.paymentService.AddPaymentAsync(Payment);

        public async ValueTask<Payment> RetrievePaymentByIdAsync(Guid Paymentid) =>
            await this.paymentService.RetrievePaymentByIdAsync(Paymentid);

        public IQueryable<Payment> RetrieveAllPayments() =>
            this.paymentService.RetrieveAllPayments();

        public async ValueTask<Payment> ModifyPaymentAsync(Payment Payment) =>
            await this.paymentService.ModifyPaymentAsync(Payment);

        public async ValueTask<Payment> RemovePaymentAsync(Guid Paymentid) =>
            await this.paymentService.RemovePaymentAsync(Paymentid);
    }
}
