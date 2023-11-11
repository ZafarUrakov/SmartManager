//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using SmartManager.Models.Payments;
using SmartManager.Models.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartManager.Services.Processings.Payments
{
    public interface IPaymentProcessingService
    {
        ValueTask<Payment> UpdatePaymentAsync(Student student);
        Task UpdatePaymentStatusForOverduePaymentsAsync();
        ValueTask<Payment> AddPaymentAsync(Payment payment);
        ValueTask<Payment> RetrievePaymentByIdAsync(Guid paymentid);
        IQueryable<Payment> RetrieveAllPayments();
        ValueTask<Payment> ModifyPaymentAsync(Payment payment);
        ValueTask<Payment> RemovePaymentAsync(Guid paymentid);
        Task CheckPaymentStatisticOfList(List<Student> students);
    }
}
