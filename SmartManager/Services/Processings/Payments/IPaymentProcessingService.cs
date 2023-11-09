//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using SmartManager.Models.Payments;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartManager.Services.Processings.Payments
{
    public interface IPaymentProcessingService
    {
        ValueTask<Payment> AddPaymentAsync(Payment payment);
        ValueTask<Payment> RetrievePaymentByIdAsync(Guid paymentid);
        IQueryable<Payment> RetrieveAllPayments();
        ValueTask<Payment> ModifyPaymentAsync(Payment payment);
        ValueTask<Payment> RemovePaymentAsync(Guid paymentid);
    }
}
