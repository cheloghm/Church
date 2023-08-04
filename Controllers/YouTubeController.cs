using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Microsoft.AspNetCore.Mvc;
using Church.Helpers;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Church.Controllers
{
    public class YouTubeController : Controller
    {
        private readonly YouTubeService _youtubeService;
        private readonly string _apiKey = "YouTube API Key HERE";
        //private readonly string _channelId = "YouTube ChannelId HERE";

        public YouTubeController()
        {
            _youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = _apiKey, // Replace with your API key.
                ApplicationName = this.GetType().ToString()
            });
        }

        public async Task<IActionResult> GetVideos(string channelId, int page = 1, int pageSize = 10)
        {
            var searchListRequest = _youtubeService.Search.List("snippet");
            searchListRequest.ChannelId = channelId; // Replace with your Channel ID.
            searchListRequest.MaxResults = 50; // Fetch the top 50 videos.
            searchListRequest.Order = SearchResource.ListRequest.OrderEnum.Date; // Order by date.

            // Call the search.list method to retrieve results matching the specified query term.
            var searchListResponse = await searchListRequest.ExecuteAsync();

            var videos = from searchResult in searchListResponse.Items
                         select new
                         {
                             searchResult.Snippet.Title,
                             searchResult.Snippet.PublishedAt,
                             VideoId = searchResult.Id.VideoId
                         };

            var pagedVideos = PaginationHelper.CreatePagedResult(videos.AsQueryable(), page, pageSize);
            return Ok(pagedVideos);
        }

        public async Task<IActionResult> GetVideoDetails(string videoId)
        {
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = _apiKey,
                ApplicationName = this.GetType().ToString()
            });

            var videoRequest = youtubeService.Videos.List("snippet, contentDetails, statistics");
            videoRequest.Id = videoId;

            var videoResponse = await videoRequest.ExecuteAsync();

            if (videoResponse.Items.Count == 0)
            {
                return NotFound("Video not found");
            }

            var video = videoResponse.Items[0];

            return Ok(video);
        }

    }
}
