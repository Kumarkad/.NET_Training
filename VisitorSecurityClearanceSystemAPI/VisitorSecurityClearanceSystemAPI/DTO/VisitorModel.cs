namespace VisitorSecurityClearanceSystemAPI.DTO
{
    public class VisitorModel
    {
        public string VisitorId { get; set; }
        public string Name { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public string Purpose { get; set; }
        public DateTime EntryTime {  get; set; }
        public DateTime ExitTime {  get; set; }
    }

    public class RequestModel:VisitorModel
    {
        public string ApprovedStatus { get; set; }
    }
}
