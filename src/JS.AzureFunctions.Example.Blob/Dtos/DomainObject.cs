using System;
using System.Text.Json.Serialization;

namespace JS.AzureFunctions.Example.Blob.Dtos
{
    public class Actor
    {
        public int Id { get; set; }
        public string Login { get; set; }
        [JsonPropertyName("gravatar_id")]
        public string GravatarId { get; set; }
        public string Url { get; set; }
        [JsonPropertyName("avatar_url")]
        public string AvatarUrl { get; set; }
    }

    public class Payload
    {
        public string Ref { get; set; }
        [JsonPropertyName("ref_type")]
        public string RefType { get; set; }
        [JsonPropertyName("master_branch")]
        public string MasterBranch { get; set; }
        public string Description { get; set; }
        [JsonPropertyName("pusher_type")]
        public string PusherType { get; set; }
    }

    public class Repo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
    }

    public class DomainObject
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public Actor Actor { get; set; }
        public Repo Repo { get; set; }
        public Payload Payload { get; set; }
        public bool Public { get; set; }
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }
    }
}
