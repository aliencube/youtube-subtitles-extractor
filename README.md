# ![Icon](https://raw.githubusercontent.com/aliencube/youtube-subtitles-extractor/main/icons/YouTubeSubtitlesExtractor-64x64.png) YouTube Subtitles Extractor

![Build and Test](https://github.com/aliencube/youtube-subtitles-extractor/workflows/Build%20and%20Test/badge.svg) [![downloads](https://img.shields.io/nuget/dt/Aliencube.YouTubeSubtitlesExtractor.svg)](https://www.nuget.org/packages/Aliencube.YouTubeSubtitlesExtractor) [![version](https://img.shields.io/nuget/v/Aliencube.YouTubeSubtitlesExtractor.svg)](https://www.nuget.org/packages/Aliencube.YouTubeSubtitlesExtractor)

This is the NuGet package library that retrieves subtitles from a given YouTube video, inspired by [@devhims](https://github.com/devhims)' [YouTube Caption Extractor](https://github.com/devhims/youtube-caption-extractor).

## Known Issues

- If you use this library on your [Blazor WebAssembly](https://learn.microsoft.com/aspnet/core/blazor/hosting-models#blazor-webassembly) project, you might encounter the [CORS error](https://developer.mozilla.org/docs/Web/HTTP/CORS). The only workaround is to use a facade API to retrieve the subtitles.

## Getting Started

1. Install the [NuGet package](https://www.nuget.org/packages/Aliencube.YouTubeSubtitlesExtractor) to your project.
1. Add the namespace to your code.

    ```csharp
    using Aliencube.YouTubeSubtitlesExtractor;
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
    // Extract video details, including the list of available language codes, from the given YouTube video URL.
    VideoDetails details = await youtube.ExtractVideoDetailsAsync(youtubeUrl);

    // Extract a single subtitle from the given YouTube video URL.
    // - defaults to English (en)
    Subtitle subtitle = await youtube.ExtractSubtitleAsync(youtubeUrl);

    // Extract a single subtitle from the given YouTube video URL with the specified language code.
    // eg) Korean (ko)
    Subtitle subtitle = await youtube.ExtractSubtitleAsync(youtubeUrl, "ko");

    // Extract list of subtitles from the given VideoOptions instance.
    // eg) English and Korean (ko)
    var options = new VideoOptions { Url = youtubeUrl, LanguageCodes = { "en", "ko" } };
    List<Subtitle> subtitles = await youtube.ExtractSubtitlesAsync(options);
    ```

## Issues or Feedbacks

Please leave any issues or feedbacks on the [GitHub Issue page](https://github.com/aliencube/youtube-subtitles-extractor/issues).

## TO-DOs

- [ ] Extract YouTube video details including title, description and available subtitle languages.
- [ ] List of available subtitle languages.

## Acknowledgments

- Icons made by [Freepik](https://www.flaticon.com/authors/freepik) from [flaticon.com](https://flaticon.com/).
