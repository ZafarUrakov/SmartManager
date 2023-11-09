//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SmartManager.Models.BotConfigurations;
using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace SmartManager.Services.Foundations.ConfigurWebhook
{
    public class ConfigureWebhook : IHostedService
    {
        private readonly ILogger<ConfigureWebhook> logger;
        private readonly IServiceProvider serviceProvider;
        private readonly BotConfiguration botConfiguration;
        public ConfigureWebhook(
            ILogger<ConfigureWebhook> logger,
            IServiceProvider serviceProvider,
            IConfiguration configuration)
        {
            this.logger = logger;
            this.serviceProvider = serviceProvider;
            this.botConfiguration = configuration
                .GetSection("BotConfiguration").Get<BotConfiguration>();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = this.serviceProvider.CreateScope();

            var botClient = scope.ServiceProvider.GetRequiredService<ITelegramBotClient>();

            var webhookAddress = $"https://smartmanager-b210ccd2cf61.herokuapp.com/bot/{botConfiguration.Token}";

            this.logger.LogInformation("Setting webhook");

            await botClient.SendTextMessageAsync(
                chatId: 1924521160,
                text: "Webhook starting work..");

            await botClient.SetWebhookAsync(
                    url: webhookAddress,
                    allowedUpdates: Array.Empty<UpdateType>(),
                    cancellationToken: cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            using var scope = this.serviceProvider.CreateScope();

            var botClient = scope.ServiceProvider.GetRequiredService<ITelegramBotClient>();

            this.logger.LogInformation("Setting webhook");

            await botClient.SendTextMessageAsync(
                chatId: 1924521160,
                text: "Bot sleeping");
        }
    }
}
