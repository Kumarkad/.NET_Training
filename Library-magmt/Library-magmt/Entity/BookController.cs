using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;

namespace Library_magmt.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        public string URI = "https://localhost:8081";
        public string PrimaryKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
        public string DatabaseName = "library-mgmt";
        public string ContainerName = "library";

        public readonly Container _container;
        public BookController()
        {
            _container = GetContainer();
        }
        private Container GetContainer()
        {
            CosmosClient cosmosclient = new CosmosClient(URI, PrimaryKey);
            Database database = cosmosclient.GetDatabase(DatabaseName);
            Container container = database.GetContainer(ContainerName);
            return container;
        }
    }



    /*
     feature :

   1-  user : [ I can be able to login and singup ] 
          steps : 1 students must able to singup ( add student )
          steps : 2 Stundets must be able to log in ( request : username and pass .... ) return (uid):

    need : student (dtype)   - CRUD
           librarian      - CRUD  
       
    Deadline : Tomarrow (3 Jan ) 


    2 - Book  
Features : 

    1- Add Book , Delete , Update , Issue book , return book , Request (xyz) 
    2 -  search by book-name , author , subject (get)


    librarian : 
    show books in library 
    show borrowed books 
    show total books 
     
    Need : Book CRUD
     
     */
}
