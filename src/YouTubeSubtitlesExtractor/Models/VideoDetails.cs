using Newtonsoft.Json;

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
    /// Gets or sets the video length in seconds.
    /// </summary>
    [JsonProperty("lengthSeconds")]
    public virtual int? LengthInSeconds { get; set; }

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
    /// Gets or sets the <see cref="Aliencube.YouTubeSubtitlesExtractor.Models.Thumbnail"/> value.
    /// </summary>
    public virtual Thumbnail? Thumbnail { get; set; }

    /// <summary>
    /// Gets or sets the value indicating whether to allow ratings or not.
    /// </summary>
    public virtual bool AllowRatings { get; set; }

    /// <summary>
    /// Gets or sets the video count.
    /// </summary>
    public virtual int? ViewCount { get; set; }

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
}

/// <summary>
/// This represents the thumbnail entity.
/// </summary>
public class Thumbnail
{
    /// <summary>
    /// Gets or sets the list of thumbnails.
    /// </summary>
    public virtual List<ThumbnailItem> Thumbnails { get; set; } = new List<ThumbnailItem>();
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
