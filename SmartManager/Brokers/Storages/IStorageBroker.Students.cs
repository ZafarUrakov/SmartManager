//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using SmartManager.Models.Students;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartManager.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Student> InsertStudentAsync(Student student);
        IQueryable<Student> SelectAllStudents();
        ValueTask<Student> SelectStudentByIdAsync(Guid studentId);
        ValueTask<Student> UpdateAppolicantAsync(Student student);
        ValueTask<Student> DeleteStudentAsync(Student student);
    }
}
