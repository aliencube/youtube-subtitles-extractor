using System.Text.Json.Serialization;

namespace Aliencube.YouTubeSubtitlesExtractor.Models;

/// <summary>
/// This represents the entity of YouTube video details.
/// </summary>
public class VideoDetails
{
    /// <summary>
    /// Gets or sets the video ID.
    /// </summary>
    public virtual string? VideoId { get; set; }

    /// <summary>
    /// Gets or sets the video title.
    /// </summary>
    public virtual string? Title { get; set; }

    /// <summary>
    /// Gets the video length in seconds.
    /// </summary>
    [JsonIgnore]
    public virtual int? LengthInSeconds
    {
        get
        {
            return string.IsNullOrWhiteSpace(this.LengthInSecondsValue) ? default(int?) : Convert.ToInt32(this.LengthInSecondsValue);
        }
    }

    /// <summary>
    /// Gets or sets the video length in seconds as a string value.
    /// </summary>
    [JsonPropertyName("lengthSeconds")]
    public virtual string? LengthInSecondsValue { get; set; }

    /// <summary>
    /// Gets or sets the list of keywords of the video.
    /// </summary>
    public virtual List<string> Keywords { get; set; } = [];

    /// <summary>
    /// Gets or sets the channel ID.
    /// </summary>
    public virtual string? ChannelId { get; set; }

    /// <summary>
    /// Gets or sets the value indicating whether it's owner view or not.
    /// </summary>
    public virtual bool IsOwnerViewing { get; set; }

    /// <summary>
    /// Gets or sets the short description of the video.
    /// </summary>
    public virtual string? ShortDescription { get; set; }

    /// <summary>
    /// Gets or sets the value indicating whether it's crawlable or not.
    /// </summary>
    public virtual bool IsCrawlable { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="Models.Thumbnail"/> value.
    /// </summary>
    public virtual Thumbnail? Thumbnail { get; set; }

    /// <summary>
    /// Gets or sets the value indicating whether to allow ratings or not.
    /// </summary>
    public virtual bool AllowRatings { get; set; }

    /// <summary>
    /// Gets the view count.
    /// </summary>
    [JsonIgnore]
    public virtual int? ViewCount
    {
        get
        {
            return string.IsNullOrWhiteSpace(this.ViewCountValue) ? default(int?) : Convert.ToInt32(this.ViewCountValue);
        }
    }

    /// <summary>
    /// Gets or sets the view count as a string value.
    /// </summary>
    [JsonPropertyName("viewCount")]
    public virtual string? ViewCountValue { get; set; }

    /// <summary>
    /// Gets or sets the video author.
    /// </summary>
    public virtual string? Author { get; set; }

    /// <summary>
    /// Gets or sets the value indicating whether it's private or not.
    /// </summary>
    public virtual bool IsPrivate { get; set; }

    /// <summary>
    /// Gets or sets the value indicating whether it's unplugged corpus or not.
    /// </summary>
    public virtual bool IsUnpluggedCorpus { get; set; }

    /// <summary>
    /// Gets or sets the value indicating whether it's live content or not.
    /// </summary>
    public virtual bool IsLiveContent { get; set; }

    /// <summary>
    /// Gets or sets the list of available langauge codes.
    /// </summary>
    public virtual List<string> AvailableLanguageCodes { get; set; } = [];
}

/// <summary>
/// This represents the thumbnail entity.
/// </summary>
public class Thumbnail
{
    /// <summary>
    /// Gets or sets the list of thumbnails.
    /// </summary>
    public virtual List<ThumbnailItem> Thumbnails { get; set; } = [];
}

/// <summary>
/// This represents the thumbnail item entity.
/// </summary>
public class ThumbnailItem
{
    /// <summary>
    /// Gets or sets the thumbnail URL.
    /// </summary>
    public virtual string? Url { get; set; }

    /// <summary>
    /// Gets or sets the thumbnail width.
    /// </summary>
    public virtual int? Width { get; set; }

    /// <summary>
    /// Gets or sets the thumbnail height.
    /// </summary>
    public virtual int? Height { get; set; }
}
