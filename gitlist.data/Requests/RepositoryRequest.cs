using gitlist.domain;
using gitlist.domain.DbModels;
using Newtonsoft.Json;

namespace gitlist.data.Requests
{
    public class RepositoryRequest
    {
        public string Name { get; set; }
        public string FullName { get; set; }
        [JsonProperty("default_branch")]
        public string DefaultBranch { get; set; }

        [JsonProperty("created_at")]
        public DateTime Create { get; set; }

        [JsonProperty("updated_at")]
        public DateTime Update { get; set; }

        [JsonProperty("html_url")]
        public string BaseUrl { get; set; }

        [JsonProperty("clone_url")]
        public string CloneUrl { get; set; }

        [JsonProperty("contents_url")]
        public string ContentUrl { get; set; }
    }

    public static class RepositoryRequestExtens
    {
        public static RepositoryEntity? UserRequestToUserEntity(this RepositoryRequest entity, int userId)
        {
            if (entity == null) return null;

            return new RepositoryEntity(entity.Name, entity.FullName, entity.DefaultBranch, entity.Create, entity.Update, entity.BaseUrl, entity.CloneUrl, entity.ContentUrl, userId);
        }
    }
}
