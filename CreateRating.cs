using System;
using System.IO;
using Microsoft.Azure.Cosmos;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Openhack.Functions
{
    public static class CreateRating
    {
        [FunctionName("CreateRating")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req, ILogger log) 
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            
            var ratingItem = RatingItem.generate(data);
            var client = Utils.generateCosmosClient();;
            var container = await Utils.getRatingContainer(client);
            var createResponse = await container.CreateItemAsync(ratingItem);
            return new OkObjectResult(createResponse.Resource);
        }
    }
}
