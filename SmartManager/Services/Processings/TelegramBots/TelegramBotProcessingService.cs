//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using Microsoft.Extensions.Logging;
using SmartManager.Models.Students;
using SmartManager.Models.TelegramInformations;
using SmartManager.Services.Foundations.TelegramBots;
using SmartManager.Services.Processings.Students;
using SmartManager.Services.Processings.TelegramInformations;
using System;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SmartManager.Services.Processings.TelegramBots
{
    public class TelegramBotProcessingService : ITelegramBotProcessingService
    {
        private readonly ILogger<TelegramBotService> logger;
        private readonly ITelegramBotService telegramBotService;
        private readonly IStudentProcessingService studentProcessingService;
        private readonly ITelegramInformationProcessingService telegramInformationProcessingService;
        public TelegramBotProcessingService(
            ILogger<TelegramBotService> logger,
            ITelegramBotService telegramBotService,
            IStudentProcessingService studentProcessingService,
            ITelegramInformationProcessingService telegramInformationProcessingService)
        {
            this.logger = logger;
            this.telegramBotService = telegramBotService;
            this.studentProcessingService = studentProcessingService;
            this.telegramInformationProcessingService = telegramInformationProcessingService;
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

        public async ValueTask SendAttendanceMassageToStudents(Student student, bool IsPresent)
        {
            var date = DateTimeOffset.Now;

            var telegramInformation = this.telegramInformationProcessingService
                .RetrieveAllTelegramInformations().FirstOrDefault(t => t.StudentId == student.Id);
            if (IsPresent is true)
            {
                await this.telegramBotService.SendTextMessageAsync(
                       telegramInformation.TelegramId,
                       $"🧠Smart Manager🧠\n\nHello {student.GivenName} {student.Surname} is absent today! " +
                       $"Date of notification: {date.Day}-{date.Month}-{date.Year}");
            }
            else
            {
                await this.telegramBotService.SendTextMessageAsync(
                      telegramInformation.TelegramId,
                      $"🧠Smart Manager🧠\n\nHello {student.GivenName} {student.Surname} is not present today! " +
                      $"Date of notification: {date.Day}-{date.Month}-{date.Year}");
            }
        }

        public async ValueTask SendPaymentMessageToStudents(Student student, bool IsPaid)
        {
            var date = DateTimeOffset.Now;

            var telegramInformation = this.telegramInformationProcessingService
                .RetrieveAllTelegramInformations().FirstOrDefault(t => t.StudentId == student.Id);

            if (IsPaid is true)
            {
                await this.telegramBotService.SendTextMessageAsync(
                       telegramInformation.TelegramId,
                       $"🧠Smart Manager🧠\n\nHello {student.GivenName} {student.Surname}, " +
                       $"your payment has been successfully received. " +
                       $"Good study!❤️ Date of notification: {date.Day}-{date.Month}-{date.Year}");
            }
        }

        public async ValueTask SendReminderMessageToStudents(Guid studentId, bool remainder)
        {
            var date = DateTimeOffset.Now;

            var student = await this.studentProcessingService.RetrieveStudentByIdAsync(studentId);

            var telegramInformation = this.telegramInformationProcessingService
                .RetrieveAllTelegramInformations()
                .FirstOrDefault(t => t.StudentId == student.Id);

            if (remainder is true)
            {
                await this.telegramBotService.SendTextMessageAsync(
                    telegramInformation.TelegramId,
                    $"🧠Smart Manager🧠\n\nHello {student.GivenName} {student.Surname}, " +
                    $"a friendly reminder that your payment is due soon. " +
                    $"Please ensure timely payment. Thank you😊! Date of notification: " +
                    $"{date.Day}-{date.Month}-{date.Year}");
            }
        }

        private async ValueTask BotOnMessageRecieved(Message message)
        {
            if (IsUserRegistered(message.Chat.Id))
            {
                await this.telegramBotService.SendTextMessageAsync(
                    message.Chat.Id,
                    $"🧠Smart Manager🧠\n\nYou are already registered. Further messages are not accepted."
                );
                return;
            }

            var student = this.studentProcessingService
                .RetrieveAllStudents().FirstOrDefault(s => s.PhoneNumber == message.Text);

            this.logger.LogInformation($"A message has arrieved: {message.Type}");

            if (message.Text is not null)
            {
                if (IsPhoneNumber((message.Text)))
                {
                    if (student is null)
                    {
                        await this.telegramBotService.SendTextMessageAsync(
                            message.Chat.Id,
                            $"🧠Smart Manager🧠\n\nSorry {message.Chat.FirstName} {message.Chat.LastName}, " +
                            $"I noticed that you are not in our database. Please contact support.");
                    }
                    else
                    {
                        await this.telegramBotService.SendTextMessageAsync(
                            message.Chat.Id,
                            $"🧠Smart Manager🧠\n\nThank you {student.GivenName} {student.Surname}, " +
                            $"you will receive a progress report.");

                        TelegramInformation telegramInformation = new TelegramInformation
                        {
                            Id = Guid.NewGuid(),
                            TelegramId = message.Chat.Id,
                            Message = message.Text,
                            StudentId = student.Id,
                        };

                        await this.telegramInformationProcessingService
                            . AddTelegramInformationAsync(telegramInformation);
                    }
                }
                else
                {
                    await this.telegramBotService.SendTextMessageAsync(
                        message.Chat.Id,
                        $"🧠Welcome I am Smart Manager🧠\n\n, " +
                        $"I am glad to welcome you, I am a smart assistant who will monitor your progress. " +
                        $"Please send your number📟 for my activation.");
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

        private bool IsUserRegistered(long telegramId)
        {
            var telegramInformation = this.telegramInformationProcessingService
                .RetrieveAllTelegramInformations()
                .FirstOrDefault(t => t.TelegramId == telegramId);

            if (telegramInformation == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private ValueTask UnknownUpdateTypeHandler(Update update)
        {
            this.logger.LogInformation($"Unknown upodate type: {update.Type}");

            return ValueTask.CompletedTask;
        }

        private async ValueTask BotOnCallBackQueryRecieved(CallbackQuery callbackQuery)
        {
            await telegramBotService.SendTextMessageAsync(
                callbackQuery.Message.Chat.Id,
                $"{callbackQuery.Data}");
        }
    }
}
