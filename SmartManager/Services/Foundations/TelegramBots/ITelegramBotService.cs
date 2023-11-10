//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using SmartManager.Models.Students;
using System.Threading.Tasks;

namespace SmartManager.Services.Foundations.TelegramBots
{
    public interface ITelegramBotService
    {
        ValueTask SendAttendanceMassageToStudents(Student student, bool IsPresent);
        ValueTask SendPaymentMessageToStudents(Student student, bool IsPresent);
    }
}
