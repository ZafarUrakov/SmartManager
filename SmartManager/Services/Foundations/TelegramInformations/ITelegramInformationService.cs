//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using SmartManager.Models.TelegramInformations;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartManager.Services.Foundations.TelegramInformations
{
    public interface ITelegramInformationService
    {
        ValueTask<TelegramInformation> AddTelegramInformationAsync(TelegramInformation telegramInformation);
        IQueryable<TelegramInformation> RetrieveAllTelegramInformations();
        ValueTask<TelegramInformation> RemoveTelegramInformationsStatisticAsync(Guid telegramInformationsStatisticId);
    }
}
