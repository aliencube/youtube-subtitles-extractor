namespace Aliencube.YouTubeSubtitlesExtractor.Models;

/// <summary>
/// This represents the video details entity.
/// </summary>
public class VideoDetails
{
    /// <summary>
    /// Gets or sets the video title.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Gets or sets the video description.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the list of available subtitle languages.
    /// </summary>
    public List<string> AvailableSubtitleLanguages { get; set; }
}