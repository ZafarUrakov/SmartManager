//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using SmartManager.Brokers.Loggings;
using SmartManager.Models.Students;
using SmartManager.Services.Foundations.Students;
using SmartManager.Services.Processings.Groups;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartManager.Services.Processings.Students
{
    public class StudentProcessingService : IStudentProcessingService
    {
        private readonly IStudentService studentService;
        private readonly IGroupProcessingService groupProcessingService;
        private readonly ILoggingBroker loggingBroker;

        public StudentProcessingService(
            IStudentService studentService,
            IGroupProcessingService groupProcessingService,
            ILoggingBroker loggingBroker)
        {
            this.studentService = studentService;
            this.groupProcessingService = groupProcessingService;
            this.loggingBroker = loggingBroker;
        }

        public async ValueTask<Student> AddStudentAsync(Student student)
        {
            student.Id = Guid.NewGuid();

            var newGroup = await this.groupProcessingService.EnsureGroupExistsByName(student.GroupName);

            student.Group = newGroup;

            return await this.studentService.AddStudentAsync(student);
        }

        public async ValueTask<Student> RetrieveStudentByIdAsync(Guid studentid) =>
            await this.studentService.RetrieveStudentByIdAsync(studentid);

        public IQueryable<Student> RetrieveAllStudents() =>
            this.studentService.RetrieveAllStudents();

        public async ValueTask<Student> ModifyStudentAsync(Student student)
        {
            var newGroup = await this.groupProcessingService.EnsureGroupExistsByName(student.GroupName);

            student.GroupId = newGroup.Id;

            return await this.studentService.ModifyStudentAsync(student);
        }

        public async ValueTask<Student> ModifyStudentWithGroupAsync(Student student) =>
            await this.studentService.ModifyStudentAsync(student);

        public async ValueTask<Student> RemoveStudentAsync(Guid studentid) =>
            await this.studentService.RemoveStudentAsync(studentid);
    }
}
