using System;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;

namespace JS.AzureFunctions.Example.Blob.Functions
{
    public static class ApiFunctions
    {
        [FunctionName(nameof(GetSasUriForBlobUpload))]
        public static IActionResult GetSasUriForBlobUpload(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "sas")] HttpRequest req,
            [Blob(Constants.BlobContainerName)] BlobContainerClient uploadContainerClient)
        {
            var blobName = Guid.NewGuid().ToString();
            var blob = uploadContainerClient.GetBlobClient(blobName);

            var uri = blob.GenerateSasUri(BlobSasPermissions.Create | BlobSasPermissions.Write, DateTimeOffset.UtcNow.AddDays(1));

            return new OkObjectResult(uri.ToString());
        }
    }
}
