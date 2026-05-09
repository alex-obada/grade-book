namespace Siemens.Internship2026.GradeBook.Dtos;

public class GradeListResponse
{
    public IEnumerable<GradeResponse> Data { get; set; } = [];
    public GradeStats Statistics {  get; set; } = new GradeStats();
}
