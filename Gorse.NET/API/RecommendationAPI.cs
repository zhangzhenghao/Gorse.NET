using Gorse.NET.Models;
using RestSharp;

namespace Gorse.NET;

public partial class Gorse
{
    /// <summary>
    /// Get recommendation with scores for a user.
    /// Uses X-API-Version: 2 header to return scores.
    /// </summary>
    public List<UserScore>? GetRecommend(string userId)
    {
        return _client.RequestWithHeaders<List<UserScore>, Object>(Method.Get, "api/recommend/" + userId, null, 
            new Dictionary<string, string> { { "X-API-Version", "2" } });
    }

    /// <summary>
    /// Get recommendation with scores for a user asynchronously.
    /// Uses X-API-Version: 2 header to return scores.
    /// </summary>
    public Task<List<UserScore>?> GetRecommendAsync(string userId)
    {
        return _client.RequestWithHeadersAsync<List<UserScore>, Object>(Method.Get, "api/recommend/" + userId, null,
            new Dictionary<string, string> { { "X-API-Version", "2" } });
    }

    public List<UserScore> GetUserNeighbors(string userId, int n = 100, int offset = 0)
    {
        return _client.Request<List<UserScore>, object>(Method.Get, $"api/user/{userId}/neighbors?n={n}&offset={offset}", null)!;
    }

    public Task<List<UserScore>> GetUserNeighborsAsync(string userId, int n = 100, int offset = 0)
    {
        return _client.RequestAsync<List<UserScore>, object>(Method.Get, $"api/user/{userId}/neighbors?n={n}&offset={offset}", null)!;
    }
}
