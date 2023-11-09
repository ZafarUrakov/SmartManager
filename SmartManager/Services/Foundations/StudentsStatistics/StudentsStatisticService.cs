//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using SmartManager.Brokers.Storages;
using SmartManager.Models.StudentsStatistic;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartManager.Services.Foundations.StudentsStatistics
{
    public class StudentsStatisticService : IStudentsStatisticService
    {
        private readonly IStorageBroker storageBroker;

        public StudentsStatisticService(IStorageBroker storageBroker)
        {
            this.storageBroker = storageBroker;
        }

        public async ValueTask<StudentsStatistic> AddStudentsStatisticAsync(StudentsStatistic studentsStatistic) =>
            await this.storageBroker.InsertStudentsStatisticAsync(studentsStatistic);

        public async ValueTask<StudentsStatistic> RetrieveStudentsStatisticByIdAsync(Guid studentsStatisticId) =>
            await this.storageBroker.SelectStudentsStatisticByIdAsync(studentsStatisticId);

        public IQueryable<StudentsStatistic> RetrieveAllStudentsStatistics() =>
            this.storageBroker.SelectAllStudentsStatistics();

        public async ValueTask<StudentsStatistic> ModifyStudentsStatisticAsync(StudentsStatistic studentsStatistic) =>
            await this.storageBroker.UpdateStudentsStatisticAsync(studentsStatistic);

        public async ValueTask<StudentsStatistic> RemoveStudentsStatisticAsync(Guid studentsStatisticId)
        {
            StudentsStatistic studentsStatistic =
                await this.storageBroker.SelectStudentsStatisticByIdAsync(studentsStatisticId);

            return await this.storageBroker.DeleteStudentsStatisticAsync(studentsStatistic);
        }
    }
}
