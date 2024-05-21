
namespace gitlist.domain.Models
{
    public partial class RepositoryDetailsModel
    {
        public string Name { get; set; }
        public string FullName { get; set; }
        public string DefaultBranch { get; set; }

        public DateTime Create { get; set; }

        public DateTime Update { get; set; }

        public string BaseUrl { get; set; }

        public string CloneUrl { get; set; }

        public string ContentUrl { get; set; }

        public RepositoryDetailsModel(string name, string fullname, string branch, DateTime create, DateTime update,
            string baseUrl, string cloneUrl, string contentUrl)
        {
            Name = name;
            FullName = fullname;
            DefaultBranch = branch;
            Create = create;
            Update = update;
            BaseUrl = baseUrl;
            CloneUrl = cloneUrl;
            ContentUrl = contentUrl;
        }
    }
}