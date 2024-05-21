using gitlist.data.Requests;
using Refit;

namespace gitlist.data;

public interface IRestMetodsRequest {
    [Get("/users/{username}")]
    Task<UserRequest> GetUserByName([AliasAs("username")] string name);

    //a.sidorov. Доработать пагинацию
    [Get("/users/{username}/repos")]
    Task<IList<RepositoryRequest>> GetPublicRepositoriesByUserName([AliasAs("username")] string username);
}