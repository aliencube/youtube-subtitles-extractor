using Aliencube.YouTubeSubtitlesExtractor;

Console.WriteLine("YouTube Subtitles Extractor");
Console.WriteLine("===========================");
Console.Write("Enter YouTube URL: ");

var youtubeUrl = Console.ReadLine();
if (string.IsNullOrWhiteSpace(youtubeUrl) == true)
{
    Console.WriteLine("No YouTube URL. Terminated.");
    return;
}

var http = new HttpClient();
var youtube = new YouTubeVideo(http);

var details = await youtube.ExtractVideoDetailsAsync(youtubeUrl);
if (details is null)
{
    Console.WriteLine("No video details found. Terminated.");
    return;
}

Console.WriteLine();
Console.WriteLine("YouTube video details:");
Console.WriteLine("----------------------");
Console.WriteLine($"YouTube Link:            {youtubeUrl}");
Console.WriteLine($"Title:                   {details.Title}");
Console.WriteLine($"Author:                  {details.Author}");
Console.WriteLine($"Description:             {details.ShortDescription}");
Console.WriteLine("----------------------");

Console.WriteLine();
Console.WriteLine("Available language codes: ");
foreach (var code in details.AvailableLanguageCodes)
{
    Console.WriteLine($"- {code}");
}

Console.WriteLine();
Console.Write("Enter language code: ");

var languageCode = Console.ReadLine();
if (string.IsNullOrWhiteSpace(languageCode) == true)
{
    Console.WriteLine("No language code. Terminated.");
    return;
}

var subtitle = await youtube.ExtractSubtitleAsync(youtubeUrl, languageCode);
if (subtitle is null)
{
    Console.WriteLine("No subtitle found. Terminated.");
    return;
}
if (subtitle.Content is null)
{
    Console.WriteLine("No subtitle content found. Terminated.");
    return;
}

Console.WriteLine("Start\t\tEnd\t\tSubtitle");
foreach (var content in subtitle.Content)
{
    var text = content.Text ?? "-";
    var start = TimeSpan.FromMilliseconds((content.Start ?? 0)*1000).ToString(@"hh\:mm\:ss");
    var end = TimeSpan.FromMilliseconds(((content.Start ?? 0) + (content.Duration ?? 0))*1000).ToString(@"hh\:mm\:ss");

    Console.WriteLine($"{start}\t{end}\t{text}");
}
