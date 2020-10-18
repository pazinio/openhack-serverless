using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Openhack.Functions
{
    public static class GetProduct
    {
        [FunctionName("GetProduct")]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            string productId = req.Query["product_id"];
            string responseMessage =   $"The product name for your product id {productId} is Starfruit Explosion";
            return new OkObjectResult(responseMessage);
        }
    }
}
