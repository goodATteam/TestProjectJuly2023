using RestSharp;
using RestSharp.Serializers.Json;

namespace TestProjectSfApiClient.Factories;

public class ClientFactory : IClientFactory
{
   public RestClient GetRestClient()
    {
        var clientOptions = new RestClientOptions($"url");

        return new RestClient(clientOptions, configureSerialization: s => s.UseSystemTextJson(SfJsonSerializerOptions.Default));
    }
}
