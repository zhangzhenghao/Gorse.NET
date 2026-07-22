using Gorse.NET.Models;
using RestSharp;

namespace Gorse.NET;

public partial class Gorse
{
    /// <summary>
    /// Get recommendation with scores for a user.
    /// Uses X-API-Version: 2 header to return scores.
    /// </summary>
    public List<UserScore>? GetRecommend(string userId, IEnumerable<string>? categories = null,
        string? writeBackType = null, string? writeBackDelay = null, int? n = null, int? offset = null)
    {
        return _client.RequestWithHeaders<List<UserScore>, Object>(Method.Get,
            GetRecommendResource(userId, categories, writeBackType, writeBackDelay, n, offset), null,
            new Dictionary<string, string> { { "X-API-Version", "2" } });
    }

    /// <summary>
    /// Get recommendation with scores for a user asynchronously.
    /// Uses X-API-Version: 2 header to return scores.
    /// </summary>
    public Task<List<UserScore>?> GetRecommendAsync(string userId, IEnumerable<string>? categories = null,
        string? writeBackType = null, string? writeBackDelay = null, int? n = null, int? offset = null)
    {
        return _client.RequestWithHeadersAsync<List<UserScore>, Object>(Method.Get,
            GetRecommendResource(userId, categories, writeBackType, writeBackDelay, n, offset), null,
            new Dictionary<string, string> { { "X-API-Version", "2" } });
    }

    private static string GetRecommendResource(string userId, IEnumerable<string>? categories,
        string? writeBackType, string? writeBackDelay, int? n, int? offset)
    {
        var query = new List<string>();
        if (categories != null)
        {
            query.AddRange(categories
                .Where(category => !string.IsNullOrEmpty(category))
                .Select(category => "category=" + Uri.EscapeDataString(category)));
        }
        if (!string.IsNullOrEmpty(writeBackType))
        {
            query.Add("write-back-type=" + Uri.EscapeDataString(writeBackType));
        }
        if (!string.IsNullOrEmpty(writeBackDelay))
        {
            query.Add("write-back-delay=" + Uri.EscapeDataString(writeBackDelay));
        }
        if (n.HasValue)
        {
            query.Add("n=" + n.Value);
        }
        if (offset.HasValue)
        {
            query.Add("offset=" + offset.Value);
        }

        var resource = "api/recommend/" + Uri.EscapeDataString(userId);
        return query.Count > 0 ? resource + "?" + string.Join("&", query) : resource;
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
