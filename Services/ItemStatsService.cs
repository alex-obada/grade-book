using Siemens.Internship2026.GradeBook.Interfaces;
using Siemens.Internship2026.GradeBook.Models;

namespace Siemens.Internship2026.GradeBook.Services;

public class ItemStatsService : IItemStats
{
    public ItemStats GetStats(IEnumerable<Item> items)
    {
        var itemList = items.ToList();
        var totalCount = itemList.Count;
        var averageValue = itemList.Any() ? itemList.Average(i => i.Value) : 0;

        return new ItemStats
        {
            TotalCount = totalCount,
            AverageValue = averageValue,
            RetrievedAt = DateTime.UtcNow
        };
    }
}
