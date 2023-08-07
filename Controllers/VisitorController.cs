using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Church.Models;
using Church.ServiceInterfaces;
using Church.DTO;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;

namespace Church.Controllers
{
    [Authorize(Roles = "Admin,Pastor,Deacon")]
    [Route("api/[controller]")]
    [ApiController]
    public class VisitorController : ControllerBase
    {
        private readonly IVisitorService _visitorService;
        private readonly IMapper _visitorMapper;

        public VisitorController(IVisitorService visitorService, IMapper visitorMapper)
        {
            _visitorService = visitorService;
            _visitorMapper = visitorMapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VisitorDTO>>> GetAllVisitors()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            // You can now use userId to filter or perform actions based on the authenticated user

            var visitors = await _visitorService.GetAllVisitors();
            return Ok(_visitorMapper.Map<IEnumerable<VisitorDTO>>(visitors));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VisitorDTO>> GetVisitor(string id)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            // You can now use userId to filter or perform actions based on the authenticated user

            var visitor = await _visitorService.GetVisitor(id);
            if (visitor == null) return NotFound();
            return Ok(_visitorMapper.Map<VisitorDTO>(visitor)); // Corrected the mapping
        }

        [HttpPost]
        public async Task<ActionResult<VisitorDTO>> PostVisitor(Visitor visitor)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            // You can now use userId to filter or perform actions based on the authenticated user

            await _visitorService.AddVisitor(visitor);
            return CreatedAtAction("GetVisitor", new { id = visitor.Id }, _visitorMapper.Map<VisitorDTO>(visitor)); // Corrected the mapping
        }

        // DELETE: api/Visitor/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVisitor(string id)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            // You can now use userId to filter or perform actions based on the authenticated user

            await _visitorService.DeleteVisitor(id);
            return Ok();
        }

        [HttpGet("by-date")]
        public IActionResult GetVisitorsByDate(DateTime date)
        {
            try
            {
                var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                // You can now use userId to filter or perform actions based on the authenticated user

                var visitors = _visitorService.GetVisitorsByDate(date);
                var visitorDTOs = visitors.Select(v => _visitorMapper.Map<VisitorDTO>(v)); // Using the mapper here
                return Ok(visitorDTOs);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
