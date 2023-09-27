# YouTube Subtitles Extractor <img src="./icons/YouTubeSubtitlesExtractor.png" width="64" height="64" />

This is the NuGet package library that retrieves subtitles from a given YouTube video, inspired by [@devhims](https://github.com/devhims) [YouTube Caption Extractor](https://github.com/devhims/youtube-caption-extractor).

| Package | Status | Version |
| --- | --- | --- |
| [Aliencube.YouTubeSubtitlesExtractor](https://www.nuget.org/packages/Aliencube.YouTubeSubtitlesExtractor) | [![](https://img.shields.io/nuget/dt/Aliencube.YouTubeSubtitlesExtractor.svg)](https://www.nuget.org/packages/Aliencube.YouTubeSubtitlesExtractor) | [![](https://img.shields.io/nuget/v/Aliencube.YouTubeSubtitlesExtractor.svg)](https://www.nuget.org/packages/Aliencube.YouTubeSubtitlesExtractor) |

## Getting Started ![Build and Test](https://github.com/aliencube/youtube-subtitles-extractor/workflows/Build%20and%20Test/badge.svg)

1. Install the NuGet package to your project from [https://www.nuget.org/packages/Aliencube.YouTubeSubtitlesExtractor](https://www.nuget.org/packages/Aliencube.YouTubeSubtitlesExtractor).
1. Add the namespace to your code.

    ```csharp
    using YouTubeSubtitlesExtractor;
    ```

1. Get the YouTube video URL.

    ```csharp
    var youtubeUrl = "https://www.youtube.com/watch?v=i8tMiWHK05M";
    ```

1. Create an instance of `YouTubeVideo` class.

    ```csharp
    var http = new HttpClient();
    var youtube = new YouTubeVideo(http);
    ```

1. Extract subtitles from the given YouTube video URL. There are a few options to extract subtitles.

    ```csharp
    // Extract subtitles from the given YouTube video URL - defaults to English (en).
    var subtitles = await youtube.ExtractSubtitleAsync(youtubeUrl);

    // Extract subtitles from the given YouTube video URL with the specified language code. eg) Korean (ko).
    var subtitles = await youtube.ExtractSubtitleAsync(youtubeUrl, "ko");

    // Extract subtitles from the given VideoOptions instance.
    var options = new VideoOptions { Url = youtubeUrl, LanguageCode = "ko" };
    var subtitles = await youtube.ExtractSubtitleAsync(options);
    ```

## Issues or Feedbacks

Please leave any issues or feedbacks [here](https://github.com/aliencube/youtube-subtitles-extractor/issues).

## TO-DOs

- [ ] Extract YouTube video details including title, description and available subtitle languages.
- [ ] List of available subtitle languages.

## Acknowledgments

- Icons made by <a href="https://www.flaticon.com/authors/freepik" title="Freepik">Freepik</a> from <a href="https://flaticon.com/" title="Flaticon">flaticon.com</a>.
