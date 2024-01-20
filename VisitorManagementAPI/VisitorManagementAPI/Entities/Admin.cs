using Newtonsoft.Json;

namespace VisitorSecurityClearanceSystemAPI.Entities
{
    public class Manager : BaseClass
    {
        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "emailId", NullValueHandling = NullValueHandling.Ignore)]
        public string EmailId { get; set; }

        [JsonProperty(PropertyName = "mobileNo", NullValueHandling = NullValueHandling.Ignore)]
        public string MobileNo { get; set; }

        [JsonProperty(PropertyName = "userName", NullValueHandling = NullValueHandling.Ignore)]
        public string UserName { get; set; }

        [JsonProperty(PropertyName = "password", NullValueHandling = NullValueHandling.Ignore)]
        public string Password { get; set; }

    }

    public class Officer : BaseClass
    {
        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "emailId", NullValueHandling = NullValueHandling.Ignore)]
        public string EmailId { get; set; }

        [JsonProperty(PropertyName = "mobileNo", NullValueHandling = NullValueHandling.Ignore)]
        public string MobileNo { get; set; }

        [JsonProperty(PropertyName = "userName", NullValueHandling = NullValueHandling.Ignore)]
        public string UserName { get; set; }

        [JsonProperty(PropertyName = "password", NullValueHandling = NullValueHandling.Ignore)]
        public string Password { get; set; }

        [JsonProperty(PropertyName = "role", NullValueHandling = NullValueHandling.Ignore)]
        public string Role { get; set; }

    }

    public class Security : BaseClass
    {
        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "emailId", NullValueHandling = NullValueHandling.Ignore)]
        public string EmailId { get; set; }

        [JsonProperty(PropertyName = "mobileNo", NullValueHandling = NullValueHandling.Ignore)]
        public string MobileNo { get; set; }

        [JsonProperty(PropertyName = "userName", NullValueHandling = NullValueHandling.Ignore)]
        public string UserName { get; set; }

        [JsonProperty(PropertyName = "password", NullValueHandling = NullValueHandling.Ignore)]
        public string Password { get; set; }

        [JsonProperty(PropertyName = "role", NullValueHandling = NullValueHandling.Ignore)]
        public string Role { get; set; }

    }
}
