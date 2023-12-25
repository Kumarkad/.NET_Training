using Newtonsoft.Json;

namespace TaskManagerAPI.DTO
{
    public class TaskModel
    {
        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "taskDesc", NullValueHandling = NullValueHandling.Ignore)]
        public string TaskDesc { get; set; }
    }
}
