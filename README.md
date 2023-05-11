# Introduction 
This is a proof-of-concept project demonstrating how to process large JSON files from blob storage.

- .NET 6
- Azure Functions v4

# Getting Started
Setup environment (or local.settings.json) values:

|Name|Description|
|---|---|
|AzureWebJobsStorage|Storage account connection string. Use value `UseDevelopmentStorage=true` for emulator (for example, Azurite)|

# How it works
1. Send HTTP GET request to ´http://localhost:7071/api/sas` -> Response body will contain an URI.
2. Send a HTTP PUT request with JSON file attached to the URI. Add header with value `x-ms-blob-type = BlockBlob`.
3. Wait for the request to complete. When it completes successfully, the JSON file will be uploaded to the blob storage.
4. In the background, a blob trigger will start processing the file, deserializing it asynchronously and doing "stuff" with the deserialized objects.
   See code starting from `ProcessorFunctions.cs:39` and add your own domain object/logic.