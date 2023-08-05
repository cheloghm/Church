using Church.Models;
using Church.ServiceInterfaces;
using Church.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Church.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IRequestService _requestService;

        public RequestController(IRequestService requestService)
        {
            _requestService = requestService;
        }

        // GET: api/Request
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RequestDTO>>> GetAllRequests()
        {
            var requests = await _requestService.GetAllRequests();
            return Ok(requests);
        }

        // GET: api/Request/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RequestDTO>> GetRequest(string id)
        {
            var request = await _requestService.GetRequest(id);

            if (request == null)
            {
                return NotFound();
            }

            return new RequestDTO
            {
                NameOfRequestor = request.NameOfRequestor,
                Title = request.Title,
                OtherRemarks = request.OtherRemarks,
                DateEntered = request.DateEntered
            };
        }

        // POST: api/Request
        [HttpPost]
        public async Task<ActionResult<RequestDTO>> PostRequest(Request request)
        {
            await _requestService.AddRequest(request);

            return CreatedAtAction("GetRequest", new { id = request.Id }, new RequestDTO
            {
                NameOfRequestor = request.NameOfRequestor,
                Title = request.Title,
                OtherRemarks = request.OtherRemarks,
                DateEntered = request.DateEntered
            });
        }

        // DELETE: api/Request/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequest(string id)
        {
            await _requestService.DeleteRequest(id);
            return Ok();
        }

        // GET: api/Request/Date/{date}
        [HttpGet("Date/{date}")]
        public async Task<ActionResult<IEnumerable<RequestDTO>>> GetRequestsByDate(DateTime date)
        {
            var requests = await _requestService.GetRequestsByDate(date);

            if (requests == null)
            {
                return NotFound();
            }

            return Ok(requests.Select(r => new RequestDTO
            {
                NameOfRequestor = r.NameOfRequestor,
                Title = r.Title,
                OtherRemarks = r.OtherRemarks,
                DateEntered = r.DateEntered
            }));
        }

    }
}
