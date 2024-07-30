using DialogProcessing.BotCommands.Common;
using Infrastructure.Request;
using Telegram.Bot;

namespace DialogProcessing.BotCommands;

public class StartupCommand : IBotCommand
{
    private readonly TelegramBotClient _bot;

    public StartupCommand(TelegramBotClient bot)
    {
        _bot = bot;
    }

    public Task<bool> IsApplicable(UserRequest request, CancellationToken ct)
    {
        return Task.FromResult(request.Request.Message?.Text == "/start");
    }

    public async Task Execute(UserRequest request, CancellationToken ct)
    {
        await _bot.SendTextMessageAsync(
            chatId: request.UserTelegramId,
            "Hello! I'm a bot that can help you with your tasks. "
                + "To start, type /help to see the list of available commands.",
            cancellationToken: ct
        );
    }
}
