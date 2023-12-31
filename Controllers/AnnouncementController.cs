﻿using System;
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
    public class AnnouncementController : ControllerBase
    {
        private readonly IGenericService<Announcement> _announcementService; // Use the generic service for Announcement
        private readonly IMapper _mapper;

        public AnnouncementController(IGenericService<Announcement> announcementService, IMapper mapper)
        {
            _announcementService = announcementService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnnouncementDTO>>> GetAllAnnouncements()
        {
            var announcements = await _announcementService.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<AnnouncementDTO>>(announcements));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AnnouncementDTO>> GetAnnouncement(string id)
        {
            var announcement = await _announcementService.GetByIdAsync(id);
            if (announcement == null) return NotFound();
            return Ok(_mapper.Map<AnnouncementDTO>(announcement));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAnnouncement(string id, AnnouncementDTO announcementDto)
        {
            var existingAnnouncement = await _announcementService.GetByIdAsync(id);
            if (existingAnnouncement == null)
            {
                return NotFound();
            }

            var announcementToUpdate = _mapper.Map<Announcement>(announcementDto);
            announcementToUpdate.Id = id; // Ensure the ID remains the same

            var updatedAnnouncement = await _announcementService.UpdateAsync(announcementToUpdate, id);
            return Ok(_mapper.Map<AnnouncementDTO>(updatedAnnouncement));
        }

        [HttpPost]
        public async Task<ActionResult<AnnouncementDTO>> PostAnnouncement(AnnouncementDTO announcementDto)
        {
            var announcement = _mapper.Map<Announcement>(announcementDto);
            announcement.Id = null; // Ensure the ID is null so MongoDB generates a new one
            announcement.DateCreated = DateTime.UtcNow.Date; // Automatically set the date to the current date (year, month, day)

            var createdAnnouncement = await _announcementService.AddAsync(announcement);
            return CreatedAtAction("GetAnnouncement", new { id = createdAnnouncement.Id }, _mapper.Map<AnnouncementDTO>(createdAnnouncement));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnnouncement(string id)
        {
            await _announcementService.DeleteAsync(id);
            return Ok();
        }

        [HttpGet("by-date")]
        public IActionResult GetAnnouncementsByDate(DateTime date)
        {
            try
            {
                var announcements = _announcementService.GetByDate(date); // This method should be added to the generic service
                var announcementDTOs = announcements.Select(a => _mapper.Map<AnnouncementDTO>(a));
                return Ok(announcementDTOs);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<AnnouncementDTO>>> SearchAnnouncements(string query)
        {
            var announcements = await _announcementService.SearchAsync(query);
            return Ok(_mapper.Map<IEnumerable<AnnouncementDTO>>(announcements));
        }


    }
}
