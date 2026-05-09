using Siemens.Internship2026.GradeBook.Interfaces;
using Siemens.Internship2026.GradeBook.Models;

namespace Siemens.Internship2026.GradeBook.Repositories;

public class GradeRepository : IGradeReader
{
    protected readonly List<Grade> _grades = new();
    protected int _nextId = 1;

    public virtual Task<Grade?> GetByIdAsync(int id)
    {
        var grades = _grades.FirstOrDefault(i => i.Id == id && i.IsActive);
        return Task.FromResult(grades);
    }

    public virtual Task<IEnumerable<Grade>> GetAllAsync()
    {
        var grades = _grades.Where(i => i.IsActive).AsEnumerable();
        return Task.FromResult(grades);
    }
}
