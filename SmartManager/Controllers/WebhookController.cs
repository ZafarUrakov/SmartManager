//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using Microsoft.AspNetCore.Mvc;
using SmartManager.Services.Foundations.TelegramBots;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace SmartManager.Controllers
{
    public class WebhookController : ControllerBase
    {
        [HttpPost]
        public async ValueTask<IActionResult> Post(
            [FromServices] TelegramBotService telegramBotService,
            [FromBody] Update update)
        {
            await telegramBotService.EchoAsync(update);

            return Ok();
        }
    }
}
