using RestSharp;

namespace TestProjectSfApiClient.Factories;

public interface IClientFactory
{
    RestClient GetRestClient();
}
