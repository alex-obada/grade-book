using Siemens.Internship2026.GradeBook.Models;

namespace Siemens.Internship2026.GradeBook.Interfaces;

public interface IItemStats
{
    ItemStats GetStats(IEnumerable<Item> items);
}
