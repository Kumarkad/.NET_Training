using Library_magmt.DTO;
using Library_magmt.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;

namespace Library_magmt.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LibrarianController : ControllerBase
    {
        public string URI = "https://localhost:8081";
        public string PrimaryKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
        public string DatabaseName = "library-mgmt";
        public string ContainerName = "library";

        public readonly Container _container;
        public LibrarianController()
        {
            _container = GetContainer();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(LibrarianModel librarianModel)
        {
            try
            {
                Librarian librarianEntity = new Librarian();

                librarianEntity.Name = librarianModel.Name;
                librarianEntity.MobileNo = librarianModel.MobileNo;
                librarianEntity.EmailId = librarianModel.EmailId;
                librarianEntity.Address = librarianModel.Address;
                librarianEntity.Password = librarianModel.Password;


                librarianEntity.Id = Guid.NewGuid().ToString();
                librarianEntity.UId = librarianEntity.Id;
                librarianEntity.DocumentType = "librarian";

                librarianEntity.CreatedOn = DateTime.Now;
                librarianEntity.CreatedByName = "Kumar";
                librarianEntity.CreatedBy = "Kumar's UId";

                librarianEntity.UpdatedOn = DateTime.Now;
                librarianEntity.UpdatedByName = "Kumar";
                librarianEntity.UpdatedBy = "Kumar's UId";

                librarianEntity.Version = 1;
                librarianEntity.Active = true;
                librarianEntity.Archieved = false;

                Librarian response = await _container.CreateItemAsync(librarianEntity);

                librarianModel.Name = response.Name;

                return Ok("SignUp Successfull!!! ");

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

                Librarian librarian = _container.GetItemLinqQueryable<Librarian>(true).Where(q => q.DocumentType == "librarian" && q.EmailId == emailId && q.Password == password).AsEnumerable().FirstOrDefault();
                if (librarian != null)
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
