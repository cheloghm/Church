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
    [Route("api/[controller]")]
    [ApiController]
    public class VisitorController : ControllerBase
    {
        private readonly IGenericService<Visitor> _visitorService; // Use the generic service
        private readonly IMapper _mapper;

        public VisitorController(IGenericService<Visitor> visitorService, IMapper mapper)
        {
            _visitorService = visitorService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VisitorDTO>>> GetAllVisitors()
        {
            var visitors = await _visitorService.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<VisitorDTO>>(visitors));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VisitorDTO>> GetVisitor(string id)
        {
            var visitor = await _visitorService.GetByIdAsync(id);
            if (visitor == null) return NotFound();
            return Ok(_mapper.Map<VisitorDTO>(visitor));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVisitor(string id, VisitorDTO visitorDto)
        {
            var existingVisitor = await _visitorService.GetByIdAsync(id);
            if (existingVisitor == null)
            {
                return NotFound();
            }

            var visitorToUpdate = _mapper.Map<Visitor>(visitorDto);
            visitorToUpdate.Id = id; // Ensure the ID remains the same

            var updatedVisitor = await _visitorService.UpdateAsync(visitorToUpdate, id); // Pass both the entity and its ID
            return Ok(_mapper.Map<VisitorDTO>(updatedVisitor));
        }

        [HttpPost]
        public async Task<ActionResult<VisitorDTO>> PostVisitor(VisitorDTO visitorDto)
        {
            var visitor = _mapper.Map<Visitor>(visitorDto);
            await _visitorService.AddAsync(visitor);
            return CreatedAtAction("GetVisitor", new { id = visitor.Id }, _mapper.Map<VisitorDTO>(visitor));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVisitor(string id)
        {
            await _visitorService.DeleteAsync(id);
            return Ok();
        }

        [HttpGet("by-date")]
        public IActionResult GetVisitorsByDate(DateTime date)
        {
            try
            {
                var visitors = _visitorService.GetByDate(date); // This method should be added to the generic service
                var visitorDTOs = visitors.Select(v => _mapper.Map<VisitorDTO>(v));
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
