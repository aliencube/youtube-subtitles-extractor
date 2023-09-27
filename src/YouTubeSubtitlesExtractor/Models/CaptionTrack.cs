namespace Aliencube.YouTubeSubtitlesExtractor.Models;

/// <summary>
/// This represents the caption track entity.
/// </summary>
public class CaptionTrack
{
    /// <summary>
    /// Gets or sets the caption track URL.
    /// </summary>
    public virtual string? BaseUrl { get; set; }

    /// <summary>
    /// Gets or sets the caption track name.
    /// </summary>
    public virtual CaptionName? Name { get; set; }

    /// <summary>
    /// Gets or sets the VSS ID.
    /// </summary>
    public virtual string? VssId { get; set; }

    /// <summary>
    /// Gets or sets the caption type. One possible value is "asr".
    /// </summary>
    public virtual string? Kind { get; set; }

    /// <summary>
    /// Gets or sets the caption track language code.
    /// </summary>
    public virtual string? LanguageCode { get; set; }

    /// <summary>
    /// Gets or sets the value indicating whether the caption track is translatable or not.
    /// </summary>
    public virtual bool IsTranslatable { get; set; }
}

/// <summary>
/// This represents the caption name entity.
/// </summary>
public class CaptionName
{
    /// <summary>
    /// Gets or sets the simple text.
    /// </summary>
    public virtual string? SimpleText { get; set; }
}