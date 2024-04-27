using gitlist.domain;
using Newtonsoft.Json;

namespace gitlist.data;

public class UserRequest
{
	[JsonProperty("login")]
	public string? Account { get; set; }
	[JsonProperty("avatar_url")]
	public string? AvatarUrl { get; set; }
	public string? Type { get; set; }
	[JsonProperty("name")]
	public string? FullUserName { get; set; }
	[JsonProperty("public_repos")]
	public int RepositoriesCount { get; set; }
}

public static class UserRequestExtens
{
	public static UserEntity? UserRequestToUserEntity(this UserRequest entity)
	{
		if (entity == null) return null;

		return new UserEntity(entity.Account, entity.FullUserName, entity.AvatarUrl, 
				entity.RepositoriesCount, entity.Type );
	}
}