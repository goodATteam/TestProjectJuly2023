using TestProjectSfApiClient;
using TestProjectSfApiClient.Constants;
using TestProjectSfApiClient.Factories;
using TestProjectSfApiLib.Enums;
using TestProjectSfApiLib.Models;

namespace TestProjectSfApiConsoleApp
{
    internal class Program
    {
        // Task1. Cover UpdateSfSystemDataLoadLog method with unit tests (if needed move to a separate class)
        // Task2. If needed close gaps related to clean code and SOLID principles
        // Note: Do not over complicate

        static async Task Main(string[] args)
        {
            await new Program().UpdateSfSystemDataLoadLog(3, 12.33m);
        }

        public async Task UpdateSfSystemDataLoadLog(int quantity, decimal amount)
        {
            var sfRestClient = new SfRestClient<SfApiSystemDataLoadLog>(new ClientFactory());

            var queryFilter = $"FIN_Process__c='{DataGroup.Payment}'+and+" +
                              $"FIN_ProcessDate__c={new DateOnly(2022, 03, 22)}";

            var dataLoadLog =
                await sfRestClient.GetByFilterAsync(queryFilter, SalesforceTables.SalesforceTableName, new CancellationToken());

            var sfApiSystemDataLoadLog = dataLoadLog.FirstOrDefault();
            sfApiSystemDataLoadLog.ActualRawTotalAmount += amount;
            sfApiSystemDataLoadLog.ActualRawTotalRecordCount += quantity;

            sfRestClient.UpdateAsync(sfApiSystemDataLoadLog, SalesforceTables.SalesforceTableName, new CancellationToken());

            Console.WriteLine("Hello, World!");
        }
    }
}