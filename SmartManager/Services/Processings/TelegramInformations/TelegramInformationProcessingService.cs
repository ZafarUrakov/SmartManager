//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using SmartManager.Models.TelegramInformations;
using SmartManager.Services.Foundations.TelegramInformations;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartManager.Services.Processings.TelegramInformations
{
    public class TelegramInformationProcessingService : ITelegramInformationProcessingService
    {

        private readonly ITelegramInformationService telegramInformationService;

        public TelegramInformationProcessingService(ITelegramInformationService telegramInformationService)
        {
            this.telegramInformationService = telegramInformationService;
        }

        public async ValueTask<TelegramInformation> AddTelegramInformationAsync(TelegramInformation telegramInformation) =>
            await this.telegramInformationService.AddTelegramInformationAsync(telegramInformation);

        public async ValueTask<TelegramInformation> RetrieveTelegramInformationByIdAsync(Guid TelegramInformationId) =>
            await this.telegramInformationService.RetrieveTelegramInformationByIdAsync(TelegramInformationId);

        public IQueryable<TelegramInformation> RetrieveAllTelegramInformations() =>
            this.telegramInformationService.RetrieveAllTelegramInformations();

        public async ValueTask<TelegramInformation> ModifyTelegramInformationAsync(TelegramInformation telegramInformation) =>
            await this.telegramInformationService.ModifyTelegramInformationAsync(telegramInformation);

        public async ValueTask<TelegramInformation> RemoveTelegramInformationAsync(Guid telegramInformationId)
        {
            TelegramInformation telegramInformation = await this.telegramInformationService.RetrieveTelegramInformationByIdAsync(telegramInformationId);

            return await this.telegramInformationService.RemoveTelegramInformationAsync(telegramInformation.Id);
        }
    }
}
