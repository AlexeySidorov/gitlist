namespace gitlistmobile;

using gitlist.core;
using Refit;

public interface IRestApiService
{
    IRestMetodsRequest Request();
}

public class RestApiService : IRestApiService
{
    public IRestMetodsRequest Request()
    {
        var httpClient = new HttpClient(new HttpLoggingHandler())
        {
            BaseAddress = new Uri(Constaint.Current.BaseApiUrl),
            Timeout = new TimeSpan(0, 0, 10)
        };

        return RestService.For<IRestMetodsRequest>(httpClient);
    }
}