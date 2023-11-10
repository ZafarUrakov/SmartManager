//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using SmartManager.Models.TelegramInformations;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartManager.Services.Processings.TelegramInformations
{
    public interface ITelegramInformationProcessingService
    {
        ValueTask<TelegramInformation> AddTelegramInformationAsync(TelegramInformation telegramInformation);
        ValueTask<TelegramInformation> RetrieveTelegramInformationByIdAsync(Guid telegramInformationId);
        IQueryable<TelegramInformation> RetrieveAllTelegramInformations();
        ValueTask<TelegramInformation> ModifyTelegramInformationAsync(TelegramInformation telegramInformation);
        ValueTask<TelegramInformation> RemoveTelegramInformationAsync(Guid telegramInformationId);
    }
}
