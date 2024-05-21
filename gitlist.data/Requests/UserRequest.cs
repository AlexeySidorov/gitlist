using gitlist.domain;
using Newtonsoft.Json;

namespace gitlist.data;

public class UserRequest
{
	public string Login { get; set; }
	public string Avatar_Url { get; set; }
	public string? Type { get; set; }
	public string Name { get; set; }
}

public static class UserRequestExtens
{
	public static UserEntity? UserRequestToUserEntity(this UserRequest entity)
	{
		if (entity == null) return null;

		return new UserEntity(entity.Login, entity.Name, entity.Avatar_Url, 
				0, entity.Type );
	}
}