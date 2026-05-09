using Microsoft.AspNetCore.Mvc;
using Siemens.Internship2026.GradeBook.Interfaces;
using Siemens.Internship2026.GradeBook.Models;

namespace Siemens.Internship2026.GradeBook.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemController : ControllerBase
{
    private readonly IItemReader _reader;
    private readonly IItemStats _statsService;
    private readonly ILogger<ItemController> _logger;

    public ItemController(IItemReader reader, IItemStats statsService, ILogger<ItemController> logger)
    {
        _reader = reader;
        _statsService = statsService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation($"[LOG] {DateTime.UtcNow}: GET api/item called");

        var items = await _reader.GetAllAsync();
        ItemStats stats = _statsService.GetStats(items); 

        _logger.LogInformation($"[LOG] Returning {stats.TotalCount} items, average value: {stats.AverageValue}");
        return Ok(new
        {
            Data = items,
            Statistics = stats
        });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        _logger.LogInformation($"[LOG] {DateTime.UtcNow}: GET api/item/{id} called");

        if (id <= 0)
        {
            _logger.LogWarning($"[LOG] Invalid id: {id}");
            return BadRequest("Id must be a positive integer.");
        }

        var item = await _reader.GetByIdAsync(id);
        if (item == null)
        {
            _logger.LogWarning($"[LOG] Item {id} not found");
            return NotFound($"Item with Id {id} was not found.");
        }

        return Ok(item);
    }
}
