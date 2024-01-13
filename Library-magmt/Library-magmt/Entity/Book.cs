using Newtonsoft.Json;

namespace Library_magmt.Entity
{
    public class Book : BaseClass
    {
        [JsonProperty(PropertyName = "uId", NullValueHandling = NullValueHandling.Ignore)]
        public string UId { get; set; }

        [JsonProperty(PropertyName = "bookId", NullValueHandling = NullValueHandling.Ignore)]
        public string BookId { get; set; }

        [JsonProperty(PropertyName = "bookName", NullValueHandling = NullValueHandling.Ignore)]
        public string BookName { get; set; }

        [JsonProperty(PropertyName = "bookAuthor", NullValueHandling = NullValueHandling.Ignore)]
        public string BookAuthor { get; set; }

        [JsonProperty(PropertyName = "bookDomain", NullValueHandling = NullValueHandling.Ignore)]
        public string BookDomain { get; set; }

    }

    public class BookIssue : BaseClass
    {
        [JsonProperty(PropertyName = "bookId", NullValueHandling = NullValueHandling.Ignore)]
        public string BookId { get; set; }

        [JsonProperty(PropertyName = "prn")]
        public string PRN { get; set; }

        [JsonProperty(PropertyName = "issueBook", NullValueHandling = NullValueHandling.Ignore)]
        public bool IssueBook { get; set; }

        [JsonProperty(PropertyName = "returnBook", NullValueHandling = NullValueHandling.Ignore)]
        public bool ReturnBook { get; set; }

        [JsonProperty(PropertyName = "issueDate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime IssueDate { get; set; }

        [JsonProperty(PropertyName = "returnDate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime ReturnDate { get; set; }

    }
}
