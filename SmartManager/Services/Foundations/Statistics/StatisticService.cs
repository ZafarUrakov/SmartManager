//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using SmartManager.Brokers.Storages;
using SmartManager.Models.Statistics;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartManager.Services.Foundations.Statistics
{
    public class StatisticService : IStatisticService
    {
        private readonly IStorageBroker storageBroker;

        public StatisticService(IStorageBroker storageBroker)
        {
            this.storageBroker = storageBroker;
        }

        public async ValueTask<Statistic> AddStatisticAsync(Statistic statistic) =>
            await this.storageBroker.InsertStatisticAsync(statistic);

        public async ValueTask<Statistic> RetrieveStatisticByIdAsync(Guid statisticid) =>
            await this.storageBroker.SelectStatisticByIdAsync(statisticid);

        public IQueryable<Statistic> RetrieveAllStatistics() =>
            this.storageBroker.SelectAllStatistics();

        public async ValueTask<Statistic> ModifyStatisticAsync(Statistic statistic) =>
            await this.storageBroker.UpdateStatisticAsync(statistic);

        public async ValueTask<Statistic> RemoveStatisticAsync(Guid statisticid)
        {
            Statistic statistic = await this.storageBroker.SelectStatisticByIdAsync(statisticid);

            return await this.storageBroker.DeleteStatisticAsync(statistic);
        }
    }
}
