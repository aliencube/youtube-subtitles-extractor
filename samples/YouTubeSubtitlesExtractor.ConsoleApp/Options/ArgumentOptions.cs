namespace Aliencube.YouTubeSubtitlesExtractor.ConsoleApp.Options;

/// <summary>
/// This represents the options entity from the arguments passed.
/// </summary>
public class ArgumentOptions
{
    /// <summary>
    /// Gets or sets the YouTube video URL.
    /// </summary>
    public string VideoUrl { get; set; }

    /// <summary>
    /// Gets or sets the value indicating whether to display help or not.
    /// </summary>
    public bool Help { get; set; }

    /// <summary>
    /// Parses the arguments and returns the options entity.
    /// </summary>
    /// <param name="args">List of arguments.</param>
    /// <returns>Returns the parsed argument as <see cref="ArgumentOptions"/> instance.</returns>
    public static ArgumentOptions Parse(string[] args)
    {
        var options = new ArgumentOptions();
        for (var i = 0; i < args.Length; i++)
        {
            var arg = args[i];
            switch (arg)
            {
                case "-u":
                case "--url":
                case "--video-url":
                    options.VideoUrl = i < args.Length - 1 ? args[++i] : string.Empty;
                    break;

                case "-h":
                case "--help":
                    options.Help = true;
                    break;
            }
        }

        return options;
    }
}