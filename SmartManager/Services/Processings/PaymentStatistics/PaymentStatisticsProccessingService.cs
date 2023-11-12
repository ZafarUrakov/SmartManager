//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using SmartManager.Models.Groups;
using SmartManager.Models.PaymentStatistics;
using SmartManager.Models.Students;
using SmartManager.Services.Foundations.PaymentStatistics;
using SmartManager.Services.Processings.Groups;
using SmartManager.Services.Processings.Payments;
using SmartManager.Services.Processings.Students;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartManager.Services.Processings.PaymentStatistics
{
    public class PaymentStatisticsProccessingService : IPaymentStatisticsProccessingService
    {
        private readonly IPaymentStatisticService paymentStatisticService;
        private readonly IGroupProcessingService groupProcessingService;
        private readonly IPaymentProcessingService paymentProcessingService;
        private readonly IStudentProcessingService studentProcessingService;
        public PaymentStatisticsProccessingService(
            IPaymentStatisticService PaymentStatisticService,
            IGroupProcessingService groupProcessingService,
            IPaymentProcessingService paymentProcessingService,
            IStudentProcessingService studentProcessingService)
        {
            this.paymentStatisticService = PaymentStatisticService;
            this.groupProcessingService = groupProcessingService;
            this.paymentProcessingService = paymentProcessingService;
            this.studentProcessingService = studentProcessingService;
        }
        public async ValueTask<PaymentStatistic> AddPaymentStatisticAsync(Student student)
        {
            var students = this.studentProcessingService.RetrieveAllStudents().Where(s => s.GroupId == student.GroupId);
            var group = await this.groupProcessingService.RetrieveGroupByIdAsync(student.GroupId);

            decimal totalStudents = 0;
            decimal paids = 0;

            TotalCountStudentsAndPayments(student, students, ref totalStudents, ref paids);

            var paymentStatistic = this.paymentStatisticService
                .RetrieveAllPaymentStatistics().FirstOrDefault(p => p.GroupId == group.Id);

            if (paymentStatistic is null)
            {
                PaymentStatistic newPaymentStatistic = AddPaymentStatisticIfNotFound(group);
                if (totalStudents != 0)
                {
                    newPaymentStatistic.PaidPercentage = (paids / totalStudents) * 100;
                    newPaymentStatistic.NotPaidPercentage = 100 - newPaymentStatistic.PaidPercentage;
                }
                else
                {
                    newPaymentStatistic.PaidPercentage = 0;

                    newPaymentStatistic.NotPaidPercentage = 100 - newPaymentStatistic.PaidPercentage;
                }

                return await this.paymentStatisticService.AddPaymentStatisticAsync(newPaymentStatistic);
            }
            else
            {
                if (totalStudents != 0)
                {
                    paymentStatistic.PaidPercentage = (paids / totalStudents) * 100;
                    paymentStatistic.NotPaidPercentage = 100 - paymentStatistic.PaidPercentage;
                }
                else
                {
                    paymentStatistic.PaidPercentage = 0;
                    paymentStatistic.NotPaidPercentage = 100 - paymentStatistic.PaidPercentage;
                }
                return await this.paymentStatisticService.ModifyPaymentStatisticAsync(paymentStatistic);
            }
        }

        public async ValueTask<PaymentStatistic> ModifyPaymentStatisticAsync(Student student)
        {
            var students = this.studentProcessingService.RetrieveAllStudents().Where(s => s.GroupId == student.GroupId);
            var group = await this.groupProcessingService.RetrieveGroupByIdAsync(student.GroupId);

            var paymentStatistic = this.paymentStatisticService.RetrieveAllPaymentStatistics()
                    .FirstOrDefault(p => p.GroupId == student.GroupId);

            decimal totalStudents = 0;
            decimal paids = 0;

            TotalCountStudentsAndPayments(student, students, ref totalStudents, ref paids);

            if(totalStudents != 0)
            {
            paymentStatistic.PaidPercentage = (paids / totalStudents) * 100;
            paymentStatistic.NotPaidPercentage = 100 - paymentStatistic.PaidPercentage;
            }
            else
            {
                paymentStatistic.PaidPercentage = 0;
            }

            return await this.paymentStatisticService.ModifyPaymentStatisticAsync(paymentStatistic);
        }

        private void TotalCountStudentsAndPayments(
            Student student, IQueryable<Student> students, ref decimal totalStudents, ref decimal paids)
        {
            var payments = this.paymentProcessingService.RetrieveAllPayments()
                .Where(p => p.Student.GroupId == student.GroupId);

            foreach (var item in students)
            {
                totalStudents++;
            }

            foreach (var payment in payments)
            {
                if (payment.IsPaid)
                    paids++;
            }
        }


        private static PaymentStatistic AddPaymentStatisticIfNotFound(Group group)
        {
            return new PaymentStatistic
            {
                Id = Guid.NewGuid(),
                Group = group,
                GroupId = group.Id
            };
        }

        public async ValueTask<PaymentStatistic> RetrievePaymentStatisticByIdAsync(Guid paymentStatisticid) =>
            await this.paymentStatisticService.RetrievePaymentStatisticByIdAsync(paymentStatisticid);

        public IQueryable<PaymentStatistic> RetrieveAllPaymentStatistics() =>
            this.paymentStatisticService.RetrieveAllPaymentStatistics();

        public async ValueTask<PaymentStatistic> RemovePaymentStatisticAsync(Guid paymentStatisticid) =>
            await this.paymentStatisticService.RemovePaymentStatisticAsync(paymentStatisticid);
    }
}
