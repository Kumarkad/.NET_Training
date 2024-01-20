using Newtonsoft.Json;

namespace VisitorSecurityClearanceSystemAPI.Entities
{
    public class Visitor : BaseClass
    {
        [JsonProperty(PropertyName = "visitorId", NullValueHandling = NullValueHandling.Ignore)]
        public string VisitorId { get; set; }

        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "emailId", NullValueHandling = NullValueHandling.Ignore)]
        public string EmailId { get; set; }

        [JsonProperty(PropertyName = "mobileNo", NullValueHandling = NullValueHandling.Ignore)]
        public string MobileNo { get; set; }

        [JsonProperty(PropertyName = "address", NullValueHandling = NullValueHandling.Ignore)]
        public string Address { get; set; }

        [JsonProperty(PropertyName = "purpose", NullValueHandling = NullValueHandling.Ignore)]
        public string Purpose { get; set; }

        [JsonProperty(PropertyName = "entryTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime EntryTime { get; set; }

        [JsonProperty(PropertyName = "exitTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime ExitTime { get; set; }

        [JsonProperty(PropertyName = "approvedStatus", NullValueHandling = NullValueHandling.Ignore)]
        public string ApprovedStatus { get; set; }
    }
}
