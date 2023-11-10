//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace SmartManager.Brokers.Telegrams
{
    public class TelegramBroker : ITelegramBroker
    {
        private readonly ITelegramBotClient telegramBotClient;

        public TelegramBroker(ITelegramBotClient telegramBotClient)
        {
            this.telegramBotClient = telegramBotClient;
        }

        public async ValueTask SendTextMessageAsync(
            long studentTelegramId,
            string message)
        {
            await telegramBotClient.SendTextMessageAsync(
                chatId: studentTelegramId,
                text: message);
        }
    }
}
