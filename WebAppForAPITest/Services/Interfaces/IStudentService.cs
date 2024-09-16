using WebAppForAPITest.Models;

namespace WebAppForAPITest.Services.Interfaces
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentModel>> Find();
        Task<IEnumerable<StudentModel>> GetAllStudents();
        Task<StudentModel> GetByName();
        Task<StudentModel> AddStudent();
    }
}
