//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using SmartManager.Models.Bots;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartManager.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Bot> InsertBotAsync(Bot bot);
        IQueryable<Bot> SelectAllBots();
        ValueTask<Bot> SelectBotByIdAsync(Guid botId);
        ValueTask<Bot> UpdateBotAsync(Bot bot);
        ValueTask<Bot> DeleteBotAsync(Bot bot);
    }
}
