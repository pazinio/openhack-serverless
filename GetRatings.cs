using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.Cosmos;
using System.Collections.Generic;

namespace Openhack.Functions
{
    public static class GetRatings
    {
        [FunctionName("GetRatings")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            string UserId = req.Query["UserId"];
            log.LogDebug("UserId: " + UserId);

            var sqlQueryText = $"SELECT * FROM Ratings WHERE Ratings.UserId = '{UserId}'";
            var ratings = await Utils.getRatingsByQuery(sqlQueryText, log);
    
            if (!Utils.IsAny(ratings)) return new NotFoundObjectResult(new {errorMessage = "No ratings found for given user id!", UserId});
            return new OkObjectResult(ratings);
        }
    }
}
