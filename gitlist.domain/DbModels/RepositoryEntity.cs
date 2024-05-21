using gitlist.core;
using gitlist.domain.Models;
using SQLite;

namespace gitlist.domain.DbModels
{
    public class RepositoryEntity : IEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string DefaultBranch { get; set; }
        public DateTime Create { get; set; }
        public DateTime Update { get; set; }
        public string BaseUrl { get; set; }
        public string CloneUrl { get; set; }
        public string ContentUrl { get; set; }
        public int UserId { get; set; }

        public RepositoryEntity()
        {
            
        }

        public RepositoryEntity(string name, string fullName, string branch, DateTime create, DateTime update, string baseUrl, string cloneUrl, string contentUrl, int userId)
        {
            Name = name; 
            FullName = fullName;
            DefaultBranch = branch;  
            Create = create; 
            Update = update;
            BaseUrl = baseUrl;
            CloneUrl = cloneUrl;
            ContentUrl = contentUrl;
            UserId = userId;
        }
    }

    public static class RepositoryEntityExtension
    {
        public static RepositoryModel? EntityToRepositoryModel(this RepositoryEntity entity)
        {
            if (entity == null) return null;

            return new RepositoryModel(entity.Name);
        }

        public static RepositoryDetailsModel? EntityToRepositoryDetailsModel(this RepositoryEntity entity)
        {
            if (entity == null) return null;

            return new RepositoryDetailsModel(entity.Name, entity.FullName, entity.DefaultBranch, entity.Create,
                entity.Update, entity.BaseUrl, entity.CloneUrl, entity.ContentUrl);
        }
    }
}
