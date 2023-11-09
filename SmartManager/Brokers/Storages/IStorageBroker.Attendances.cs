//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using SmartManager.Models.Attendances;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartManager.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Attendance> InsertAttendanceAsync(Attendance attendance);
        IQueryable<Attendance> SelectAllAttendances();
        ValueTask<Attendance> SelectAttendanceByIdAsync(Guid attendanceId);
        ValueTask<Attendance> UpdateAttendanceAsync(Attendance attendance);
        ValueTask<Attendance> DeleteAttendanceAsync(Attendance attendance);
    }
}
