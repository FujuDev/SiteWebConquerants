using Microsoft.AspNetCore.Mvc;
using SiteConquerants.Models;
using System.Diagnostics;

namespace SiteConquerants.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly YoutubeAPI _youtubeAPI;

        public HomeController(ILogger<HomeController> logger, YoutubeAPI youtubeAPI)
        {
            _logger = logger;
            _youtubeAPI = youtubeAPI; // Injection de dépendance de YoutubeAPI
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Confidentialite()
        {
            return View();
        }

        public async Task<IActionResult> Historique()
        {
            List<VideoConqu> listeVideos = await _youtubeAPI.ObtenirVideosConquerant();
            listeVideos.Reverse();
            return View(listeVideos);
        }

        [HttpGet]
        public IActionResult RedirectionVideo(string id) {

            string url = $"https://www.youtube.com/watch?v={id}";
            return Redirect(url);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}