//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using SmartManager.Brokers.Storages;
using SmartManager.Models.Bots;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartManager.Services.Foundations.Bots
{
    public class BotService
    {

        private readonly IStorageBroker storageBroker;

        public BotService(IStorageBroker storageBroker)
        {
            this.storageBroker = storageBroker;
        }

        public async ValueTask<Bot> AddBotAsync(Bot bot) =>
            await storageBroker.InsertBotAsync(bot);

        public async ValueTask<Bot> RetrieveBotByIdAsync(Guid botId) =>
            await storageBroker.SelectBotByIdAsync(botId);

        public IQueryable<Bot> RetrieveAllBots() =>
            storageBroker.SelectAllBots();

        public async ValueTask<Bot> ModifyBotAsync(Bot bot) =>
            await storageBroker.UpdateBotAsync(bot);

        public async ValueTask<Bot> RemoveBotAsync(Guid botId)
        {
            Bot bot = await storageBroker.SelectBotByIdAsync(botId);

            return await storageBroker.DeleteBotAsync(bot);
        }
    }
}
