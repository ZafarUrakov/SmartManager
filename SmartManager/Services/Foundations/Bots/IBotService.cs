//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using SmartManager.Models.Bots;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartManager.Services.Foundations.Bots
{
    public interface IBotService
    {
        ValueTask<Bot> AddBotAsync(Bot bot);
        ValueTask<Bot> RetrieveBotByIdAsync(Guid botId);
        IQueryable<Bot> RetrieveAllBots();
        ValueTask<Bot> ModifyBotAsync(Bot bot);
        ValueTask<Bot> RemoveBotAsync(Guid botId);
    }
}
