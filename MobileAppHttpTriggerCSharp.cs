using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using MobileApp;

namespace MobileApp
{
    public static class MobileAppHttpTriggerCSharp
    {
        [FunctionName("MobileAppHttpTriggerCSharp")]
        public static IActionResult Run (
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            try
            {
                string connectionString = Environment.GetEnvironmentVariable("People");
                log.LogInformation(connectionString);
                var db = new DatabaseContext(connectionString);
                var people = db.GetPeople();
                return new JsonResult(people);
            }
            catch (Exception ex)
            {
                log.LogError(ex, ex.Message);
                return new JsonResult(ex);
            }
        }
    }
}
