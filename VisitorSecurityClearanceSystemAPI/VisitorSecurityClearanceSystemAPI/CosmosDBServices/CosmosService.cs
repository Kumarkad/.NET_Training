using Microsoft.Azure.Cosmos;
using VisitorSecurityClearanceSystemAPI.Entities;

namespace VisitorSecurityClearanceSystemAPI.CosmosDBServices
{
    public class CosmosService : ICosmosService
    {
        public Container _container;
        public CosmosService()
        {
            _container = GetContainer();
        }

        public async Task<OfficeUser> AddOfficeUser(OfficeUser user)
        {
            return await _container.CreateItemAsync<OfficeUser>(user);
        }

        public async Task<SecurityUser> AddSecurityUser(SecurityUser user)
        {
            return await _container.CreateItemAsync<SecurityUser>(user);
        }

        public async Task<Visitor> AddVisitor(Visitor visitor)
        {
            return await _container.CreateItemAsync<Visitor>(visitor);
        }

        public async Task<OfficeUser> DeleteOfficeUser(OfficeUser user)
        {
            return await _container.ReplaceItemAsync(user, user.Id);
        }

        public async Task<SecurityUser> DeleteSecurityUser(SecurityUser user)
        {
            return await _container.ReplaceItemAsync(user, user.Id);
        }

        public async Task<List<Visitor>> GetAllApprovedVisitor()
        {
            var visitors = _container.GetItemLinqQueryable<Visitor>(true).Where(q => q.DocumentType == "visitor" && q.ApprovedStatus == "approved" &&  q.Archieved == false && q.Active == true).AsEnumerable().ToList();
            return visitors;
        }

        public async Task<List<Visitor>> GetAllPendingVisitor()
        {
            var visitors = _container.GetItemLinqQueryable<Visitor>(true).Where(q => q.DocumentType == "visitor" && q.ApprovedStatus == "pending" &&  q.Archieved == false && q.Active == true).AsEnumerable().ToList();
            return visitors;
        }

        public async Task<List<Visitor>> GetAllRejectedVisitor()
        {
            var visitors = _container.GetItemLinqQueryable<Visitor>(true).Where(q => q.DocumentType == "visitor" && q.ApprovedStatus == "rejected" &&  q.Archieved == false && q.Active == true).AsEnumerable().ToList();
            return visitors;
        }

        public async Task<List<Visitor>> GetAllVisitor()
        {
            var visitors = _container.GetItemLinqQueryable<Visitor>(true).Where(q => q.DocumentType == "visitor" && q.Archieved == false && q.Active == true).AsEnumerable().ToList();
            return visitors;
        }

        public async Task<OfficeUser> GetOfficeUserbyUId(string UId)
        {
            var user = _container.GetItemLinqQueryable<OfficeUser>(true).Where(q => q.DocumentType == "officeUser" && q.UId == UId && q.Archieved == false && q.Active == true).AsEnumerable().FirstOrDefault();
            return user;
        }

        public async Task<SecurityUser> GetSecurityUserbyUId(string UId)
        {
            var user = _container.GetItemLinqQueryable<SecurityUser>(true).Where(q => q.DocumentType == "securityUser" && q.UId == UId && q.Archieved == false && q.Active == true).AsEnumerable().FirstOrDefault();
            return user;
        }

        public async Task<Visitor> GetVisitorByMobileNo(string MobileNo)
        {
            var visitor = _container.GetItemLinqQueryable<Visitor>(true).Where(q => q.DocumentType == "visitor" && q.MobileNo == MobileNo && q.ApprovedStatus == "approved" && q.Archieved == false && q.Active == true).AsEnumerable().FirstOrDefault();
            return visitor;
        }

        public async Task<Visitor> GetVisitorByVisitorId(string visitorId)
        {
            var visitor = _container.GetItemLinqQueryable<Visitor>(true).Where(q => q.DocumentType == "visitor" && q.VisitorId == visitorId && q.Archieved == false && q.Active == true).AsEnumerable().FirstOrDefault();
            return visitor;
        }

        public async Task<Manager> LoginManager(string username, string password)
        {
            var manager = _container.GetItemLinqQueryable<Manager>(true).Where(q => q.DocumentType == "manager" && q.Active == true && q.Archieved == false && q.UserName == username && q.Password == password).AsEnumerable().FirstOrDefault();
            return manager;
        }

        public async Task<OfficeUser> LoginOfficeUser(string username, string password)
        {
            var user = _container.GetItemLinqQueryable<OfficeUser>(true).Where(q => q.DocumentType == "officeUser" && q.Active == true && q.Archieved == false && q.UserName == username && q.Password == password).AsEnumerable().FirstOrDefault();
            return user;
        }

        public async Task<SecurityUser> LoginSecurityUser(string username, string password)
        {
            var user = _container.GetItemLinqQueryable<SecurityUser>(true).Where(q => q.DocumentType == "securityUser" && q.Active == true && q.Archieved == false && q.UserName == username && q.Password == password).AsEnumerable().FirstOrDefault();
            return user;
        }

        public async Task<Manager> SignUpManager(Manager manager)
        {
            return await _container.CreateItemAsync<Manager>(manager);
        }

        public async Task<Visitor> UpdateVisitor(Visitor visitor)
        {
            return await _container.UpsertItemAsync<Visitor>(visitor);
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
