using Microsoft.AspNetCore.Mvc;
using Siemens.Internship2026.GradeBook.Interfaces;
using Siemens.Internship2026.GradeBook.Models;

namespace Siemens.Internship2026.GradeBook.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GradeController : ControllerBase
{
    private readonly IGradeReader _reader;
    private readonly IGradeStats _statsService;
    private readonly ILogger<GradeController> _logger;

    public GradeController(IGradeReader reader, IGradeStats statsService, ILogger<GradeController> logger)
    {
        _reader = reader;
        _statsService = statsService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation($"[LOG] {DateTime.UtcNow}: GET api/grade called");

        var grades = await _reader.GetAllAsync();
        GradeStats stats = _statsService.GetStats(grades); 

        _logger.LogInformation($"[LOG] Returning {stats.TotalCount} grades, average value: {stats.AverageValue}");
        return Ok(new
        {
            Data = grades,
            Statistics = stats
        });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        _logger.LogInformation($"[LOG] {DateTime.UtcNow}: GET api/grade/{id} called");

        if (id <= 0)
        {
            _logger.LogWarning($"[LOG] Invalid id: {id}");
            return BadRequest("Id must be a positive integer.");
        }

        var grade = await _reader.GetByIdAsync(id);
        if (grade == null)
        {
            _logger.LogWarning($"[LOG] Grade {id} not found");
            return NotFound($"Grade with Id {id} was not found.");
        }

        return Ok(grade);
    }
}
