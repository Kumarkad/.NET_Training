using VisitorSecurityClearanceSystemAPI.Entities;

namespace VisitorSecurityClearanceSystemAPI.CosmosDBServices
{
    public interface ICosmosService
    {
        Task<Manager> SignUpManager(Manager manager);
        Task<Manager> LoginManager(string username, string password);

        Task<OfficeUser> AddOfficeUser(OfficeUser user);
        Task<OfficeUser> LoginOfficeUser(string username, string password);
        Task<OfficeUser> GetOfficeUserbyUId(string UId);
        Task<OfficeUser> DeleteOfficeUser(OfficeUser user);

        Task<SecurityUser> AddSecurityUser(SecurityUser user);
        Task<SecurityUser> LoginSecurityUser(string username, string password);
        Task<SecurityUser> GetSecurityUserbyUId(string UId);
        Task<SecurityUser> DeleteSecurityUser(SecurityUser user);

        Task<Visitor> AddVisitor(Visitor visitor);
        Task<Visitor> GetVisitorByVisitorId(string visitorId);
        Task<Visitor> GetVisitorByMobileNo(string MobileNo);
        Task<List<Visitor>> GetAllVisitor();
        Task<List<Visitor>> GetAllPendingVisitor();
        Task<List<Visitor>> GetAllApprovedVisitor();
        Task<List<Visitor>> GetAllRejectedVisitor();
        Task<Visitor> UpdateVisitor(Visitor visitor);

    }
}
