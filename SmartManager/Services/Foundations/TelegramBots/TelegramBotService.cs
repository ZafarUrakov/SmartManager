//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using SmartManager.Brokers.Telegrams;
using System.Threading.Tasks;

namespace SmartManager.Services.Foundations.TelegramBots
{
    public class TelegramBotService : ITelegramBotService
    {
        private readonly ITelegramBroker telegramBroker;

        public TelegramBotService(ITelegramBroker telegramBroker)
        {
            this.telegramBroker = telegramBroker;
        }

        public async ValueTask SendTextMessageAsync(
            long studentTelegramId,
            string message)
        {
            await this.telegramBroker.SendTextMessageAsync(
                studentTelegramId,
                message);
        }
        public async ValueTask SendTextMessageWithShareContactAsync(
            long studentTelegramId,
            string message)
        {
            await this.telegramBroker.SendTextMessageAsync(
                studentTelegramId,
                message);
        }
    }
}