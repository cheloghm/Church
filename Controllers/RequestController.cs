using Amazon.Runtime.Internal;
using AutoMapper;
using Church.DTO;
using Church.Mapper;
using Church.Models;
using Church.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class RequestController : ControllerBase
{
    private readonly IRequestService _requestService;
    private readonly IMapper _requestMapper;

    public RequestController(IRequestService requestService, IMapper requestMapper)
    {
        _requestService = requestService;
        _requestMapper = requestMapper;
    }

    // GET: api/Request
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RequestDTO>>> GetAllRequests()
    {
        var requests = await _requestService.GetAllRequests();
        return Ok(_requestMapper.Map<IEnumerable<RequestDTO>>(requests));
    }

    // GET: api/Request/id
    [HttpGet("{id}")]
    public async Task<ActionResult<RequestDTO>> GetRequest()
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        var request = await _requestService.GetRequest(userId);

        if (request == null)
        {
            return NotFound();
        }

        return Ok(_requestMapper.Map<IEnumerable<RequestDTO>>(request));
    }

    // POST: api/Request
    [HttpPost]
    public async Task<ActionResult<RequestDTO>> PostRequest(Request request)
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        request.UserId = userId; // Assuming Request has a UserId property

        await _requestService.AddRequest(request);
        return CreatedAtAction("GetRequest", new { id = request.Id }, Ok(_requestMapper.Map<IEnumerable<RequestDTO>>(request)));
    }

    // DELETE: api/Request
    [HttpDelete]
    public async Task<IActionResult> DeleteRequest()
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        await _requestService.DeleteRequest(userId);
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

        return Ok(requests.Select(r => Ok(_requestMapper.Map<IEnumerable<RequestDTO>>(r))));
    }
}
