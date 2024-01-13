using Newtonsoft.Json;

namespace Library_magmt.DTO
{
    public class UserModel
    {
        [JsonProperty(PropertyName = "Name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "mobileNo", NullValueHandling = NullValueHandling.Ignore)]
        public double MobileNo { get; set; }

        [JsonProperty(PropertyName = "emailId")]
        public string EmailId { get; set; }

        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }

        [JsonProperty(PropertyName = "address", NullValueHandling = NullValueHandling.Ignore)]
        public string Address { get; set; }
    }

    public class LibrarianModel : UserModel
    {
        [JsonProperty(PropertyName = "instituteId")]
        public string InstituteId { get; set; }
    }

    public class StudentModel : UserModel
    {
        [JsonProperty(PropertyName = "uId", NullValueHandling = NullValueHandling.Ignore)]
        public string UId { get; set; }

        [JsonProperty(PropertyName = "prn")]
        public string PRN { get; set; }

        [JsonProperty(PropertyName = "branch", NullValueHandling = NullValueHandling.Ignore)]
        public string Branch { get; set; }

        [JsonProperty(PropertyName = "year", NullValueHandling = NullValueHandling.Ignore)]
        public string Year { get; set; }


    }
}
