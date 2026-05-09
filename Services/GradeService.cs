using Siemens.Internship2026.GradeBook.Dtos;
using Siemens.Internship2026.GradeBook.Interfaces;

namespace Siemens.Internship2026.GradeBook.Services;

public class GradeService : IGradeService
{
    private readonly IGradeReader _reader;
    private readonly IGradeStats _statsService;
    private readonly ILogger<GradeService> _logger;

    public GradeService(IGradeReader reader, IGradeStats statsService, ILogger<GradeService> logger)
    {
        _reader = reader;
        _statsService = statsService;
        _logger = logger;
    }

    public async Task<GradeListResponse> GetAll()
    {
        var grades = await _reader.GetAllAsync();
        GradeStats stats = _statsService.GetStats(grades);
        var dtoGrades = grades.Select(g => new GradeResponse
        {
            Id = g.Id,
            Value = g.Value
        });

        _logger.LogInformation($"[LOG] Returning {stats.TotalCount} grades, average value: {stats.AverageValue}");
        return new GradeListResponse
        {
            Data = dtoGrades,
            Statistics = stats
        };
    }

    public async Task<GradeResponse?> GetById(int id)
    {
        if (id <= 0)
        {
            _logger.LogWarning($"[LOG] Id less than 1: {id}");
            return null;
        }

        var grade = await _reader.GetByIdAsync(id);
        if (grade == null)
        {
            _logger.LogWarning($"[LOG] Id not found: {id}");
            return null;
        }

        return new GradeResponse
        {
            Id = grade.Id,
            Value = grade.Value
        };
    }

    public async Task<IEnumerable<GradeResponse>> GetFirstNPassingGrades(int number)
    {
        if(number <= 0)
        {
            _logger.LogWarning("[LOG] Number of grades is less than 1");
            return [];
        }
        
        var grades = await _reader.GetAllAsync();
        return grades
            .Where(g => g.Value >= 5)
            .Take(number)
            .Select(g => new GradeResponse
            {
                Id = g.Id,
                Value = g.Value
            })
            .ToList();
    }
}
