//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using System.Threading.Tasks;

namespace SmartManager.Brokers.Telegrams
{
    public interface ITelegramBroker
    {
        ValueTask SendTextMessageAsync(long userTelegramId,
            string message);

        ValueTask SendTextMessageWithShareContactAsync(long userTelegramId,
            string message);
    }
}
