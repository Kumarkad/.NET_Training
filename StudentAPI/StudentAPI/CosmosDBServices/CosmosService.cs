using Microsoft.Azure.Cosmos;
using StudentAPI.Entity;

namespace StudentAPI.CosmosDBServices
{
    public class CosmosService : ICosmosService
    {
        public Container _container;
        public CosmosService() 
        {
            _container = GetContainer();
        }

        public async Task<Student> AddStudent(Student student)
        {
            return await _container.CreateItemAsync<Student>(student);
        }

        public async Task<Student> DeleteStudent(Student student)
        {
            return await _container.ReplaceItemAsync(student,student.Id);
        }

        public async Task<List<Student>> GetAllStudents()
        {
            var students = _container.GetItemLinqQueryable<Student>(true).Where(q => q.DocumentType == "student" && q.Archieved==false && q.Active==true).AsEnumerable().ToList();
            return students;
        }

        public async Task<Student> GetStudentbyUId(string UId)
        {
            var student = _container.GetItemLinqQueryable<Student>(true).Where(q => q.DocumentType == "student" && q.UId == UId && q.Archieved == false && q.Active == true).AsEnumerable().FirstOrDefault();
            return student;
        }

        public async Task<Student> UpdateStudent(Student student)
        {
            return await _container.UpsertItemAsync<Student>(student);
        }

        private Container GetContainer()
        {
            string URI = Environment.GetEnvironmentVariable("cosmos-url");
            string PrimaryKey = Environment.GetEnvironmentVariable("auth-token");
            string DatabaseName = Environment.GetEnvironmentVariable("database-name");
            string ContainerName = Environment.GetEnvironmentVariable("container-name");

            CosmosClient cosmosclient = new CosmosClient(URI, PrimaryKey);
            Database database = cosmosclient.GetDatabase(DatabaseName);
            Container container = database.GetContainer(ContainerName);
            return container;
        }
    }
}
