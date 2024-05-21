using gitlist.core;
using gitlist.domain.DbModels;
using gitlist.domain.Models;

namespace gitlist.data.Repositories;

public interface IPublicUserRepositories : ICrudServiceAsync<RepositoryEntity>
{
    Task AddOrUpdateRepositories(IList<RepositoryEntity> repositories);
    Task<IList<RepositoryModel>> GetRepositories(int skip, int count);
    Task<RepositoryDetailsModel?> GetRepositoryInfo(string reponame);
    Task DeleteAllData(int userId);
}

public class PublicUserRepositories : AsyncBaseDataService<RepositoryEntity>, IPublicUserRepositories
{
    private readonly IAsyncRepository<RepositoryEntity> _repository;

    public PublicUserRepositories(IAsyncRepository<RepositoryEntity> repository) : base(repository)
    {
        _repository = repository;
    }

    public async Task AddOrUpdateRepositories(IList<RepositoryEntity> repositories)
    {
        await _repository.CreateOrUpdateAllAsync(repositories);
    }

    public async Task DeleteAllData(int userId)
    {
        var resut = await _repository.FetchAsync(r => r.UserId == userId);
        if (resut.Count == 0) return;

        foreach (var repo in resut)
            await _repository.DeleteAsync(repo);
    }

    public async Task<IList<RepositoryModel>> GetRepositories(int skip, int count)
    {
        var resut = await _repository.FetchAsync(skip, count);
        if (resut.Count() == 0) return new List<RepositoryModel>();

        var list = new List<RepositoryModel>();
        foreach (var model in resut)
            list.Add(model.EntityToRepositoryModel());

        return list;
    }

    public async Task<RepositoryDetailsModel?> GetRepositoryInfo(string reponame)
    {
        var result = await _repository.GetAsync(r => r.Name.Equals(reponame));
        if (result == null) return null;

        return result.EntityToRepositoryDetailsModel();
    }
}