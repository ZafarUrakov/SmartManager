//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SmartManager.Models.BotConfigurations;
using SmartManager.Services.Processings.Students;
using SmartManager.Services.Processings.TelegramInformations;
using System;
using System.Linq;
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
        private readonly ITelegramInformationProcessingService telegramInformationProcessingService;
        private readonly IStudentProcessingService studentProcessingService;

        public ConfigureWebhook(
            ILogger<ConfigureWebhook> logger,
            IServiceProvider serviceProvider,
            IConfiguration configuration,
            ITelegramInformationProcessingService telegramInformationProcessingService)
        {
            this.logger = logger;
            this.serviceProvider = serviceProvider;
            this.botConfiguration = configuration
                .GetSection("BotConfiguration").Get<BotConfiguration>();
            this.telegramInformationProcessingService = telegramInformationProcessingService;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                using var scope = this.serviceProvider.CreateScope();

                var botClient = scope.ServiceProvider.GetRequiredService<ITelegramBotClient>();

                var webhookAddress = $"https://smartmanager-b210ccd2cf61.herokuapp.com/bot/{botConfiguration.Token}";

                this.logger.LogInformation("Setting webhook");

                await botClient.SendTextMessageAsync(
                    chatId: 1924521160,
                    text: "Webhook starting work..");

                await SendMonthlyPaymentMessageToStudents(botClient);

                await botClient.SetWebhookAsync(
                        url: webhookAddress,
                        allowedUpdates: Array.Empty<UpdateType>(),
                        cancellationToken: cancellationToken);
            }
            catch (Exception ex) { throw ex; }
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

        private async ValueTask SendMonthlyPaymentMessageToStudents(ITelegramBotClient botClient)
        {
            var telegramInformation1 = this.telegramInformationProcessingService
                .RetrieveAllTelegramInformations();
            var currentDate = DateTime.Now;

            if (currentDate.Day == 10)
            {
                var students = this.studentProcessingService.RetrieveAllStudents();

                foreach (var student in students)
                {
                    var telegramInformation = this.telegramInformationProcessingService
                        .RetrieveAllTelegramInformations().FirstOrDefault(t => t.StudentId == student.Id);

                    var paymentMessage = $"Dear {student.GivenName} {student.Surname}, it's time to pay your monthly fee. Please proceed with the payment.";

                    await botClient.SendTextMessageAsync(
                        telegramInformation.TelegramId,
                        paymentMessage);
                }

                this.logger.LogInformation("Payment messages sent successfully.");
            }
        }
    }
}
