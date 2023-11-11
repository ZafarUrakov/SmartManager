//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using Microsoft.AspNetCore.Mvc;
using SmartManager.Services.Processings.TelegramBots;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace SmartManager.Controllers
{
    public class WebhookController : ControllerBase
    {
        [HttpPost]
        public async ValueTask<IActionResult> Post(
            [FromServices] ITelegramBotProcessingService telegramBotProcessingService,
            [FromBody] Update update)
        {
            await telegramBotProcessingService.EchoAsync(update);

            return Ok();
        }
    }
}
