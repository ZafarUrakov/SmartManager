//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using Microsoft.EntityFrameworkCore;
using SmartManager.Models.Statistics;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartManager.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Statistic> Statistics { get; set; }
        public async ValueTask<Statistic> InsertStatisticAsync(Statistic statistic) =>
            await InsertAsync(statistic);

        public IQueryable<Statistic> SelectAllStatistics() =>
            SelectAll<Statistic>().AsQueryable();

        public async ValueTask<Statistic> SelectStatisticByIdAsync(Guid statisticId) =>
            await SelectAsync<Statistic>(statisticId);

        public async ValueTask<Statistic> UpdateStatisticAsync(Statistic statistic) =>
            await UpdateAsync(statistic);

        public async ValueTask<Statistic> DeleteStatisticAsync(Statistic statistic) =>
            await DeleteAsync(statistic);

    }
}
