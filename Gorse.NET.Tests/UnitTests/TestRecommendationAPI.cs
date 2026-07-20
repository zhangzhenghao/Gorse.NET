using Gorse.NET.Models;

namespace Gorse.NET.Tests;

public partial class Tests
{
    [Test]
    public void TestRecommend()
    {
        client.InsertUser(new Models.User { UserId = "3000" });
        var recommendations = client.GetRecommend(
            "3000", ["Drama", "Comedy"], "recommend", "1h", 3, 0);
        Assert.That(recommendations, Is.Not.Null);
        Assert.That(recommendations, Has.Count.EqualTo(3));
        foreach (var recommendation in recommendations)
        {
            var item = client.GetItem(recommendation.Id);
            Assert.That(item.Categories, Has.Some.Matches<string>(category => category is "Drama" or "Comedy"));
        }
    }

    [Test]
    public async Task TestRecommendAsync()
    {
        await client.InsertUserAsync(new User { UserId = "4000" });
        var recommendations = await client.GetRecommendAsync(
            "4000", ["Drama", "Comedy"], "recommend", "1h", 3, 0);
        Assert.That(recommendations, Is.Not.Null);
        Assert.That(recommendations, Has.Count.EqualTo(3));
        foreach (var recommendation in recommendations)
        {
            var item = await client.GetItemAsync(recommendation.Id);
            Assert.That(item.Categories, Has.Some.Matches<string>(category => category is "Drama" or "Comedy"));
        }
    }
}
