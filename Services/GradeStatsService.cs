using Siemens.Internship2026.GradeBook.Interfaces;
using Siemens.Internship2026.GradeBook.Models;

namespace Siemens.Internship2026.GradeBook.Services;

public class GradeStatsService : IGradeStats
{
    public GradeStats GetStats(IEnumerable<Grade> grades)
    {
        var gradesList = grades.ToList();
        var totalCount = gradesList.Count;
        var averageValue = gradesList.Any() ? gradesList.Average(i => i.Value) : 0;

        return new GradeStats
        {
            TotalCount = totalCount,
            AverageValue = averageValue,
            RetrievedAt = DateTime.UtcNow
        };
    }
}
