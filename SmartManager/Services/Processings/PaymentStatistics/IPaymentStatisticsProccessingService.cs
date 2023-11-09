//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using SmartManager.Models.PaymentStatistics;
using SmartManager.Models.Students;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartManager.Services.Processings.PaymentStatistics
{
    public interface IPaymentStatisticsProccessingService
    {
        ValueTask<PaymentStatistic> AddPaymentStatisticAsync(Student student);
        ValueTask<PaymentStatistic> RetrievePaymentStatisticByIdAsync(Guid paymentStatisticId);
        IQueryable<PaymentStatistic> RetrieveAllPaymentStatistics();
        ValueTask<PaymentStatistic> ModifyPaymentStatisticAsync(Student student);
        ValueTask<PaymentStatistic> RemovePaymentStatisticAsync(Guid paymentStatisticId);
    }
}
