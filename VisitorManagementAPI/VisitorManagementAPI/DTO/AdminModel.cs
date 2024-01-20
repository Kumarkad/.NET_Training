namespace VisitorSecurityClearanceSystemAPI.DTO
{
    public class ManagerModel
    {
        public string UId { get; set; }
        public string Name { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class OfficerModel
    {
        public string UId { get; set; }
        public string Name { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }

    public class SecurityModel
    {
        public string UId { get; set; }
        public string Name { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
