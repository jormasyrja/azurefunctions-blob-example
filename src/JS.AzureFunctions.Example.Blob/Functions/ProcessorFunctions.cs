using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Azure.Storage.Blobs;
using Microsoft.Azure.WebJobs;
using System.Threading.Tasks;
using Azure.Data.Tables;
using JS.AzureFunctions.Example.Blob.Dtos;

namespace JS.AzureFunctions.Example.Blob.Functions
{
    public class ProcessorFunctions
    {
        [FunctionName(nameof(ProcessUploadedBlob))]
        public static async Task ProcessUploadedBlob(
            [BlobTrigger(Constants.BlobContainerName)] BlobClient blobClient,
            [Table(Constants.OutputTableName)] TableClient tableClient)
        {
            var response = await blobClient.GetPropertiesAsync();
            if (!response.HasValue)
            {
                return;
            }

            var properties = response.Value;
            if (properties.ContentLength < 1000) // just some sanity check that blob is not empty
            {
                return;
            }

            // Process
            await using var stream = await blobClient.OpenReadAsync();
            var asyncEnumerable = JsonSerializer.DeserializeAsyncEnumerable<DomainObject>(stream, Constants.DefaultJsonSerializerOptions);

            var numberOfRows = 0L;
            using var sha256 = SHA256.Create();

            await foreach (var domainObject in asyncEnumerable)
            {
                // do stuff with deserialized object...
                numberOfRows++;

                var bytes = Encoding.Unicode.GetBytes(domainObject.Id);
                sha256.TransformBlock(bytes, 0, bytes.Length, null, 0);
            }

            sha256.TransformFinalBlock(Array.Empty<byte>(), 0, 0);
            var hashString = BitConverter.ToString(sha256.Hash ?? Array.Empty<byte>());

            await tableClient.UpsertEntityAsync(new TableEntity(blobClient.Name, string.Empty)
            {
                {"NumberOfRows", numberOfRows},
                {"SHA256", hashString}
            });
        }
    }
}
