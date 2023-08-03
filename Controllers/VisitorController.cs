using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Church.Models;
using Church.ServiceInterfaces;

namespace Church.Controllers
{
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
        public async Task<ActionResult<IEnumerable<Visitor>>> GetAllVisitors()
        {
            return Ok(await _visitorService.GetAllVisitors());
        }

        // GET: api/Visitor/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Visitor>> GetVisitor(string id)
        {
            var visitor = await _visitorService.GetVisitor(id);

            if (visitor == null)
            {
                return NotFound();
            }

            return visitor;
        }

        // POST: api/Visitor
        [HttpPost]
        public async Task<ActionResult<Visitor>> PostVisitor(Visitor visitor)
        {
            await _visitorService.AddVisitor(visitor);

            return CreatedAtAction("GetVisitor", new { id = visitor.Id }, visitor);
        }

        // DELETE: api/Visitor/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVisitor(string id)
        {
            await _visitorService.DeleteVisitor(id);
            return Ok();
        }

        [HttpGet]
        public IActionResult GetVisitorsByDate(DateTime date)
        {
            try
            {
                var visitors = _visitorService.GetVisitorsByDate(date);
                return Ok(visitors);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
