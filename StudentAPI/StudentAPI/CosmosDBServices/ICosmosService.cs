using StudentAPI.Entity;

namespace StudentAPI.CosmosDBServices
{
    public interface ICosmosService
    {
        Task<Student> AddStudent(Student student);

        Task<Student> GetStudentbyUId(string UId);

        Task<List<Student>> GetAllStudents();

        Task<Student> UpdateStudent(Student student);

        Task<Student> DeleteStudent(Student student);
    }
}
