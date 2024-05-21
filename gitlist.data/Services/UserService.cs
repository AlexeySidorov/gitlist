using gitlist.data.Repositories;
using gitlist.domain;

namespace gitlist.data.Services;

public interface IUserService
{
    Task<UserModel?> GetUserInfo(string? name);
    Task<string?> GetCurrentUserAccount();
}

public class UserService : IUserService
{
    private readonly IRestApiService _restApiService;
    private readonly IUserRepository _userRepository;

    public UserService(IRestApiService restApiService, IUserRepository userRepository)
    {
        _restApiService = restApiService;
        _userRepository = userRepository;
    }

    public async Task<string?> GetCurrentUserAccount()
    {
        return await _userRepository.GetCurrentUser();
    }

    public async Task<UserModel?> GetUserInfo(string? name)
    {
        var localUserName = name;
        if (string.IsNullOrEmpty(localUserName) || string.IsNullOrWhiteSpace(localUserName))
            return await _userRepository.GetCurrentUserAccount();

        var localUser = await _userRepository.GetUserInfo(localUserName);
        if (localUser == null)
        {
            var remouteUser = await _restApiService.Request().GetUserByName(localUserName);
            if (remouteUser == null) return null;

            var userEntity = remouteUser.UserRequestToUserEntity();
            if (userEntity != null)
                await _userRepository.AddNewUser(userEntity);

            return userEntity?.EntityToUserModel();

        }

        return localUser;
    }
}