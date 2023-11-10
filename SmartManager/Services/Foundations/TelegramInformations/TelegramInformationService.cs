//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using SmartManager.Brokers.Storages;
using SmartManager.Models.TelegramInformations;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartManager.Services.Foundations.TelegramInformations
{
    public class TelegramInformationService : ITelegramInformationService
    {

        private readonly IStorageBroker storageBroker;

        public TelegramInformationService(IStorageBroker storageBroker)
        {
            this.storageBroker = storageBroker;
        }

        public async ValueTask<TelegramInformation> AddTelegramInformationAsync(TelegramInformation telegramInformation) =>
            await storageBroker.InsertTelegramInformationAsync(telegramInformation);

        public async ValueTask<TelegramInformation> RetrieveTelegramInformationByIdAsync(Guid telegramInformationId) =>
            await storageBroker.SelectTelegramInformationByIdAsync(telegramInformationId);

        public IQueryable<TelegramInformation> RetrieveAllTelegramInformations() =>
            storageBroker.SelectAllTelegramInformations();

        public async ValueTask<TelegramInformation> ModifyTelegramInformationAsync(TelegramInformation telegramInformation) =>
            await storageBroker.UpdateTelegramInformationAsync(telegramInformation);

        public async ValueTask<TelegramInformation> RemoveTelegramInformationAsync(Guid telegramInformationId)
        {
            TelegramInformation telegramInformation = await storageBroker.SelectTelegramInformationByIdAsync(telegramInformationId);

            return await storageBroker.DeleteTelegramInformationAsync(telegramInformation);
        }
    }
}
