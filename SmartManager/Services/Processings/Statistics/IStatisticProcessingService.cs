//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using SmartManager.Models.Statistics;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartManager.Services.Processings.Statistics
{
    public interface IStatisticProcessingService
    {
        ValueTask<Statistic> AddOrUpdateStatisticAsync();
        ValueTask<Statistic> RetrieveStatisticByIdAsync(Guid statisticid);
        IQueryable<Statistic> RetrieveAllStatistics();
        ValueTask<Statistic> ModifyStatisticAsync(Statistic statistic);
        ValueTask<Statistic> RemoveStatisticAsync(Guid statisticid);
    }
}
