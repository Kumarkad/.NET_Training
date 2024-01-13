using Library_magmt.DTO;
using Library_magmt.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;

namespace Library_magmt.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public string URI = Environment.GetEnvironmentVariable("cosmos-url");
        public string PrimaryKey = Environment.GetEnvironmentVariable("auth-token");
        public string DatabaseName = Environment.GetEnvironmentVariable("database-name");
        public string ContainerName = Environment.GetEnvironmentVariable("container-name");
       
        public readonly Container _container;
        
        public UserController()
        {
            _container = GetContainer();
        }

        [HttpPost]
        public async Task<IActionResult> StudentSignUp(StudentModel studentModel)
        {
            try
            {
                Student studentEntity = ToStudentEntity(studentModel);

                Student response = await _container.CreateItemAsync(studentEntity);

                studentModel = ToStudentModel(response);

                return Ok("SignUp Successfull!!! ");

            }
            catch (Exception ex)
            {
                return BadRequest("Data Adding Failed" + ex);
            }
        }

        [HttpPost]
        public IActionResult StudentLogin(string emailId, string password)
        {
            try
            {
                emailId = emailId.ToLower();
                Student student = _container.GetItemLinqQueryable<Student>(true).Where(q => q.DocumentType == "student" && q.Active==true && q.Archieved==false && q.EmailId == emailId && q.Password == password).AsEnumerable().FirstOrDefault();
                if (student != null)
                {
                    return Ok($" UId : {student.UId}  \n Login Successfully !!! ");
                }
                else
                {
                    return Unauthorized("Invalid Credentials !!!");
                }

            }
            catch (Exception ex)
            {
                return BadRequest("Login Get Failed");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteStudent(string UId)
        {
            var existingStudent = _container.GetItemLinqQueryable<Student>(true).Where(q => q.DocumentType == "student" && q.UId == UId && q.Archieved == false && q.Active == true).AsEnumerable().FirstOrDefault();
            if (existingStudent != null)
            {
                try
                {
                    existingStudent.Active = false;
                    await _container.ReplaceItemAsync(existingStudent, existingStudent.Id);
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

        [HttpPost]
        public async Task<IActionResult> LibrarianSignUp(LibrarianModel librarianModel)
        {
            try
            {
                Librarian librarianEntity = new Librarian();

                librarianEntity.Name = librarianModel.Name;
                librarianEntity.MobileNo = librarianModel.MobileNo;
                librarianEntity.EmailId = librarianModel.EmailId.ToLower();
                librarianEntity.Address = librarianModel.Address;
                librarianEntity.Password = librarianModel.Password;
                librarianEntity.InstituteId = librarianModel.InstituteId;

                librarianEntity.Id = Guid.NewGuid().ToString();
                librarianEntity.UId = librarianEntity.Id;
                librarianEntity.DocumentType = "librarian";

                librarianEntity.CreatedOn = DateTime.Now;
                librarianEntity.CreatedByName = "Kumar";
                librarianEntity.CreatedBy = "Kumar's UId";

                librarianEntity.UpdatedOn = DateTime.Now;
                librarianEntity.UpdatedByName = "";
                librarianEntity.UpdatedBy = "";

                librarianEntity.Version = 1;
                librarianEntity.Active = true;
                librarianEntity.Archieved = false;

                Librarian response = await _container.CreateItemAsync(librarianEntity);

                librarianModel =ToLibrarianModel( response);

                return Ok("SignUp Successfull!!! ");

            }
            catch (Exception ex)
            {
                return BadRequest("Data Adding Failed" + ex);
            }
        }

        [HttpPost]
        public IActionResult LibrarianLogin(string emailId, string password)
        {
            try
            {
                emailId = emailId.ToLower();
                Librarian librarian = _container.GetItemLinqQueryable<Librarian>(true).Where(q => q.DocumentType == "librarian" && q.Active == true && q.Archieved == false && q.EmailId == emailId && q.Password == password).AsEnumerable().FirstOrDefault();
                if (librarian != null)
                {
                    return Ok($" UId : {librarian.UId}  \n Login Successfully !!! ");
                }
                else
                {
                    return Unauthorized("Invalid Credentials !!!");
                }

            }
            catch (Exception ex)
            {
                return BadRequest("Login Get Failed");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteLibrarian(string UId)
        {
            var existingLibrarian = _container.GetItemLinqQueryable<Librarian>(true).Where(q => q.DocumentType == "librarian" && q.UId == UId && q.Archieved == false && q.Active == true).AsEnumerable().FirstOrDefault();
            if (existingLibrarian != null)
            {
                try
                {
                    existingLibrarian.Active = false;
                    await _container.ReplaceItemAsync(existingLibrarian, existingLibrarian.Id);
                    return Ok("Librarian Deleted Successfully !!!");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }
            }
            else
            {
                return Ok("Librarian Not Found !!!");
            }
        }

        private Container GetContainer()
        {
            CosmosClient cosmosclient = new CosmosClient(URI, PrimaryKey);
            Database database = cosmosclient.GetDatabase(DatabaseName);
            Container container = database.GetContainer(ContainerName);
            return container;
        }

        private Student ToStudentEntity(StudentModel studentModel)
        {
            Student studentEntity = new Student();

            studentEntity.Name = studentModel.Name;
            studentEntity.PRN = studentModel.PRN;
            studentEntity.MobileNo = studentModel.MobileNo;
            studentEntity.EmailId = studentModel.EmailId.ToLower();
            studentEntity.Branch = studentModel.Branch;
            studentEntity.Year = studentModel.Year;
            studentEntity.Address = studentModel.Address;
            studentEntity.Password = studentModel.Password;


            studentEntity.Id = Guid.NewGuid().ToString();
            studentEntity.UId = studentEntity.Id;
            studentEntity.DocumentType = "student";

            studentEntity.CreatedOn = DateTime.Now;
            studentEntity.CreatedByName = "Kumar";
            studentEntity.CreatedBy = "Kumar's UId";

            studentEntity.UpdatedOn = DateTime.Now;
            studentEntity.UpdatedByName = "";
            studentEntity.UpdatedBy = "";

            studentEntity.Version = 1;
            studentEntity.Active = true;
            studentEntity.Archieved = false;
            return studentEntity;
        }

        private StudentModel ToStudentModel(Student student)
        {
            var studentModel = new StudentModel();
            studentModel.Name = student.Name;
            studentModel.PRN = student.PRN;
            studentModel.MobileNo = student.MobileNo;
            studentModel.EmailId = student.EmailId;
            studentModel.Branch = student.Branch;
            studentModel.Year = student.Year;
            studentModel.Password = student.Password; 
            studentModel.Address = student.Address;
            studentModel.UId = student.UId;   

            return studentModel;
        }

        private LibrarianModel ToLibrarianModel(Librarian librarian)
        {
            var librarianModel = new LibrarianModel();

            librarianModel.Name = librarian.Name;
            librarianModel.MobileNo   =librarian.MobileNo;
            librarianModel.EmailId = librarian.EmailId;
            librarianModel.Password = librarian.Password;
            librarianModel.Address = librarian.Address;

            return librarianModel;
        }
    }
}
