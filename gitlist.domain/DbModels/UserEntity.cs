using gitlist.core;
using SQLite;

namespace gitlist.domain;

public class UserEntity: IEntity
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string? Account { get; set; }
    public string? FullUserName { get; set; }
    public string? AvatarUrl { get; set; }
    public int RepositoriesCount { get; set; }
    public string? Type { get; set; }

    public UserEntity()
    {
        
    }

    public UserEntity(string? account, string? fullUserName, string? avatarUrl, int repositoriesCount,
                     string? type)
    {
        Id = 0;
        Account = account;
        FullUserName = fullUserName;
        AvatarUrl = avatarUrl;
        RepositoriesCount = repositoriesCount;
        Type = type;
    }
}

public static class UserEnityExtension {
    public static UserModel? EntityToUserModel(this UserEntity entity) {
        if(entity == null) return null;

        return new UserModel(entity.Id, entity.FullUserName, entity.AvatarUrl);
    }
}