using System.Xml.Serialization;

namespace Aliencube.YouTubeSubtitlesExtractor.Models;

/// <summary>
/// This represents the subtitle entity.
/// </summary>
public class Subtitle
{
    private static readonly XmlSerializer serialiser = new (typeof(SubtitleTranscript));

    /// <summary>
    /// Initializes a new instance of the <see cref="Subtitle"/> class.
    /// </summary>
    /// <param name="languageCode">Language code.</param>
    /// <param name="content">Subtitle content.</param>
    public Subtitle(string? languageCode = default, string? content = default)
    {
        this.LanguageCode = languageCode;
        this.Raw = content;
        this.Content = SetContent(content ?? string.Empty);
    }

    /// <summary>
    /// Gets or sets the language code.
    /// </summary>
    public virtual string? LanguageCode { get; set; } = "en";

    /// <summary>
    /// Gets the subtitle content.
    /// </summary>
    public virtual List<SubtitleText>? Content { get; private set; }

    /// <summary>
    /// Gets or sets the subtitle content in a raw format.
    /// </summary>
    public virtual string? Raw { get; set; }

    private static List<SubtitleText> SetContent(string content)
    {
        if (string.IsNullOrWhiteSpace(content))
        {
            return [];
        }

        var deserialised = default(SubtitleTranscript);
        using (var reader = new StringReader(content))
        {
            deserialised = serialiser.Deserialize(reader) as SubtitleTranscript;
        }

        return deserialised == default ? [] : deserialised.Text!;
    }
}

/// <summary>
/// This represents the transcript entity of the subtitle XML document.
/// </summary>
[Serializable]
[XmlRoot("transcript")]
public class SubtitleTranscript
{
    /// <summary>
    /// Gets or sets the list of <see cref="SubtitleText"/> instance.
    /// </summary>
    [XmlElement("text")]
    public virtual List<SubtitleText> Text { get; set; } = [];
}

/// <summary>
/// This represents the text entity of the subtitle XML document.
/// </summary>
public class SubtitleText
{
    /// <summary>
    /// Gets or sets the start time.
    /// </summary>
    [XmlAttribute("start")]
    public virtual float Start { get; set; }

    /// <summary>
    /// Gets or sets the duration.
    /// </summary>
    [XmlAttribute("dur")]
    public virtual float Duration { get; set; }

    /// <summary>
    /// Gets or sets the subtitle text.
    /// </summary>
    [XmlText]
    public virtual string Text { get; set; } = string.Empty;
}
