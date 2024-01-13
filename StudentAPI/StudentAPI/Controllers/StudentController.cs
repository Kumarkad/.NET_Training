using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using StudentAPI.DTO;
using StudentAPI.Entity;
using StudentAPI.Interfaces;

namespace StudentAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StudentController : ControllerBase
    {

        public Container _container;
        public IStudentService _studentService;
        public IMapper _mapper;
        public StudentController(IStudentService studentService,IMapper mapper)
        {
            _container = GetContainer();
            _studentService = studentService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent(StudentModel studentModel)
        {
            try
            {
                Student student = new Student();

                student.Name =studentModel.Name;
                student.RollNo = studentModel.RollNo;
                student.Address = studentModel.Address;
                student.MobileNo = studentModel.MobileNo;
                student.Department = studentModel.Department;

                var response = await _studentService.AddStudent(student);

                var model = _mapper.Map<StudentModel>(response);
                return Ok(model);

            }
            catch (Exception ex)
            {
                return BadRequest("Data Adding Failed" + ex);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetStudentByUId(string UId)
        {
            var student = await _studentService.GetStudentbyUId(UId);

            if (student != null)
            {
                var model=_mapper.Map<StudentModel>(student);
                return Ok(model);
            }
            else
            {
                return Ok("No Student Found !!!");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStudent()
        {
            try
            {
               var studentList=await _studentService.GetAllStudents();

                if (studentList != null)
                {
                    List <StudentModel> students=new List<StudentModel>(); 
                    foreach(var  student in studentList)
                    {
                        StudentModel studentModel=_mapper.Map<StudentModel>(student);
                        students.Add(studentModel);
                    }
                    return Ok(students);
                }
                else
                {
                    return NotFound("No Record Found !!!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Data Get Failed");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStudent(StudentModel studentModel)
        {
            Student existingStudent = await _studentService.GetStudentbyUId(studentModel.UId);
            if (existingStudent != null)
            {
                existingStudent.Name = studentModel.Name;
                existingStudent.Address = studentModel.Address;
                existingStudent.MobileNo = studentModel.MobileNo;
                existingStudent.Department = studentModel.Department;
               

                try
                {
                    var responseStudent = await _studentService.UpdateStudent(existingStudent);
                    if (responseStudent != null)
                    {
                        return Ok("Book Issued Successfully !!!");
                    }
                    else
                    {
                        return BadRequest("Failed to Issue Book !!!");
                    }

                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

            }

            return BadRequest("Book Not Found !!!");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteStudent(string studentUId)
        {
            var student = await _studentService.GetStudentbyUId(studentUId);
            if (student != null)
            {
                try
                {
                    student.Active = false;
                    await _studentService.DeleteStudent(student);
                    return Ok("Student Deleted Successfully !!!");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }
            }
            else
            {
                return Ok("Student Not Found !!!");
            }
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
