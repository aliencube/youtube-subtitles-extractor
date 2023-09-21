namespace YouTubeSubtitlesExtractor.Models;

/// <summary>
/// This represents the video options entity.
/// </summary>
public class VideoOptions
{
    /// <summary>
    /// Gets or sets the video ID. If both VideoId and VideoUrl are set, VideoId will be used.
    /// </summary>
    public virtual string? VideoId { get; set; }

    /// <summary>
    /// Gets or sets the video URL. If both VideoId and VideoUrl are set, VideoId will be used.
    /// </summary>
    public virtual string? VideoUrl { get; set; }

    /// <summary>
    /// Gets or sets the list of language codes.
    /// </summary>
    public virtual List<string> LanguageCodes { get; set; } = new() { "en" };
}
