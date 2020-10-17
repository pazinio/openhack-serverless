using System;
using Microsoft.Azure.Cosmos;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Linq;

namespace Openhack.Functions
{
    public class Utils {
        static public CosmosClient generateCosmosClient() 
        {
            var cosmosURI = System.Environment.GetEnvironmentVariable("COSMOS_URI", EnvironmentVariableTarget.Process);                        
            var cosmosKey = System.Environment.GetEnvironmentVariable("COSMOS_KEY", EnvironmentVariableTarget.Process);
            return new CosmosClient(cosmosURI, cosmosKey);
        }

         public static async Task<List<RatingItem>> getRatingsByQuery(String sqlQueryText, ILogger log) 
         {
            var client = generateCosmosClient();
            var container = await getRatingContainer(client);
            var queryDefinition = new QueryDefinition(sqlQueryText);
            var queryResultSetIterator = container.GetItemQueryIterator<RatingItem>(queryDefinition);
            var ratings = new List<RatingItem>();

            while (queryResultSetIterator.HasMoreResults)
            {
                log.LogDebug("has result!");
                var currentResultSet = await queryResultSetIterator.ReadNextAsync();
                foreach (RatingItem rating in currentResultSet)
                {
                    ratings.Add(rating);
                    log.LogDebug("\tRead {0}\n", rating);
                }
            }

            return ratings;
        }

         public static async Task<Container> getRatingContainer(CosmosClient client) 
         {
            var database = await client.CreateDatabaseIfNotExistsAsync("12345678");
            return client.GetContainer("12345678", "Ratings");
        }

         public static bool IsAny<T>(IEnumerable<T> data) {
            return data != null && data.Any();
         }
    }
}