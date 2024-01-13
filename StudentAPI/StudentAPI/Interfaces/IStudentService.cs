using StudentAPI.Entity;

namespace StudentAPI.Interfaces
{
    public interface IStudentService
    {
        Task<Student> AddStudent(Student student);

        public Task<Student> GetStudentbyUId(string UId);

        public Task<List<Student>> GetAllStudents();

        public Task<Student> UpdateStudent(Student student);

        public Task<Student> DeleteStudent(Student student);
    }
}
