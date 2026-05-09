using Siemens.Internship2026.GradeBook.Dtos;
using Siemens.Internship2026.GradeBook.Models;

namespace Siemens.Internship2026.GradeBook.Interfaces;

public interface IGradeStats
{
    GradeStats GetStats(IEnumerable<Grade> grades);
}
