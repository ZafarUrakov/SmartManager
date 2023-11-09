//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using SmartManager.Models.StudentsStatistic;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartManager.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<StudentsStatistic> InsertStudentsStatisticAsync(StudentsStatistic studentsStatistic);
        IQueryable<StudentsStatistic> SelectAllStudentsStatistics();
        ValueTask<StudentsStatistic> SelectStudentsStatisticByIdAsync(Guid studentsStatisticId);
        ValueTask<StudentsStatistic> UpdateStudentsStatisticAsync(StudentsStatistic studentsStatistic);
        ValueTask<StudentsStatistic> DeleteStudentsStatisticAsync(StudentsStatistic studentsStatistic);
    }
}
