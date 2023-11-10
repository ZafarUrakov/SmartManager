//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using Microsoft.EntityFrameworkCore;
using SmartManager.Models.TelegramInformations;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartManager.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<TelegramInformation> TelegramInformations { get; set; }

        public async ValueTask<TelegramInformation> InsertTelegramInformationAsync(TelegramInformation telegramInformation) =>
           await InsertAsync(telegramInformation);

        public IQueryable<TelegramInformation> SelectAllTelegramInformations() =>
            SelectAll<TelegramInformation>().AsQueryable();

        public async ValueTask<TelegramInformation> SelectTelegramInformationByIdAsync(Guid telegramInformationId) =>
            await SelectAsync<TelegramInformation>(telegramInformationId);

        public async ValueTask<TelegramInformation> UpdateTelegramInformationAsync(TelegramInformation telegramInformation) =>
            await UpdateAsync(telegramInformation);

        public async ValueTask<TelegramInformation> DeleteTelegramInformationAsync(TelegramInformation telegramInformation) =>
            await DeleteAsync(telegramInformation);
    }
}
