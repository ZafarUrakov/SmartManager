//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using Microsoft.EntityFrameworkCore;
using SmartManager.Models.PaymentStatistics;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartManager.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<PaymentStatistic> PaymentStatistics { get; set; }

        public async ValueTask<PaymentStatistic> InsertPaymentStatisticAsync(PaymentStatistic paymentStatistic) =>
            await InsertAsync(paymentStatistic);

        public IQueryable<PaymentStatistic> SelectAllPaymentStatistics() =>
            SelectAll<PaymentStatistic>().AsQueryable();

        public async ValueTask<PaymentStatistic> SelectPaymentStatisticByIdAsync(Guid paymentStatisticId) =>
            await SelectAsync<PaymentStatistic>(paymentStatisticId);

        public async ValueTask<PaymentStatistic> UpdatePaymentStatisticAsync(PaymentStatistic paymentStatistic) =>
            await UpdateAsync(paymentStatistic);

        public async ValueTask<PaymentStatistic> DeletePaymentStatisticAsync(PaymentStatistic paymentStatistic) =>
            await DeleteAsync(paymentStatistic);
    }
}
