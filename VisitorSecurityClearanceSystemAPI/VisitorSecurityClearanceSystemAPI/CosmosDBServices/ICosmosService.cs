using VisitorSecurityClearanceSystemAPI.Entities;

namespace VisitorSecurityClearanceSystemAPI.CosmosDBServices
{
    public interface ICosmosService
    {
        Task<Manager> SignUpManager(Manager manager);
        Task<Manager> LoginManager(string username, string password);
        Task<OfficeUser> AddOfficeUser(OfficeUser user);
        Task<SecurityUser> AddSecurityUser(SecurityUser user);
        Task<SecurityUser> LoginSecurityUser(string username, string password);
        Task<OfficeUser> LoginOfficeUser(string username, string password);
    }
}
