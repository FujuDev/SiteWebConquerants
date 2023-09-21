using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using SiteConquerants.Models;

namespace SiteConquerants.Controllers
{
    public class YoutubeAPI
    {

        List<VideoConqu> YoutubeVideo = new List<VideoConqu>();
        private readonly IConfiguration _configuration;

        public YoutubeAPI(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<VideoConqu>> ObtenirVideosConquerant()
        {
            String playlistIdLoL = "PLtcWzmFtrt5xiWuoCbuF1zn0c87SgO257";
            string _apiKey = _configuration["YouTubeApiKey"];

            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = _apiKey,
                ApplicationName = "SiteConquerants"
            });

            var playlistElementRequest = youtubeService.PlaylistItems.List("snippet,contentDetails");
            playlistElementRequest.PlaylistId = playlistIdLoL;
            playlistElementRequest.MaxResults = 10000;

            var playlistElementResponse = await playlistElementRequest.ExecuteAsync();

            YoutubeVideo.Clear();

            foreach (var video in playlistElementResponse.Items)
            {
                YoutubeVideo.Add(new VideoConqu{ Title = video.Snippet.Title, Id = video.ContentDetails.VideoId, Description = video.Snippet.Description, UrlImg = video.Snippet.Thumbnails.High.Url});
            }
            return YoutubeVideo;
        }
    }
}
