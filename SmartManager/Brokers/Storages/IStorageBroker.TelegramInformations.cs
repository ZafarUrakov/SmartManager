//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using SmartManager.Models.TelegramInformations;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartManager.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<TelegramInformation> InsertTelegramInformationAsync(TelegramInformation telegramInformation);
        IQueryable<TelegramInformation> SelectAllTelegramInformations();
        ValueTask<TelegramInformation> SelectTelegramInformationByIdAsync(Guid telegramInformationId);
        ValueTask<TelegramInformation> UpdateTelegramInformationAsync(TelegramInformation telegramInformation);
        ValueTask<TelegramInformation> DeleteTelegramInformationAsync(TelegramInformation telegramInformation);
    }
}
