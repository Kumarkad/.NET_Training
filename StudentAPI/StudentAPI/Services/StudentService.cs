using Microsoft.Azure.Cosmos;
using StudentAPI.CosmosDBServices;
using StudentAPI.Entity;
using StudentAPI.Interfaces;
using System;

namespace StudentAPI.Services
{
    public class StudentService : IStudentService
    {
        public ICosmosService _cosmosService;
        public StudentService(ICosmosService cosmosService)
        {
            _cosmosService = cosmosService;
        }
        public async Task<Student> AddStudent(Student student)
        {

            student.Id = Guid.NewGuid().ToString();
            student.UId = student.Id;
            student.DocumentType = "student";

            student.CreatedOn = DateTime.Now;
            student.CreatedByName = "Kumar";
            student.CreatedBy = "Kumar's UId";

            student.UpdatedOn = DateTime.Now;
            student.UpdatedByName = "";
            student.UpdatedBy = "";

            student.Version = 1;
            student.Active = true;
            student.Archieved = false;

            Student response = await _cosmosService.AddStudent(student);
            return response;
        }

        public async Task<Student> GetStudentbyUId(string UId)
        {
            return await _cosmosService.GetStudentbyUId(UId);
        }

        public async Task<List<Student>> GetAllStudents()
        {
            return await _cosmosService.GetAllStudents();
        }

        public async Task<Student> UpdateStudent(Student student)
        {
            student.Version++;
            student.UpdatedOn = DateTime.Now;
            student.UpdatedByName = "Kumar";
            student.UpdatedBy = "Kumar's UId";
            return await _cosmosService.UpdateStudent(student);
        }

        public async Task<Student> DeleteStudent(Student student)
        {
            return await _cosmosService.DeleteStudent(student);
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
