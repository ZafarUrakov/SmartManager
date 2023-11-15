//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using SmartManager.Models.Payments;
using SmartManager.Models.Students;
using SmartManager.Services.Foundations.Payments;
using System;
using System.Collections.Generic;
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

        public async Task UpdatePaymentStatusForOverduePaymentsAsync()
        {
            await Task.Run(() =>
            {
                var payments = this.paymentService.RetrieveAllPayments();

                var currentDate = DateTime.Now;

                foreach (var payment in payments)
                {

                    if (payment != null && (currentDate - payment.Date).Days >= 30)
                    {
                        payment.IsPaid = false;
                    }

                    this.paymentService.ModifyPaymentAsync(payment);
                }
            });
        }

        public async Task CheckPaymentStatisticOfList(List<Student> students)
        {
            foreach (var item in students)
            {
                await UpdatePaymentAsync(item);
            }
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
