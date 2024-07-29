using Infrastructure.Settings;
using Infrastructure.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static BotConfiguration AddBotConfiguration(
        this IServiceCollection services,
        IConfiguration config
    )
    {
        BotConfiguration botConfig = config.GetSection("BotConfiguration").Get<BotConfiguration>()!;

        botConfig.Token = Environment.GetEnvironmentVariable("TG_BOT_TOKEN")!;

        if (EnvironmentResolver.IsProduction)
        {
            botConfig.HostAddress = Environment.GetEnvironmentVariable("SITE_URL")!;
        }
        
        services.AddSingleton(botConfig);

        return botConfig;
    }
}
