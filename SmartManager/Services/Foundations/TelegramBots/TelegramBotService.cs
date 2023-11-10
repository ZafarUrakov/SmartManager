//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using Microsoft.Extensions.Logging;
using SmartManager.Brokers.Telegrams;
using SmartManager.Models.Students;
using SmartManager.Models.TelegramInformations;
using SmartManager.Services.Processings.Payments;
using SmartManager.Services.Processings.Students;
using SmartManager.Services.Processings.TelegramInformations;
using System;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace SmartManager.Services.Foundations.TelegramBots
{
    public class TelegramBotService : ITelegramBotService
    {
        private readonly ILogger<TelegramBotService> logger;
        private readonly ITelegramBroker telegramBroker;
        private readonly IStudentProcessingService studentProcessingService;
        private readonly ITelegramInformationProcessingService telegramInformationProcessingService;
        private readonly IPaymentProcessingService paymentProcessingService;
        public TelegramBotService(
            ILogger<TelegramBotService> logger,
            ITelegramBroker telegramBroker,
            IStudentProcessingService studentProcessingService,
            ITelegramInformationProcessingService telegramInformationProcessingService,
            IPaymentProcessingService paymentProcessingService)
        {
            this.logger = logger;
            this.telegramBroker = telegramBroker;
            this.studentProcessingService = studentProcessingService;
            this.telegramInformationProcessingService = telegramInformationProcessingService;
            this.paymentProcessingService = paymentProcessingService;
        }

        public async ValueTask EchoAsync(Update update)
        {
            var handler = update.Type switch
            {
                UpdateType.Message => BotOnMessageRecieved(update.Message),
                UpdateType.CallbackQuery => BotOnCallBackQueryRecieved(update.CallbackQuery),
                _ => UnknownUpdateTypeHandler(update)
            };

            try
            {
                await handler;
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }
        public async ValueTask SendAttendanceMassageToStudents(Student student, bool IsPresent)
        {
            var telegramInformation = this.telegramInformationProcessingService
                .RetrieveAllTelegramInformations().FirstOrDefault(t => t.StudentId == student.Id);
            if (IsPresent is true)
            {
                await this.telegramBroker.SendTextMessageAsync(
                       telegramInformation.TelegramId,
                       $"{student.GivenName} {student.Surname} is present today!");
            }
            else
            {
                await this.telegramBroker.SendTextMessageAsync(
                      telegramInformation.TelegramId,
                      $"{student.GivenName} {student.Surname} is not present today!");
            }
        }

        public async ValueTask SendPaymentMessageToStudents(Student student, bool IsPaid)
        {
            var telegramInformation = this.telegramInformationProcessingService
                .RetrieveAllTelegramInformations().FirstOrDefault(t => t.StudentId == student.Id);

            if (IsPaid is true)
            {
                await this.telegramBroker.SendTextMessageAsync(
                       telegramInformation.TelegramId,
                       $"{student.GivenName} {student.Surname} your payment has been successfully received. Good study!");
            }
        }

        public async ValueTask SendReminderMessageToStudents(Guid studentId, bool remainder)
        {
            var student = await this.studentProcessingService.RetrieveStudentByIdAsync(studentId);

            var telegramInformation = this.telegramInformationProcessingService
                .RetrieveAllTelegramInformations()
                .FirstOrDefault(t => t.StudentId == student.Id);

            if (remainder is true)
            {
                await this.telegramBroker.SendTextMessageAsync(
                    telegramInformation.TelegramId,
                    $"Hello {student.GivenName} {student.Surname}, " +
                    $"a friendly reminder that your payment is due soon. Please ensure timely payment. Thank you!");
            }
        }


        public ValueTask HandleErrorAsync(Exception ex)
        {
            var errorMessage = ex switch
            {
                ApiRequestException apiRequestException =>
                    $"Telegram API Error :\n{apiRequestException.ErrorCode}",
                _ => ex.ToString()
            };

            this.logger.LogInformation(errorMessage);

            return ValueTask.CompletedTask;
        }

        private ValueTask UnknownUpdateTypeHandler(Update update)
        {
            this.logger.LogInformation($"Unknown upodate type: {update.Type}");

            return ValueTask.CompletedTask;
        }

        private async ValueTask BotOnCallBackQueryRecieved(CallbackQuery callbackQuery)
        {
            await telegramBroker.SendTextMessageAsync(
                callbackQuery.Message.Chat.Id,
                $"{callbackQuery.Data}");
        }
        private async ValueTask BotOnMessageRecieved(Message message)
        {
            var student = this.studentProcessingService
                .RetrieveAllStudents().FirstOrDefault(s => s.PhoneNumber == message.Text);

            this.logger.LogInformation($"A message has arrieved: {message.Type}");

            if (message.Text is not null)
            {
                if (IsPhoneNumber((message.Text)))
                {
                    if (student is null)
                    {
                        await this.telegramBroker.SendTextMessageAsync(
                            message.Chat.Id,
                            $"Sorry, you are not in our database. Contact support.");
                    }
                    else
                    {
                        await this.telegramBroker.SendTextMessageAsync(
                            message.Chat.Id,
                            $"Thank you {message.Chat.FirstName} {message.Chat.LastName}, " +
                            $"you will receive a progress report.");

                        TelegramInformation telegramInformation = new TelegramInformation
                        {
                            Id = Guid.NewGuid(),
                            TelegramId = message.Chat.Id,
                            Message = message.Text,
                            StudentId = student.Id,
                        };

                        await this.telegramInformationProcessingService.AddTelegramInformationAsync(telegramInformation);
                    }
                }
                else
                {
                    await this.telegramBroker.SendTextMessageAsync(
                        message.Chat.Id,
                        $"Welcome to Smart Manager, please send us the phone number. ");
                }
            }
        }
        private bool IsPhoneNumber(string text)
        {
            if (text.StartsWith("+") || long.TryParse(text, out _))
            {
                return true;
            }

            return false;
        }
    }
}