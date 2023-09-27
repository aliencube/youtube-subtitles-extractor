using System.Xml;

using Newtonsoft.Json;

namespace YouTubeSubtitlesExtractor.Models;

/// <summary>
/// This represents the subtitle entity.
/// </summary>
public class Subtitle
{
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
            return new List<SubtitleText>();
        }

        var doc = new XmlDocument();
        doc.LoadXml(content);

        var serialised = JsonConvert.SerializeXmlNode(doc);
        var deserialised = JsonConvert.DeserializeObject<SubtitleXmlRoot>(serialised);

        return deserialised == null ? new List<SubtitleText>() : deserialised.Transcript.Text;
    }
}

/// <summary>
/// This represents the root entity for the subtitle XML.
/// </summary>
public class SubtitleXmlRoot
{
    /// <summary>
    /// Gets or sets the <see cref="SubtitleTranscript"/> instance.
    /// </summary>
    public virtual SubtitleTranscript? Transcript { get; set; }
}

/// <summary>
/// This represents the transcript entity of the subtitle XML document.
/// </summary>
public class SubtitleTranscript
{
    /// <summary>
    /// Gets or sets the list of <see cref="SubtitleText"/> instance.
    /// </summary>
    public virtual List<SubtitleText>? Text { get; set; }
}

/// <summary>
/// This represents the text entity of the subtitle XML document.
/// </summary>
public class SubtitleText
{
    /// <summary>
    /// Gets or sets the start time.
    /// </summary>
    [JsonProperty("@start")]
    public virtual float? Start { get; set; }

    /// <summary>
    /// Gets or sets the duration.
    /// </summary>
    [JsonProperty("@dur")]
    public virtual float? Duration { get; set; }

    /// <summary>
    /// Gets or sets the subtitle text.
    /// </summary>
    [JsonProperty("#text")]
    public virtual string? Text { get; set; }
}
