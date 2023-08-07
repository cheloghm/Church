using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Church.Models;
using Church.ServiceInterfaces;
using Church.DTO;
using Microsoft.AspNetCore.Authorization;
using Church.Mapper;
using AutoMapper;
using Amazon.Runtime.Internal;

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
            var visitors = await _visitorService.GetAllVisitors();
            return Ok(visitors.Select(v => Ok(_visitorMapper.Map<IEnumerable<RequestDTO>>(visitors))));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VisitorDTO>> GetVisitor(string id)
        {
            var visitor = await _visitorService.GetVisitor(id);
            if (visitor == null) return NotFound();
            return Ok(_visitorMapper.Map<IEnumerable<RequestDTO>>(visitor));
        }

        [HttpPost]
        public async Task<ActionResult<VisitorDTO>> PostVisitor(Visitor visitor)
        {
            await _visitorService.AddVisitor(visitor);
            return CreatedAtAction("GetVisitor", new { id = visitor.Id }, Ok(_visitorMapper.Map<IEnumerable<RequestDTO>>(visitor)));
        }

        // DELETE: api/Visitor/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVisitor(string id)
        {
            await _visitorService.DeleteVisitor(id);
            return Ok();
        }

        [HttpGet("by-date")]
        public IActionResult GetVisitorsByDate(DateTime date)
        {
            try
            {
                var visitors = _visitorService.GetVisitorsByDate(date);
                var visitorDTOs = visitors.Select(v => Ok(_visitorMapper.Map<IEnumerable<RequestDTO>>(visitors))); // Using the mapper here
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
