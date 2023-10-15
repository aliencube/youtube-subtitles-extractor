using Aliencube.YouTubeSubtitlesExtractor.Models;

namespace Aliencube.YouTubeSubtitlesExtractor.Abstractions;

/// <summary>
/// This provides interfaces to the <see cref="YouTubeVideo"/> class.
/// </summary>
public interface IYouTubeVideo
{
    /// <summary>
    /// Gets the video ID from the given URL.
    /// </summary>
    /// <param name="videoUrl">YouTube video URL.</param>
    /// <returns>Returns the video ID.</returns>
    string GetVideoId(string videoUrl);

    /// <summary>
    /// Extracts the subtitles from the given video options.
    /// </summary>
    /// <param name="videoUrl">YouTube video URL.</param>
    /// <param name="languageCode">Language code.</param>
    /// <returns>Returns the <see cref="Subtitle"/> instance.</returns>
    Task<Subtitle> ExtractSubtitleAsync(string videoUrl, string languageCode = "en");

    /// <summary>
    /// Extracts the subtitles from the given video options.
    /// </summary>
    /// <param name="options"><see cref="VideoOptions"/> instance.</param>
    /// <returns>Returns the list of <see cref="Subtitle"/> instances.</returns>
    Task<List<Subtitle>> ExtractSubtitlesAsync(VideoOptions options);

    /// <summary>
    /// Extracts the details from the given video options.
    /// <summary>
    /// <params name="videoUrl">YouTube video URL.</param>
    /// <returns>Returns the list of <see cref="videoDetails"/> instances.</returns>
    Task<VideoDetails> ExtractDetailsAsync(VideoOptions options);
}
