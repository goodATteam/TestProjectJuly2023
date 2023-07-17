using System.Reflection;
using System.Text.Json.Serialization;
using System.Text.Json;
using RestSharp;
using TestProjectSfApiClient.Factories;
using TestProjectSfApiLib.Models;

namespace TestProjectSfApiClient
{
    public class SfRestClient<T> where T : SfApiEntity
    {
        private readonly RestClient _sfRestClient;

        public SfRestClient(IClientFactory sfRestClientFactory) => _sfRestClient = sfRestClientFactory.GetRestClient();

        public async Task<IEnumerable<T>?> GetByFilterAsync(string queryFilter, string tableName, CancellationToken cancellationToken)
        {
            var soqlQuery = $"{GetBaseSoqlStatement<T>(tableName)}+WHERE+{queryFilter}";
            var queryResult = await _sfRestClient.GetJsonAsync<QueryResponse<T>>(
                $"/query?q={soqlQuery}",
                cancellationToken);

            return queryResult?.Records;
        }

        public async Task UpdateAsync(T entity, string tableName, CancellationToken cancellationToken)
        {
            if (entity.Id is null)
            {
                throw new ArgumentException($"{nameof(entity.Id)} can not be null");
            }

            var id = entity.Id;
            entity.Id = null;
            var request = new RestRequest($"/sobjects/{tableName}/{id}", Method.Patch)
                .AddJsonBody(JsonSerializer.Serialize(entity, SfJsonSerializerOptions.Default), false);
            entity.Id = id;

            var result = await _sfRestClient.ExecuteAsync(request, cancellationToken);

            if (!result.IsSuccessStatusCode)
            {
                throw new Exception($"Salesforce returned: {result.StatusCode} for update operation, id: {entity.Id}.");
            }
        }

        protected string GetBaseSoqlStatement<TEnt>(string tableName)
        {
            var fieldNames = string.Join(',',
                typeof(TEnt).GetProperties()
                    .Select(prop => prop.GetCustomAttribute<JsonPropertyNameAttribute>()?.Name ?? prop.Name));
            return $"SELECT+{fieldNames}+FROM+{tableName}";
        }
    }
}