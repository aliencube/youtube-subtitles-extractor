using System.Runtime.CompilerServices;

using Aliencube.YouTubeSubtitlesExtractor;
using Aliencube.YouTubeSubtitlesExtractor.Models;


//Url with some subtitles (testing purposes)
//https://www.youtube.com/watch?v=KLe7Rxkrj94 

Console.WriteLine("Input youtube url: ");
string? youtubeUrl = Console.ReadLine();

if ( string.IsNullOrWhiteSpace( youtubeUrl) )
{
    Console.WriteLine("Error, youtube url empty");
    return;
}

var http = new HttpClient();
var youtube = new YouTubeVideo(http);

var details = await youtube.ExtractVideoDetailsAsync(youtubeUrl);

if (details is not null)
{
    
    Console.WriteLine("AvailableLanguageCodes: ");
    for (var i = 0; i < details.AvaiableLanguageCodes.Count; i++)
    {
        Console.WriteLine(details.AvaiableLanguageCodes[i]);
    }
    
    Console.WriteLine("Input language code: ");
    string? langCode = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(langCode))
    {
        Console.WriteLine("Error, language code empty");
        return;
    }
   

    var videoSubs = await youtube.ExtractSubtitleAsync(youtubeUrl, langCode);
    if (videoSubs is not null && videoSubs.Content is not null)
    {
        string colSep = "\t";
        Console.WriteLine($"Start   {colSep} End     {colSep} Subtitle");
        for (var i = 0; i < videoSubs.Content.Count; i++)
        {
            var subsContent = videoSubs.Content[i];

            string substart = "-", subend = "-";
            string subtext = subsContent.Text ?? "-";

            if (subsContent.Start.HasValue)
            {
                float start_ms = subsContent.Start ?? 0;
                start_ms = start_ms * 1000;

                TimeSpan start_ts = TimeSpan.FromMilliseconds(start_ms);
                substart = start_ts.ToString(@"hh\:mm\:ss");

                if (subsContent.Duration.HasValue)
                {
                    float duration_ms = subsContent.Duration ?? 0;
                    duration_ms = duration_ms * 1000;

                    TimeSpan duration_ts = TimeSpan.FromMilliseconds(duration_ms);
                    subend = start_ts.Add(duration_ts).ToString(@"hh\:mm\:ss");
                }
            }

            Console.WriteLine($"{substart} {colSep} {subend} {colSep} {subtext}");

        }
    }
    else
    { 
        Console.WriteLine("Error Extracting Subtitles"); 
    }

}

