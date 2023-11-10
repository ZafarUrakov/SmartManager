﻿//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using Microsoft.Extensions.Logging;
using SmartManager.Brokers.Telegrams;
using SmartManager.Models.Students;
using SmartManager.Models.TelegramInformations;
using SmartManager.Services.Processings.Students;
using SmartManager.Services.Processings.TelegramInformations;
using System;
using System.Linq;
using System.ServiceModel.Channels;
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
        public TelegramBotService(
            ILogger<TelegramBotService> logger,
            ITelegramBroker telegramBroker,
            IStudentProcessingService studentProcessingService,
            ITelegramInformationProcessingService telegramInformationProcessingService)
        {
            this.logger = logger;
            this.telegramBroker = telegramBroker;
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

        public async ValueTask SendAttendanceMassageToStudents(Student student, bool IsPresent)
        {
            var telegramInformation = this.telegramInformationProcessingService
                .RetrieveAllTelegramInformations().FirstOrDefault(t => t.StudentId == student.Id);
            if (IsPresent is true)
            {
                await this.telegramBroker.SendTextMessageAsync(
                       telegramInformation.TelegramId,
                       $"{student.GivenName} {student.Surname} is present!");
            }
            else
            {
                await this.telegramBroker.SendTextMessageAsync(
                      telegramInformation.TelegramId,
                      $"{student.GivenName} {student.Surname} is not present!");
            }
        }

        private async ValueTask BotOnMessageRecieved(Telegram.Bot.Types.Message message)
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
                            $"Sorry, you are in our database. Contact support.");
                    }
                    else
                    {
                        await this.telegramBroker.SendTextMessageAsync(
                            message.Chat.Id,
                            $"Thank you {message.Chat.FirstName} " +
                            $"{message.Chat.FirstName}, you will receive a progress report.");

                        TelegramInformation telegramInformation = new TelegramInformation
                        {
                            Id = Guid.NewGuid(),
                            TelegramId = message.Chat.Id,
                            Message = message.Text,
                            StudentId = student.Id,
                        };

                        await this.telegramInformationProcessingService
                            .AddTelegramInformationAsync(telegramInformation);
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
    }
}