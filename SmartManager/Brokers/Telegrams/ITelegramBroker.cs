//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;

namespace SmartManager.Brokers.Telegrams
{
    public interface ITelegramBroker
    {
        ValueTask SendTextMessageAsync(long userTelegramId,
            string message);
    }
}
