using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ConsoleApiCall.Keys;

namespace ConsoleApiCall;

public class Program
{
    public static void Main()
    {
        Task<string> apiCallTask = ApiHelper.ApiCall(EnvironmentVariables.ApiKey);
        string result = apiCallTask.Result;
        JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(result);
        Console.WriteLine(jsonResponse["results"]);
    }
}

public class ApiHelper
{
    public static async Task<string> ApiCall(string apiKey)
    {
        RestClient client = new RestClient("https://api.nytimes.com/svc/topstories/v2");
        RestRequest request = new RestRequest($"home.json?api-key={apiKey}", Method.Get);
        RestResponse response = await client.ExecuteAsync(request);
        return response.Content;
    }
}