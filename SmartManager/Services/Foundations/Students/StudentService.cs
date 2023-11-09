//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using SmartManager.Brokers.Loggings;
using SmartManager.Brokers.Storages;
using SmartManager.Models.Students;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartManager.Services.Foundations.Students
{
    public class StudentService : IStudentService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;

        public StudentService(IStorageBroker storageBroker, ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
        }

        public async ValueTask<Student> AddStudentAsync(Student student)
        {

            return await this.storageBroker.InsertStudentAsync(student);
        }

        public async ValueTask<Student> RetrieveStudentByIdAsync(Guid studentid)
        {
            var maybeStudent =
              await this.storageBroker.SelectStudentByIdAsync(studentid);

            return await this.storageBroker.SelectStudentByIdAsync(studentid);
        }
        public IQueryable<Student> RetrieveAllStudents() =>
            this.storageBroker.SelectAllStudents();

        public async ValueTask<Student> ModifyStudentAsync(Student student)
        {

            var maybeStudent =
                await this.storageBroker.SelectStudentByIdAsync(student.Id);


            return await this.storageBroker.UpdateAppolicantAsync(student);
        }

        public async ValueTask<Student> RemoveStudentAsync(Guid studentid)
        {
            var maybeStudent = await this.storageBroker.SelectStudentByIdAsync(studentid);

            return await this.storageBroker.DeleteStudentAsync(maybeStudent);
        }
    }
}
