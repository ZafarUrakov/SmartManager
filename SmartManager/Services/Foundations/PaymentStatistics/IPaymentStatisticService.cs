//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using SmartManager.Models.PaymentStatistics;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartManager.Services.Foundations.PaymentStatistics
{
    public interface IPaymentStatisticService
    {
        ValueTask<PaymentStatistic> AddPaymentStatisticAsync(PaymentStatistic paymentStatistic);
        ValueTask<PaymentStatistic> RetrievePaymentStatisticByIdAsync(Guid paymentStatisticId);
        IQueryable<PaymentStatistic> RetrieveAllPaymentStatistics();
        ValueTask<PaymentStatistic> ModifyPaymentStatisticAsync(PaymentStatistic paymentStatistic);
        ValueTask<PaymentStatistic> RemovePaymentStatisticAsync(Guid paymentStatisticId);
    }
}
