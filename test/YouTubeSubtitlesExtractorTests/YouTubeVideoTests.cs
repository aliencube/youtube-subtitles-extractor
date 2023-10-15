using FluentAssertions;

using Aliencube.YouTubeSubtitlesExtractor;
using Aliencube.YouTubeSubtitlesExtractor.Models;

namespace Aliencube.YouTubeSubtitlesExtractorTests
{
    [TestClass]
    public class YouTubeVideoTests
    {
        [TestMethod]
        public void Given_NullParameter_When_Initiated_Then_It_Should_Throw_Exception()
        {
            Action action = () => new YouTubeVideo(null);

            action.Should().Throw<ArgumentNullException>();
        }

        [DataTestMethod]
        [DataRow("https://www.youtube.com/watch?v=i8tMiWHK05M&list=PLdo4fOcmZ0oUm1AcifkaP6cEAKF74FtDH&index=3", "i8tMiWHK05M")]
        [DataRow("https://www.youtube.com/watch?v=i8tMiWHK05M", "i8tMiWHK05M")]
        [DataRow("https://youtu.be/i8tMiWHK05M?feature=shared", "i8tMiWHK05M")]
        [DataRow("https://youtube.com/live/Lyu6T5GDBL8?feature=share", "Lyu6T5GDBL8")]
        [DataRow("https://youtube.com/Lyu6T5GDBL8?feature=share", "")]
        public void Given_VideoUrl_When_GetVideoId_Invoked_Then_It_Should_Return_VideoId(string videoUrl, string expected)
        {
            var http = new HttpClient();
            var sut = new YouTubeVideo(http);

            var result = sut.GetVideoId(videoUrl);

            result.Should().Be(expected);
        }

        [DataTestMethod]
        [DataRow("", "")]
        [DataRow("", "https://youtube.com/Lyu6T5GDBL8?feature=share")]
        public void Given_Invalid_VideoId_When_ExtractSubtitlesAsync_Invoked_Then_It_Should_Throw_Exception(string videoId, string videoUrl)
        {
            var http = new HttpClient();
            var sut = new YouTubeVideo(http);

            var options = new VideoOptions() { VideoId = videoId, VideoUrl = videoUrl };

            Func<Task> result = async () => await sut.ExtractSubtitlesAsync(options).ConfigureAwait(false);

            result.Should().ThrowAsync<ArgumentException>();
        }

        [DataTestMethod]
        [DataRow("https://www.youtube.com/watch?v=i8tMiWHK05M", 0)]
        public async Task Given_VideoUrl_When_ExtractSubtitlesAsync_Invoked_Then_It_Should_Return_Subtitles(string videoUrl, int expected)
        {
            var http = new HttpClient();
            var sut = new YouTubeVideo(http);

            var options = new VideoOptions() { VideoUrl = videoUrl };

            var result = await sut.ExtractSubtitlesAsync(options).ConfigureAwait(false);

            result.Should().HaveCount(expected);
        }

        [DataTestMethod]
        [DataRow("https://www.youtube.com/watch?v=i8tMiWHK05M", "en", 1)]
        [DataRow("https://www.youtube.com/watch?v=i8tMiWHK05M", "ko", 1)]
        [DataRow("https://www.youtube.com/watch?v=i8tMiWHK05M", "en,ko", 2)]
        [DataRow("https://www.youtube.com/watch?v=i8tMiWHK05M", "ja", 0)]
        public async Task Given_VideoUrl_And_LanguageCode_When_ExtractSubtitlesAsync_Invoked_Then_It_Should_Return_Subtitles(string videoUrl, string languageCodes, int expected)
        {
            var http = new HttpClient();
            var sut = new YouTubeVideo(http);

            var options = new VideoOptions()
            {
                VideoUrl = videoUrl,
                LanguageCodes = languageCodes.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries).ToList()
            };

            var result = await sut.ExtractSubtitlesAsync(options).ConfigureAwait(false);

            result.Should().HaveCount(expected);
        }

        [DataTestMethod]
        [DataRow("https://www.youtube.com/watch?v=i8tMiWHK05M")]
        public async Task Given_VideoUrl_When_ExtractSubtitleAsync_Invoked_Then_It_Should_Return_Subtitle(string videoUrl)
        {
            var http = new HttpClient();
            var sut = new YouTubeVideo(http);

            var result = await sut.ExtractSubtitleAsync(videoUrl).ConfigureAwait(false);

            result.Should().NotBeNull();
            result.LanguageCode.Should().BeEquivalentTo("en");
        }

        [DataTestMethod]
        [DataRow("https://www.youtube.com/live/47CZqb53nCM?si=QOR3XVjcUzZSSdqX", "en")]
        [DataRow("https://www.youtube.com/watch?v=i8tMiWHK05M", "en")]
        [DataRow("https://www.youtube.com/watch?v=i8tMiWHK05M", "ko")]
        public async Task Given_VideoUrl_And_LanguageCode_When_ExtractSubtitleAsync_Invoked_Then_It_Should_Return_Subtitle(string videoUrl, string languageCode)
        {
            var http = new HttpClient();
            var sut = new YouTubeVideo(http);

            var result = await sut.ExtractSubtitleAsync(videoUrl, languageCode).ConfigureAwait(false);

            result.Should().NotBeNull();
            result.LanguageCode.Should().BeEquivalentTo(languageCode);
        }

        [TestMethod]
        public void Given_NullParameter_When_ExtractVideoDetailsAsync_Invoked_Then_It_Should_Throw_Exception()
        {
            var http = new HttpClient();
            var sut = new YouTubeVideo(http);

            Func<Task> result = async () => await sut.ExtractVideoDetailsAsync(null).ConfigureAwait(false);

            result.Should().ThrowAsync<ArgumentNullException>();
        }

        [DataTestMethod]
        [DataRow("https://www.youtube.com/live/47CZqb53nCM?si=QOR3XVjcUzZSSdqX", 1)]
        [DataRow("https://www.youtube.com/watch?v=i8tMiWHK05M", 2)]
        public async Task Given_VideoUrl_When_ExtractVideoDetailsAsync_Invoked_Then_It_Should_Return_VideoDetails(string videoUrl, int count)
        {
            var http = new HttpClient();
            var sut = new YouTubeVideo(http);

            var result = await sut.ExtractVideoDetailsAsync(videoUrl).ConfigureAwait(false);

            result.Should().NotBeNull();
            result.VideoId.Should().NotBeNullOrWhiteSpace();
            result.Title.Should().NotBeNullOrWhiteSpace();
            result.ShortDescription.Should().NotBeNullOrWhiteSpace();
            result.AvaiableLanguageCodes.Should().HaveCount(count);
        }
     }
}