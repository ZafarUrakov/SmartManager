//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using SmartManager.Brokers.Storages;
using SmartManager.Models.PaymentStatistics;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartManager.Services.Foundations.PaymentStatistics
{
    public class PaymentStatisticService : IPaymentStatisticService
    {
        private readonly IStorageBroker storageBroker;

        public PaymentStatisticService(IStorageBroker storageBroker)
        {
            this.storageBroker = storageBroker;
        }

        public async ValueTask<PaymentStatistic> AddPaymentStatisticAsync(PaymentStatistic paymentStatistic) =>
            await this.storageBroker.InsertPaymentStatisticAsync(paymentStatistic);

        public async ValueTask<PaymentStatistic> RetrievePaymentStatisticByIdAsync(Guid paymentStatisticId) =>
            await this.storageBroker.SelectPaymentStatisticByIdAsync(paymentStatisticId);

        public IQueryable<PaymentStatistic> RetrieveAllPaymentStatistics() =>
            this.storageBroker.SelectAllPaymentStatistics();

        public async ValueTask<PaymentStatistic> ModifyPaymentStatisticAsync(PaymentStatistic paymentStatistic) =>
            await this.storageBroker.UpdatePaymentStatisticAsync(paymentStatistic);

        public async ValueTask<PaymentStatistic> RemovePaymentStatisticAsync(Guid paymentStatisticId)
        {
            PaymentStatistic paymentStatistic = await this.storageBroker.SelectPaymentStatisticByIdAsync(paymentStatisticId);

            return await this.storageBroker.DeletePaymentStatisticAsync(paymentStatistic);
        }
    }
}
