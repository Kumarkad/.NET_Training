using Newtonsoft.Json;

namespace Library_magmt.Entity
{
    public class Users : BaseClass
    {
        [JsonProperty(PropertyName = "emailId")]
        public string EmailId { get; set; }

        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }

        [JsonProperty(PropertyName = "Name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "mobileNo", NullValueHandling = NullValueHandling.Ignore)]
        public double MobileNo { get; set; }

        [JsonProperty(PropertyName = "address", NullValueHandling = NullValueHandling.Ignore)]
        public string Address { get; set; }

    }

    public class Librarian : Users
    {
        [JsonProperty(PropertyName = "instituteId")]
        public string InstituteId { get; set; }

    }

    public class Student : Users
    {
        [JsonProperty(PropertyName = "prn")]
        public string PRN { get; set; }

        [JsonProperty(PropertyName = "branch", NullValueHandling = NullValueHandling.Ignore)]
        public string Branch { get; set; }

        [JsonProperty(PropertyName = "year", NullValueHandling = NullValueHandling.Ignore)]
        public string Year { get; set; }

    }
}
