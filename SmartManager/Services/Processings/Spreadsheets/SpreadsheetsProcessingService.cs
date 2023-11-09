//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using SmartManager.Models.ExternalStudents;
using SmartManager.Models.Groups;
using SmartManager.Models.Students;
using SmartManager.Services.Foundations.Spreadsheets;
using SmartManager.Services.Processings.Groups;
using SmartManager.Services.Processings.GroupsStatistics;
using SmartManager.Services.Processings.PaymentStatistics;
using SmartManager.Services.Processings.Students;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SmartManager.Services.Processings.Spreadsheets
{
    public class SpreadsheetsProcessingService : ISpreadsheetsProcessingService
    {
        private readonly ISpreadsheetService spreadsheetService;
        private readonly IStudentProcessingService studentProcessingService;
        private readonly IGroupProcessingService groupProcessingService;
        private readonly IGroupsStatisticProccessingService groupsStatisticProccessingService;
        private readonly IPaymentStatisticsProccessingService paymentStatisticsProccessingService;

        public SpreadsheetsProcessingService(
            IStudentProcessingService studentProcessingService,
            IGroupProcessingService groupProcessingService,
            ISpreadsheetService spreadsheetService,
            IGroupsStatisticProccessingService groupsStatisticProccessingService,
            IPaymentStatisticsProccessingService paymentStatisticsProccessingService)
        {
            this.studentProcessingService = studentProcessingService;
            this.groupProcessingService = groupProcessingService;
            this.spreadsheetService = spreadsheetService;
            this.groupsStatisticProccessingService = groupsStatisticProccessingService;
            this.paymentStatisticsProccessingService = paymentStatisticsProccessingService;
        }

        public async ValueTask<List<Student>> ProcessImportRequest(MemoryStream stream)
        {
            List<Student> mappedStudents = new List<Student>();

            List<ExternalStudent> validExternalStudents =
                this.spreadsheetService.GetExternalStudents(stream);

            foreach (var externalStudent in validExternalStudents)
            {

                Group ensureGroup =
                    await groupProcessingService
                    .EnsureGroupExistsByName(externalStudent.GroupName);


                Student student = MapToStudent(externalStudent, ensureGroup);

                mappedStudents.Add(student);

                await studentProcessingService.AddStudentAsync(student);

                await this.paymentStatisticsProccessingService.AddPaymentStatisticAsync(student);
                await this.groupsStatisticProccessingService.AddGroupsStatisticAsync(ensureGroup);
                await this.groupsStatisticProccessingService.AddGroupsStatisticsWithStudentsAsync(student);
            }
            return mappedStudents;
        }
        private Student MapToStudent(ExternalStudent externalStudent, Group ensureGroup)
        {
            return new Student
            {
                Id = Guid.NewGuid(),
                GivenName = externalStudent.GivenName,
                Surname = externalStudent.Surname,
                Gender = externalStudent.Gender,
                PhoneNumber = externalStudent.PhoneNumber,
                GroupId = ensureGroup.Id,
                GroupName = ensureGroup.GroupName,
            };
        }
    }
}