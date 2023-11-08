using Aliencube.YouTubeSubtitlesExtractor.Abstractions;
using Aliencube.YouTubeSubtitlesExtractor.ConsoleApp.Options;
using Aliencube.YouTubeSubtitlesExtractor.Models;

namespace Aliencube.YouTubeSubtitlesExtractor.ConsoleApp.Services;

/// <summary>
/// This provides interfaces to the <see cref="YouTubeService"/> class.
/// </summary>
public interface IYouTubeService
{
    /// <summary>
    /// Executes the service.
    /// </summary>
    /// <param name="args">List of arguments parsed from the command line.</param>
    Task ExecuteAsync(string[] args);
}

/// <summary>
/// This represents the service entity to extract YouTube video details.
/// </summary>
public class YouTubeService : IYouTubeService
{
    private readonly IYouTubeVideo _video;

    /// <summary>
    /// Initializes a new instance of the <see cref="YouTubeService"/> class.
    /// </summary>
    /// <param name="video"><see cref="IYouTubeVideo"/> instance.</param>
    public YouTubeService(IYouTubeVideo video)
    {
        this._video = video ?? throw new ArgumentNullException(nameof(video));
    }

    /// <inheritdoc />
    public async Task ExecuteAsync(string[] args)
    {
        var options = ArgumentOptions.Parse(args);
        if (options.Help)
        {
            this.DisplayHelp();
            return;
        }

        try
        {
            var details = await this._video.ExtractVideoDetailsAsync(options.VideoUrl).ConfigureAwait(false);
            this.DisplayDetails(details);
        }
        catch(Exception ex)
        {
            Console.WriteLine("Invalid video URL");
            this.DisplayHelp();
        }
    }

    private void DisplayDetails(VideoDetails details)
    {
        Console.WriteLine($"Title:                   {details.Title}");
        Console.WriteLine($"Author:                  {details.Author}");
        Console.WriteLine($"Description:             {details.ShortDescription}");
        Console.WriteLine($"Available Language Code: {details.AvaiableLanguageCodes.Aggregate((a, b) => $"{a}, {b}")}");
    }

    private void DisplayHelp()
    {
        Console.WriteLine("Usage:");
        Console.WriteLine("  -u, --url, --video-url <url>        YouTube video URL");
        Console.WriteLine("  -h, --help                          Display help");
    }
}
