using Siemens.Internship2026.GradeBook.Dtos;
namespace Siemens.Internship2026.GradeBook.Interfaces;

public interface IGradeService
{
    public Task<GradeListResponse> GetAll();
    public Task<GradeResponse?> GetById(int id);
}
