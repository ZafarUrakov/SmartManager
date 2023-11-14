//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using SmartManager.Models.Students;
using System;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace SmartManager.Services.Processings.TelegramBots
{
    public interface ITelegramBotProcessingService
    {
        ValueTask EchoAsync(Update update);
        ValueTask SendAttendanceMassageToStudents(Student student, bool IsPresent);
        ValueTask SendPaymentMessageToStudents(Student student, bool isPaid, decimal amount);
        ValueTask SendReminderMessageToStudents(Guid studentId, bool remainder);
        ValueTask FarewellMessageToStudent(Student student);
        ValueTask HandleErrorAsync(Exception ex);
    }
}
