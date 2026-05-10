using Siemens.Internship2026.GradeBook.Models;

namespace Siemens.Internship2026.GradeBook.Dtos;

public class GradeAPIResult
{
    public IEnumerable<Grade> Items { get; set; } = [];
}
