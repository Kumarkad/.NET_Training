using Microsoft.Azure.Cosmos;
using VisitorSecurityClearanceSystemAPI.Entities;

namespace VisitorSecurityClearanceSystemAPI.CosmosDBServer
{
    public class CosmosService : ICosmosService
    {
        public Container _container;
        public CosmosService() 
        {
            _container = GetContainer();
        }
        public async Task<Manager> LoginManager(string username, string password)
        {
            var manager = _container.GetItemLinqQueryable<Manager>(true).Where(q => q.DocumentType == "manager" && q.Active == true && q.Archieved == false && q.UserName == username && q.Password == password).AsEnumerable().FirstOrDefault();
            return manager;
        }

        public async Task<Manager> SignUpManager(Manager manager)
        {
            return await _container.CreateItemAsync<Manager>(manager);
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
