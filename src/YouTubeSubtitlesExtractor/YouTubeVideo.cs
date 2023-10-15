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
    private static readonly Regex youtubeVideoDetails = new(@"videoDetails"":(\{.*?\[.*?\]\}.*?\})");

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

        var subtitles = new List<Subtitle>();
        var captionTracks = await this.ExtractCaptionTracksAsync(options.VideoId).ConfigureAwait(false);
        if (captionTracks == default)
        {
            return subtitles;
        }

        foreach (var code in options.LanguageCodes)
        {
            var tracks = captionTracks.Where(p => p.LanguageCode.Equals(code, StringComparison.InvariantCultureIgnoreCase));
            if (tracks.Any() == false)
            {
                continue;
            }

            var track = tracks.Count() > 1
                ? tracks.SingleOrDefault(p => string.IsNullOrWhiteSpace(p.Kind) == true)
                : tracks.SingleOrDefault();
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

    /// <inheritdoc/>
    public async Task<VideoDetails> ExtractVideoDetailsAsync(string videoUrl)
    {
        var videoId = this.GetVideoId(videoUrl);
        if (string.IsNullOrWhiteSpace(videoId))
        {
            throw new ArgumentException("Video ID is invalid.", nameof(videoId));
        }

        var details = default(VideoDetails);
        var url = $"https://www.youtube.com/watch?v={videoId}";
        var page = await this._http.GetStringAsync(url).ConfigureAwait(false);
        if (page.Contains("videoDetails") == false)
        {
            return details;
        }

        var match = youtubeVideoDetails.Match(page);
        if (match.Success == false)
        {
            return details;
        }

        details = JsonConvert.DeserializeObject<VideoDetails>(match.Groups[1].Value);

        var captionTracks = await this.ExtractCaptionTracksAsync(videoId).ConfigureAwait(false);
        details.AvaiableLanguageCodes = captionTracks.GroupBy(p => p.LanguageCode)
                                                     .Select(g => g.First().LanguageCode)
                                                     .ToList();
        return details;
    }

    private async Task<List<CaptionTrack>> ExtractCaptionTracksAsync(string videoId)
    {
        if (string.IsNullOrWhiteSpace(videoId))
        {
            throw new ArgumentException("Video ID is invalid.", nameof(videoId));
        }

        var tracks = default(List<CaptionTrack>);
        var url = $"https://www.youtube.com/watch?v={videoId}";
        var page = await this._http.GetStringAsync(url).ConfigureAwait(false);

        if (page.Contains("captionTracks") == false)
        {
            return tracks;
        }

        var match = youtubeCaptionTracks.Match(page);
        if (match.Success == false)
        {
            return tracks;
        }

        tracks = JsonConvert.DeserializeObject<List<CaptionTrack>>(match.Groups[1].Value);

        return tracks;
    }
}
