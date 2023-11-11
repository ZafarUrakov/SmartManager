//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using System.Threading.Tasks;

namespace SmartManager.Services.Foundations.TelegramBots
{
    public interface ITelegramBotService
    {
        ValueTask SendTextMessageAsync(long userTelegramId,
            string message);
        ValueTask SendTextMessageWithShareContactAsync(long userTelegramId,
            string message);
    }
}
