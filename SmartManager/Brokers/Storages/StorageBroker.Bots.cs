//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using Microsoft.EntityFrameworkCore;
using SmartManager.Models.Bots;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartManager.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Bot> Bots { get; set; }

        public async ValueTask<Bot> InsertBotAsync(Bot bot) =>
           await InsertAsync(bot);

        public IQueryable<Bot> SelectAllBots() =>
            SelectAll<Bot>().AsQueryable();

        public async ValueTask<Bot> SelectBotByIdAsync(Guid botId) =>
            await SelectAsync<Bot>(botId);

        public async ValueTask<Bot> UpdateBotAsync(Bot bot) =>
            await UpdateAsync(bot);

        public async ValueTask<Bot> DeleteBotAsync(Bot bot) =>
            await DeleteAsync(bot);
    }
}
