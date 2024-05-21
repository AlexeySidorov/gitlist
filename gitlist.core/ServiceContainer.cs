
using Microsoft.Extensions.DependencyInjection;

namespace gitlist.core;

public class ServiceContainer
{
    private static readonly Lazy<ServiceContainer> Instance = new Lazy<ServiceContainer>(() => new ServiceContainer());
    public static ServiceContainer Current => Instance.Value;

    private IServiceProvider _serviceProvider { get; set; }

    public void Initialize(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public T? GetService<T>() => _serviceProvider.GetService<T>();

    public string BaseApiUrl { get; set; }

    public string DbName { get; set; }

    public string DbPath { get; set; }

    public string CurrentUser { get; set; }
}