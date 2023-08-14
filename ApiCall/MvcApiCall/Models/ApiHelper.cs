using System.Threading.Tasks;
using RestSharp;

namespace MvcApiCall.Models;

public class ApiHelper
{
    public static async Task<string> ApiCall(string apiKey)
    {
        RestClient client = new RestClient("https://api.nytimes.com/svc/topstories/v2");
        RestRequest request = new RestRequest($"home.json?api-key={apiKey}", Method.GET);
        RestResponse response = await client.ExecuteAsync(request);
        return response.Content;
    }
}