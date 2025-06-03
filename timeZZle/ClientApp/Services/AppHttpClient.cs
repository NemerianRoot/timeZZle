using System.Linq.Expressions;
using System.Reflection;

namespace timeZZle.ClientApp.Services;

public sealed class AppHttpClient(IHttpClientFactory clientFactory)
{
    public const string HttpClientName = "ApiClient";
    
    // public async Task<ApiResponse> PostAsync<TBody>(string relativeUri, TBody? body = default)
    // {
    //     var httpClient = clientFactory.CreateClient(HttpClientName);
    //     return await httpClient.PostAsJsonAsync(relativeUri, body);
    // }
    //
    // public async Task<ApiResponse> PutAsync<TBody>(string relativeUri, TBody? body = default)
    // {
    //     var httpClient = clientFactory.CreateClient(HttpClientName);
    //     return await httpClient.PutAsJsonAsync(relativeUri, body);
    // }
    //
    // public async Task<ApiResponse> GetAsync(string relativeUri)
    // {
    //     var httpClient = clientFactory.CreateClient(HttpClientName);
    //     return await httpClient.GetAsync(relativeUri);
    // }
    //
    // public async Task<ApiResponse> DeleteAsync(string relativeUri)
    // {
    //     var httpClient = clientFactory.CreateClient(HttpClientName);
    //     return await httpClient.DeleteAsync(relativeUri);
    // }

    
    public async Task<HttpResponseMessage> PostAsync<TBody>(string relativeUri, TBody? body = default)
    {
        var httpClient = clientFactory.CreateClient(HttpClientName);
        return await httpClient.PostAsJsonAsync(relativeUri, body);
    }
    
    public async Task<HttpResponseMessage> GetAsync(string relativeUri)
    {
        var httpClient = clientFactory.CreateClient(HttpClientName);
        return await httpClient.GetAsync(relativeUri);
    }
    
    private static Dictionary<string, Func<T, object>> GetPropertyGetters<T>()
    {
        var dictionary = new Dictionary<string, Func<T, object>>();

        foreach (var property in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            var param = Expression.Parameter(typeof(T), "_");
            var propertyAccess = Expression.Property(param, property);
            var convert = Expression.Convert(propertyAccess, typeof(object));
            var lambda = Expression.Lambda<Func<T, object>>(convert, param).Compile();

            dictionary[property.Name] = lambda;
        }

        return dictionary;
    }
}