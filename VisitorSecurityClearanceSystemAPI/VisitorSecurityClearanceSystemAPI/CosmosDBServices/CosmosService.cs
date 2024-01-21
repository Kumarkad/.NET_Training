﻿using Microsoft.Azure.Cosmos;
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
