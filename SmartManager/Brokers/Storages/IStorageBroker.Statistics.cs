using SmartManager.Models.Statistics;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartManager.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Statistic> InsertStatisticAsync(Statistic statistic);
        IQueryable<Statistic> SelectAllStatistics();
        ValueTask<Statistic> SelectStatisticByIdAsync(Guid statisticId);
        ValueTask<Statistic> UpdateStatisticAsync(Statistic statistic);
        ValueTask<Statistic> DeleteStatisticAsync(Statistic statistic);
    }
}
