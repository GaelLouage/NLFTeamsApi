using Newtonsoft.Json;

namespace Infrastructuur.Model
{
    public class UserTeamEntity
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("userId")]
        public int UserId { get; set; }

        [JsonProperty("teamId")]
        public int TeamId { get; set; }
    }
}
