//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using Microsoft.AspNetCore.Mvc;
using SmartManager.Services.Processings.PaymentStatistics;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartManager.Controllers
{
    public class PaymentStatisticController : Controller
    {
        private readonly IPaymentStatisticsProccessingService paymentStatisticsProccessingService;

        public PaymentStatisticController(IPaymentStatisticsProccessingService paymentStatisticsProccessingService)
        {
            this.paymentStatisticsProccessingService = paymentStatisticsProccessingService;
        }

        public IActionResult PaymentStatistic(Guid groupId)
        {
            var paymentStatistics = this.paymentStatisticsProccessingService
                .RetrieveAllPaymentStatistics();

            return View(paymentStatistics);
        }
    }
}
