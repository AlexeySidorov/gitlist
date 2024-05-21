
namespace gitlist.data.Repositories;

using gitlist.core;
using gitlist.domain;

public interface IUserRepository : ICrudServiceAsync<UserEntity>
{
    public Task<UserModel?> GetUserInfo(string name);
    public Task<string?> GetUserAccount(string name);
    public Task AddNewUser(UserEntity userEntity);
    public Task<string?> GetCurrentUser();
    public Task<UserModel?> GetCurrentUserAccount();
    public Task<int> DeleteCurrentUser();
}

public class UserRepository : AsyncBaseDataService<UserEntity>, IUserRepository
{
    private readonly IAsyncRepository<UserEntity> _repository;
    public UserRepository(IAsyncRepository<UserEntity> repository) : base(repository)
    {
        _repository = repository;
    }

    public async Task AddNewUser(UserEntity userEntity)
    {
        await _repository.CreateAsync(userEntity);
    }

    public async Task<string?> GetCurrentUser()
    {
        var users = await _repository.FetchAsync();
        if (!users.Any()) return null;

        return users.ToList()[0].Account;
    }

    public async Task<UserModel?> GetCurrentUserAccount()
    {
        var users = await _repository.FetchAsync();
        if (!users.Any()) return null;

        return users.ToList()[0].EntityToUserModel();
    }

    public async Task<string?> GetUserAccount(string name)
    {
        var users = await GetUserEntry(name);
        if (users == null) return null;

        return users[0].Account;
    }

    public async Task<UserModel?> GetUserInfo(string name)
    {
        var users = await GetUserEntry(name);
        if (users == null) return null;

        return users[0].EntityToUserModel();
    }

    private async Task<List<UserEntity>?> GetUserEntry(string name)
    {
        if (await _repository.CountAsync() == 0) return null;

        var users = await _repository.FetchAsync(user =>
            user.Account != null && user.Account.Equals(name));

        if (users == null || users.Count == 0) return null;

        return users.ToList();
    }

    public async Task<int> DeleteCurrentUser()
    {
        var users = await _repository.FetchAsync();
        if (!users.Any()) return -1;

        var currentUser = users.ToList()[0];

        await _repository.DeleteAsync(currentUser);

        return currentUser.Id;
    }
}