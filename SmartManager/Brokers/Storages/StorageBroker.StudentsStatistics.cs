//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using Microsoft.EntityFrameworkCore;
using SmartManager.Models.StudentsStatistic;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartManager.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<StudentsStatistic> StudentsStatistics { get; set; }

        public async ValueTask<StudentsStatistic> InsertStudentsStatisticAsync(StudentsStatistic studentsStatistic) =>
           await InsertAsync(studentsStatistic);

        public IQueryable<StudentsStatistic> SelectAllStudentsStatistics() =>
            SelectAll<StudentsStatistic>().AsQueryable();

        public async ValueTask<StudentsStatistic> SelectStudentsStatisticByIdAsync(Guid studentsStatisticId) =>
            await SelectAsync<StudentsStatistic>(studentsStatisticId);

        public async ValueTask<StudentsStatistic> UpdateStudentsStatisticAsync(StudentsStatistic studentsStatistic) =>
            await UpdateAsync(studentsStatistic);

        public async ValueTask<StudentsStatistic> DeleteStudentsStatisticAsync(StudentsStatistic studentsStatistic) =>
            await DeleteAsync(studentsStatistic);
    }
}
