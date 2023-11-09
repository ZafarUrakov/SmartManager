//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using SmartManager.Models.Statistics;
using SmartManager.Models.Students;
using SmartManager.Services.Foundations.Statistics;
using SmartManager.Services.Processings.Payments;
using SmartManager.Services.Processings.Students;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartManager.Services.Processings.Statistics
{
    public class StatisticProcessingService : IStatisticProcessingService
    {
        private readonly IStatisticService statisticService;
        private readonly IStudentProcessingService studentProcessingService;
        private readonly IPaymentProcessingService paymentProcessingService;

        public StatisticProcessingService(
            IStatisticService statisticService,
            IStudentProcessingService studentProcessingService,
            IPaymentProcessingService paymentProcessingService)
        {
            this.statisticService = statisticService;
            this.studentProcessingService = studentProcessingService;
            this.paymentProcessingService = paymentProcessingService;
        }
        public async ValueTask<Statistic> AddOrUpdateStatisticAsync() =>
            await ProcessAndAddStudentsStatistic();

        public async ValueTask<Statistic> RetrieveStatisticByIdAsync(Guid statisticid) =>
            await this.statisticService.RetrieveStatisticByIdAsync(statisticid);

        public IQueryable<Statistic> RetrieveAllStatistics() =>
            this.statisticService.RetrieveAllStatistics();

        public async ValueTask<Statistic> ModifyStatisticAsync(Statistic statistic) =>
            await this.statisticService.ModifyStatisticAsync(statistic);

        public async ValueTask<Statistic> RemoveStatisticAsync(Guid statisticid) =>
            await this.statisticService.RemoveStatisticAsync(statisticid);

        private async ValueTask<Statistic> ProcessAndAddStudentsStatistic()
        {
            var maybeStatistic = this.statisticService
                .RetrieveAllStatistics().FirstOrDefault();

            if (maybeStatistic is null)
            {
                Statistic statistic = new Statistic
                {
                    Id = Guid.NewGuid()
                };

                UpdateStatistic(statistic);

                return await this.statisticService.AddStatisticAsync(statistic);
            }
            else
            {
                UpdateStatistic(maybeStatistic);

                return await this.statisticService.ModifyStatisticAsync(maybeStatistic);
            }
        }

        private void UpdateStatistic(Statistic statistic)
        {
            var students = this.studentProcessingService.RetrieveAllStudents();
            var payments = this.paymentProcessingService.RetrieveAllPayments();

            var maleStudents = this.studentProcessingService
                .RetrieveAllStudents().Where(s => s.Gender == Gender.Male).ToList();
            var femaleStudents = this.studentProcessingService
                .RetrieveAllStudents().Where(s => s.Gender == Gender.Female).ToList();
            var ontherStudents = this.studentProcessingService
                .RetrieveAllStudents().Where(s => s.Gender == Gender.Other).ToList();

            int studentsCount = students.Count();
            int maleStudentsCount = maleStudents.Count();
            int femaleStudentsCount = femaleStudents.Count();
            int otherStudentsCount = ontherStudents.Count();
            int unknownStudentsCount = studentsCount - (maleStudentsCount + femaleStudentsCount + otherStudentsCount);

            statistic.TotalStudentsCount = studentsCount;
            statistic.MaleStudentsCount = maleStudentsCount;
            statistic.FemaleStudentsCount = femaleStudentsCount;
            statistic.OtherStudentsCount = otherStudentsCount;
            statistic.UnknownStudentsCount = unknownStudentsCount;

            var paidStudents = this.paymentProcessingService.RetrieveAllPayments().Where(p => p.IsPaid == true);
            int paidStudetnsCount = paidStudents.Count();

            decimal paidStudentsPercentage;
            if (studentsCount != 0)
                paidStudentsPercentage = (paidStudetnsCount / studentsCount) * 100;
            else
                paidStudentsPercentage = 0;

            statistic.PaidStudentsCount = paidStudetnsCount;
            statistic.PaidStudentsPercentage = paidStudentsPercentage;

            decimal totalPayment = 0;

            foreach (var payment in payments)
            {
                totalPayment += payment.Amount;
            }

            statistic.TotalPayment = totalPayment;
        }
    }
}
