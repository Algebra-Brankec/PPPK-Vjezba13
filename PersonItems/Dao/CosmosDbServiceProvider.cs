using Microsoft.Azure.Cosmos;
using System.Threading.Tasks;

namespace PersonItems.Dao
{
    public static class CosmosDbServiceProvider
    {
        private const string DatabaseName = "Persons";
        private const string ContainerName = "Persons";
        private const string Account = "https://branimirpppkcosmo.documents.azure.com:443/";
        private const string Key = "HFkKIcimXk5w8qLnLGb3IzkqJ6q7qswCMiO9pYgCkUUdC2Co8uXpDPcHFUBnE3S8PekmjcVj76QYnpKli2mxRQ==";
        private static ICosmosDbService cosmosDbService;

        public static ICosmosDbService CosmosDbService { get => cosmosDbService; }

        public async static Task Init()
        {
            CosmosClient client = new CosmosClient(Account, Key);
            cosmosDbService = new CosmosDbService(client, DatabaseName, ContainerName);
            DatabaseResponse database = await client.CreateDatabaseIfNotExistsAsync(DatabaseName);
            await database.Database.CreateContainerIfNotExistsAsync(ContainerName, "/id");
        }
    }
}