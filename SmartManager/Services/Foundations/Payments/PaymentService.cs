//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using SmartManager.Brokers.Storages;
using SmartManager.Models.Payments;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartManager.Services.Foundations.Payments
{
    public class PaymentService : IPaymentService
    {
        private readonly IStorageBroker storageBroker;

        public PaymentService(IStorageBroker storageBroker)
        {
            this.storageBroker = storageBroker;
        }

        public async ValueTask<Payment> AddPaymentAsync(Payment payment) =>
            await this.storageBroker.InsertPaymentAsync(payment);

        public async ValueTask<Payment> RetrievePaymentByIdAsync(Guid paymentid) =>
            await this.storageBroker.SelectPaymentByIdAsync(paymentid);

        public IQueryable<Payment> RetrieveAllPayments() =>
            this.storageBroker.SelectAllPayments();

        public async ValueTask<Payment> ModifyPaymentAsync(Payment payment) =>
            await this.storageBroker.UpdatePaymentAsync(payment);

        public async ValueTask<Payment> RemovePaymentAsync(Guid paymentid)
        {
            Payment payment = await this.storageBroker.SelectPaymentByIdAsync(paymentid);

            return await this.storageBroker.DeletePaymentAsync(payment);
        }
    }
}
