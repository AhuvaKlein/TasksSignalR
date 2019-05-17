using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TasksSignalR.Data
{
    public class Chore
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public User User { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public Status Status { get; set; }

    }


}
