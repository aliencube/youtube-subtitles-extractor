using System.Text.RegularExpressions;

using Newtonsoft.Json;

using Aliencube.YouTubeSubtitlesExtractor.Abstractions;
using Aliencube.YouTubeSubtitlesExtractor.Models;

namespace Aliencube.YouTubeSubtitlesExtractor;

/// <summary>
/// This represents the YouTube video entity.
/// </summary>
public class YouTubeVideo : IYouTubeVideo
{
    private static readonly Regex youtubeUrl = new(@"(?:youtube\.com\/watch\?v=|youtu\.be\/|youtube\.com\/.*[&?#]v=|youtube\.com\/live\/)([\w-]{11})");
    private static readonly Regex youtubeCaptionTracks = new(@"captionTracks"":(\[.*?\])");

    private readonly HttpClient _http;

    /// <summary>
    /// Initializes a new instance of the <see cref="YouTubeVideo"/> class.
    /// </summary>
    /// <param name="http"><see cref="HttpClient"/> instance.</param>
    public YouTubeVideo(HttpClient http)
    {
        this._http = http ?? throw new ArgumentNullException(nameof(http));
    }

    /// <inheritdoc/>
    public string GetVideoId(string videoUrl)
    {
        var match = youtubeUrl.Match(videoUrl);

        return match.Success ? match.Groups[1].Value : string.Empty;
    }

    /// <inheritdoc/>
    public async Task<VideoDetail> GetVideoDetail(string videoUrl)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public async Task<Subtitle> ExtractSubtitleAsync(string videoUrl, string languageCode = "en")
    {
        var options = new VideoOptions() { VideoUrl = videoUrl, LanguageCodes = { languageCode } };

        var subtitles = await this.ExtractSubtitlesAsync(options).ConfigureAwait(false);

        return subtitles.SingleOrDefault();
    }

    /// <inheritdoc/>
    public async Task<List<Subtitle>> ExtractSubtitlesAsync(VideoOptions options)
    {
        options.VideoId = options.VideoId ?? this.GetVideoId(options.VideoUrl);
        if (string.IsNullOrWhiteSpace(options.VideoId))
        {
            throw new ArgumentException("Video ID is invalid.", nameof(options.VideoId));
        }

        var subtitles = new List<Subtitle>();

        var url = $"https://www.youtube.com/watch?v={options.VideoId}";
        var page = await this._http.GetStringAsync(url).ConfigureAwait(false);

        if (page.Contains("captionTracks") == false)
        {
            return subtitles;
        }

        var match = youtubeCaptionTracks.Match(page);
        if (match.Success == false)
        {
            return subtitles;
        }

        var captionTracks = JsonConvert.DeserializeObject<List<CaptionTrack>>(match.Groups[1].Value);

        foreach (var code in options.LanguageCodes)
        {
            var tracks = captionTracks.Where(p => p.LanguageCode.Equals(code, StringComparison.InvariantCultureIgnoreCase));
            if (tracks.Any() == false)
            {
                continue;
            }
            var track = default(CaptionTrack);
            if (tracks.Count() > 1)
            {
                track = tracks.SingleOrDefault(p => string.IsNullOrWhiteSpace(p.Kind) == true);
            }
            else
            {
                track = tracks.SingleOrDefault();
            }
            if (track == default)
            {
                continue;
            }

            var xml = await this._http.GetStringAsync(track.BaseUrl).ConfigureAwait(false);
            var subtitle = new Subtitle(code, xml);
            subtitles.Add(subtitle);
        }

        return subtitles;
    }
}
