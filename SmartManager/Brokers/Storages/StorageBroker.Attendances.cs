//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using Microsoft.EntityFrameworkCore;
using SmartManager.Models.Attendances;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartManager.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Attendance> Attendances { get; set; }

        public async ValueTask<Attendance> InsertAttendanceAsync(Attendance attendance) =>
           await InsertAsync(attendance);

        public IQueryable<Attendance> SelectAllAttendances() =>
            SelectAll<Attendance>().AsQueryable();

        public async ValueTask<Attendance> SelectAttendanceByIdAsync(Guid attendanceId) =>
            await SelectAsync<Attendance>(attendanceId);

        public async ValueTask<Attendance> UpdateAttendanceAsync(Attendance attendance) =>
            await UpdateAsync(attendance);

        public async ValueTask<Attendance> DeleteAttendanceAsync(Attendance attendance) =>
            await DeleteAsync(attendance);
    }
}
