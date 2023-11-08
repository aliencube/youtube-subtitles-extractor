using Aliencube.YouTubeSubtitlesExtractor;
using Aliencube.YouTubeSubtitlesExtractor.Abstractions;
using Aliencube.YouTubeSubtitlesExtractor.ConsoleApp.Services;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args)
               .UseConsoleLifetime()
               .ConfigureServices(services =>
               {
                   services.AddHttpClient();
                   services.AddTransient<IYouTubeVideo, YouTubeVideo>();
                   services.AddTransient<IYouTubeService, YouTubeService>();
               })
               .Build();

var service = host.Services.GetRequiredService<IYouTubeService>();
await service.ExecuteAsync(args);
