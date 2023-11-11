//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using SmartManager.Models.TelegramInformations;
using System.Linq;
using System.Threading.Tasks;

namespace SmartManager.Services.Processings.TelegramInformations
{
    public interface ITelegramInformationProcessingService
    {
        ValueTask<TelegramInformation> AddTelegramInformationAsync(TelegramInformation telegramInformation);
        IQueryable<TelegramInformation> RetrieveAllTelegramInformations();

    }
}
