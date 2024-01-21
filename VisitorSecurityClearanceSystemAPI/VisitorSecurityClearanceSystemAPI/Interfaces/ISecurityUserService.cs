using VisitorSecurityClearanceSystemAPI.Entities;

namespace VisitorSecurityClearanceSystemAPI.Interfaces
{
    public interface ISecurityUserService
    {
        Task<SecurityUser> AddSecurityUser(SecurityUser user);
        Task<SecurityUser> LoginSecurityUser(string username, string password);
        Task<SecurityUser> GetSecurityUserbyUId(string UId);
        Task<SecurityUser> DeleteUser(SecurityUser user);
    }
}
