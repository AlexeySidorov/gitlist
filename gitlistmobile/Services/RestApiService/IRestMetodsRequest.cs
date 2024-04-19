using Refit;

namespace gitlistmobile;

public interface IRestMetodsRequest {
    [Get("/users/{username}")]
    Task<UserRequest> GetUserByName([AliasAs("username")] string name);

    [Get("/users/{username}/repos")]
    Task<IList<UserRequest>> GetPublicRepositoriesByUserName([AliasAs("username")] string username);
}