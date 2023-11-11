﻿//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using SmartManager.Brokers.Storages;
using SmartManager.Models.TelegramInformations;
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

        public IQueryable<TelegramInformation> RetrieveAllTelegramInformations() =>
            storageBroker.SelectAllTelegramInformations();
    }
}
