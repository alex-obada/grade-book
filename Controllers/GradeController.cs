using Microsoft.AspNetCore.Mvc;
using Siemens.Internship2026.GradeBook.Interfaces;

namespace Siemens.Internship2026.GradeBook.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GradeController : ControllerBase
{
    private readonly IGradeService _gradeService;
    private readonly ILogger<GradeController> _logger;

    public GradeController(IGradeService gradeService, ILogger<GradeController> logger)
    {
        _gradeService = gradeService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation($"[LOG] {DateTime.UtcNow}: GET api/grade called");
        var response = await _gradeService.GetAll();
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        _logger.LogInformation($"[LOG] {DateTime.UtcNow}: GET api/grade/{id} called");
        if(id <= 0)
        {
            _logger.LogWarning($"[LOG] Invalid id: {id}");
            return BadRequest("Id must be a positive integer");
        }

        var gradeResponse = await _gradeService.GetById(id);
        if (gradeResponse is null)
            return NotFound($"Grade with Id {id} was not found.");

        return Ok(gradeResponse);
    }

    [HttpGet("passing")]
    public async Task<IActionResult> GetFirstNPassing([FromQuery] int number)
    {
        _logger.LogInformation($"[LOG] {DateTime.UtcNow}: GET api/grade/passing?number={number} called");
        if (number <= 0)
        {
            _logger.LogWarning("[LOG] Number of grades is less than 1");
            return BadRequest("Number must be a positive integer");
        }

        return Ok(await _gradeService.GetFirstNPassingGrades(number));
    }
}
