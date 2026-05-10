using Siemens.Internship2026.GradeBook.Dtos;
using Siemens.Internship2026.GradeBook.Interfaces;
using Siemens.Internship2026.GradeBook.Models;

namespace Siemens.Internship2026.GradeBook.Repositories;

public sealed class GradeRepository : IGradeReader
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<GradeRepository> _logger;

    public GradeRepository(HttpClient httpClient, ILogger<GradeRepository> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<Grade?> GetByIdAsync(int id)
    {
        var grades = await GetAllAsync();
        return grades.FirstOrDefault(g => g.Id == id);
    }

    public async Task<IEnumerable<Grade>> GetAllAsync()
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<GradeAPIResult>("");
            var grades = response?.Items ?? [];
            return grades.Where(g => g.IsActive);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "[LOG] Failed to fetch data from external API");
            return [];
        }
    }
}
