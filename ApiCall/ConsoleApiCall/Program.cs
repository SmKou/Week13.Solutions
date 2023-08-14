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
        List<Article> articleList = JsonConvert.DeserializeObject<List<Article>>(jsonResponse["results"].ToString());

        foreach (Article article in articleList)
        {
            Console.WriteLine($"Section: {article.Section}");
            Console.WriteLine($"Title: {article.Title}");
            Console.WriteLine($"Abstract: {article.Abstract}");
            Console.WriteLine($"Url: {article.Url}");
            Console.WriteLine($"Byline: {article.Byline}");
            Console.WriteLine($"Item_Type: {article.Item_Type}");
            if (article.Multimedia != null)
            {
                foreach (Multimedia multimedia in article.Multimedia)
                {
                    Console.WriteLine("-----------");
                    Console.WriteLine($"MULTIMEDIA");
                    Console.WriteLine($"Type: {media.Type}");
                    Console.WriteLine($"SubType: {media.SubType}");
                    Console.WriteLine($"Caption: {media.Caption}");
                }
            }
            Console.WriteLine("-------------------------");
        }
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