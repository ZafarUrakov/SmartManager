//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using SmartManager.Models.Attendances;
using SmartManager.Models.Students;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartManager.Services.Processings.Attendances
{
    public interface IAttendanceProcessingService
    {
        ValueTask<Attendance> AddAttendanceAsync(Student student, bool isPresent);
        ValueTask<Attendance> RetrieveAttendanceByIdAsync(Guid attendanceid);
        IQueryable<Attendance> RetrieveAllAttendances();
        ValueTask<Attendance> ModifyAttendanceAsync(Attendance attendance);
        ValueTask<Attendance> RemoveAttendanceAsync(Guid attendanceid);
    }
}
