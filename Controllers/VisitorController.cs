using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Church.Models;
using Church.ServiceInterfaces;
using Church.DTO;
using Microsoft.AspNetCore.Authorization;

namespace Church.Controllers
{
    [Authorize(Roles = "Admin,Pastor,Deacon")]
    [Route("api/[controller]")]
    [ApiController]
    public class VisitorController : ControllerBase
    {
        private readonly IVisitorService _visitorService;

        public VisitorController(IVisitorService visitorService)
        {
            _visitorService = visitorService;
        }

        // GET: api/Visitor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VisitorDTO>>> GetAllVisitors()
        {
            var visitors = await _visitorService.GetAllVisitors();
            return Ok(visitors.Select(v => new VisitorDTO
            {
                FullName = v.FullName,
                GuestOf = v.GuestOf,
                OtherRemarks = v.OtherRemarks,
                DateEntered = v.DateEntered,
                OtherGuests = v.OtherGuests
            }));
        }


        // GET: api/Visitor/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VisitorDTO>> GetVisitor(string id)
        {
            var visitor = await _visitorService.GetVisitor(id);

            if (visitor == null)
            {
                return NotFound();
            }

            var visitorDTO = new VisitorDTO
            {
                FullName = visitor.FullName,
                GuestOf = visitor.GuestOf,
                OtherRemarks = visitor.OtherRemarks,
                DateEntered = visitor.DateEntered,
                OtherGuests = visitor.OtherGuests
            };

            return visitorDTO;
        }

        // POST: api/Visitor
        [HttpPost]
        public async Task<ActionResult<VisitorDTO>> PostVisitor(Visitor visitor)
        {
            await _visitorService.AddVisitor(visitor);

            var visitorDTO = new VisitorDTO
            {
                FullName = visitor.FullName,
                GuestOf = visitor.GuestOf,
                OtherRemarks = visitor.OtherRemarks,
                DateEntered = visitor.DateEntered,
                OtherGuests = visitor.OtherGuests
            };

            return CreatedAtAction("GetVisitor", new { id = visitor.Id }, visitorDTO);
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
                var visitorDTOs = visitors.Select(v => new VisitorDTO
                {
                    FullName = v.FullName,
                    GuestOf = v.GuestOf,
                    OtherRemarks = v.OtherRemarks,
                    DateEntered = v.DateEntered,
                    OtherGuests = v.OtherGuests
                });
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
