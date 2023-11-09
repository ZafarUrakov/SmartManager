//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using SmartManager.Models.StudentsStatistic;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartManager.Services.Foundations.StudentsStatistics
{
    public interface IStudentsStatisticService
    {
        ValueTask<StudentsStatistic> AddStudentsStatisticAsync(StudentsStatistic studentsStatistic);
        ValueTask<StudentsStatistic> RetrieveStudentsStatisticByIdAsync(Guid studentsStatisticId);
        IQueryable<StudentsStatistic> RetrieveAllStudentsStatistics();
        ValueTask<StudentsStatistic> ModifyStudentsStatisticAsync(StudentsStatistic studentsStatistic);
        ValueTask<StudentsStatistic> RemoveStudentsStatisticAsync(Guid studentsStatisticId);
    }
}
