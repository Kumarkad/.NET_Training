using Library_magmt.DTO;
using Library_magmt.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using System.Threading.Tasks;

namespace Library_magmt.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        public string URI = "https://localhost:8081";
        public string PrimaryKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
        public string DatabaseName = "library-mgmt";
        public string ContainerName = "library";

        public readonly Container _container;
        public StudentController()
        {
            _container = GetContainer();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(StudentModel studentModel)
        {
            try
            {
                Student studentEntity = new Student();

                studentEntity.Name = studentModel.Name;
                studentEntity.PRN = studentModel.PRN;
                studentEntity.MobileNo = studentModel.MobileNo;
                studentEntity.EmailId = studentModel.EmailId;
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
                studentEntity.UpdatedByName = "Kumar";
                studentEntity.UpdatedBy = "Kumar's UId";

                studentEntity.Version = 1;
                studentEntity.Active = true;
                studentEntity.Archieved = false;

                Student response = await _container.CreateItemAsync(studentEntity);

                studentModel.Name = response.Name;

                return Ok("SignUp Successfull!!! " + studentModel);

            }
            catch (Exception ex)
            {
                return BadRequest("Data Adding Failed" + ex);
            }
        }

        [HttpPost]
        public IActionResult Login(string emailId, string password)
        {
            try
            {
                
                Student student = _container.GetItemLinqQueryable<Student>(true).Where(q => q.DocumentType == "student" && q.EmailId == emailId && q.Password == password).AsEnumerable().FirstOrDefault();
                if (student != null)
                {
                    return Ok("Login Successfully !!! ");
                }
                else
                {
                    return BadRequest("Invalid Credentials !!!");
                }
                
            }
            catch (Exception ex)
            {
                return BadRequest("Login Get Failed");
            }
        }

        private Container GetContainer()
        {
            CosmosClient cosmosclient = new CosmosClient(URI, PrimaryKey);
            Database database = cosmosclient.GetDatabase(DatabaseName);
            Container container = database.GetContainer(ContainerName);
            return container;
        }
    }
}
