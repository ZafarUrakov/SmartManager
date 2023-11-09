//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using SmartManager.Models.Payments;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartManager.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Payment> InsertPaymentAsync(Payment payment);
        IQueryable<Payment> SelectAllPayments();
        ValueTask<Payment> SelectPaymentByIdAsync(Guid paymentId);
        ValueTask<Payment> UpdatePaymentAsync(Payment payment);
        ValueTask<Payment> DeletePaymentAsync(Payment payment);
    }
}
