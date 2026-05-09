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

    public ItemController(IItemReader reader, IItemStats statsService)
    {
        _reader = reader;
        _statsService = statsService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        Console.WriteLine($"[LOG] {DateTime.UtcNow}: GET api/item called");

        var items = await _reader.GetAllAsync();

        ItemStats stats = _statsService.GetStats(items); 

        Console.WriteLine($"[LOG] Returning {stats.TotalCount} items, average value: {stats.AverageValue}");

        return Ok(new
        {
            Data = items,
            Statistics = stats
        });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        Console.WriteLine($"[LOG] {DateTime.UtcNow}: GET api/item/{id} called");

        if (id <= 0)
        {
            Console.WriteLine($"[LOG] Invalid id: {id}");
            return BadRequest("Id must be a positive integer.");
        }

        var item = await _reader.GetByIdAsync(id);
        if (item == null)
        {
            Console.WriteLine($"[LOG] Item {id} not found");
            return NotFound($"Item with Id {id} was not found.");
        }

        return Ok(item);
    }
}
