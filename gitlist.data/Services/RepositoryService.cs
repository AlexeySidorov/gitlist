using gitlist.data.Repositories;
using gitlist.data.Requests;
using gitlist.domain.DbModels;
using gitlist.domain.Models;

namespace gitlist.data.Services;


public interface IPublicUserRepositoryService
{
    Task<IList<RepositoryModel>> GetRepositoryByPage(int userId, string username, int skip, int count);
    Task<RepositoryDetailsModel?> GetRepositoryDetailsByPage(string repoName);
}

public class PublicUserRepositoryService : IPublicUserRepositoryService
{
    private readonly IRestApiService _restApiService;
    private readonly IPublicUserRepositories _publicUserRepository;

    public PublicUserRepositoryService(IRestApiService restApiService, IPublicUserRepositories publicUserRepository)
    {
        _restApiService = restApiService;
        _publicUserRepository = publicUserRepository;
    }

    public async Task<IList<RepositoryModel>> GetRepositoryByPage(int userId, string username, int skip, int count)
    {
        //a.sidorov. Доделать с пагинацией
        var repoList = await _restApiService.Request().GetPublicRepositoriesByUserName(username);
        if (repoList != null)
        {
            var list = new List<RepositoryEntity>();
            foreach (var repo in repoList)
            {
                var data = repo.UserRequestToUserEntity(userId);
                if (data != null)
                    list.Add(data);
            }

            await _publicUserRepository.AddOrUpdateRepositories(list);
        }

        return await _publicUserRepository.GetRepositories(skip, count);
    }

    public async Task<RepositoryDetailsModel?> GetRepositoryDetailsByPage(string repoName)
    {
        return await _publicUserRepository.GetRepositoryInfo(repoName);
    }
}