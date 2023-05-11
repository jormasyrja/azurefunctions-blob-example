using System.Text.Json;
using System.Text.Json.Serialization;

namespace JS.AzureFunctions.Example.Blob
{
    public static class Constants
    {
        public const string BlobContainerName = "upload-container";

        public static readonly JsonSerializerOptions DefaultJsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter() }
        };

        public const string OutputTableName = "Results";
    }
}
