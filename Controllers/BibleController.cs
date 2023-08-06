using Church.ServiceInterfaces;
using Church.Models;
using Microsoft.AspNetCore.Mvc;

namespace Church.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BibleController : ControllerBase
    {
        private readonly IBibleService _bibleService;

        public BibleController(IBibleService bibleService)
        {
            _bibleService = bibleService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bible>>> GetBibles()
        {
            var bibles = await _bibleService.GetBiblesAsync();
            return Ok(bibles);
        }

        // Add other endpoints as needed

        [HttpGet("{book}/{chapter}/{verse}")]
        public async Task<IActionResult> GetVerses(string book, int chapter, int verse)
        {
            var verses = await _bibleService.GetVersesAsync(book, chapter, verse);
            return Ok(verses);
        }
    }

}
