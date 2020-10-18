using System.Data.Common;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
namespace Openhack.Functions
{
    public static class GetRating
    {   
        [FunctionName("GetRating")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            string ratingId = req.Query["ratingId"];
            log.LogDebug("\ratingId: " + ratingId);
            var sqlQueryText = $"SELECT * FROM Ratings WHERE Ratings.id = '{ratingId}'";
            var ratings = await Utils.getRatingsByQuery(sqlQueryText, log);
            if (!Utils.IsAny(ratings)) return new NotFoundObjectResult(new {errorMessage = "No rating found for given rating id!", ratingId});
            return new OkObjectResult(ratings[0]);
            
        }
    }
}