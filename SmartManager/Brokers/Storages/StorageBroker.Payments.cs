//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using Microsoft.EntityFrameworkCore;
using SmartManager.Models.Payments;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartManager.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Payment> Payments { get; set; }

        public async ValueTask<Payment> InsertPaymentAsync(Payment payment) =>
            await InsertAsync(payment);

        public IQueryable<Payment> SelectAllPayments() =>
            SelectAll<Payment>().AsQueryable();

        public async ValueTask<Payment> SelectPaymentByIdAsync(Guid paymentId) =>
            await SelectAsync<Payment>(paymentId);

        public async ValueTask<Payment> UpdatePaymentAsync(Payment payment) =>
            await UpdateAsync(payment);

        public async ValueTask<Payment> DeletePaymentAsync(Payment payment) =>
            await DeleteAsync(payment);
    }
}
