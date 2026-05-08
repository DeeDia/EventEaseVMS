using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;


namespace EventEaseVMS.EEVServices
{
    public class BlobStorageServices
    {
        private readonly BlobServiceClient _blobServiceClient;
        private const string ContainerName = "venue-images";
        
        public BlobStorageServices(IConfiguration configuration)
        {
            _blobServiceClient = new BlobServiceClient (configuration.GetConnectionString("AzureBlobStorage"));
        }

        // Uploads a file and returns the public Azure Blob URL
        public async Task<string> UploadImageAsync(IFormFile file)
        {
            var containerClient =
                _blobServiceClient.GetBlobContainerClient(ContainerName);
            // Unique filename prevents overwriting existing blobs
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var blobClient = containerClient.GetBlobClient(fileName);
            using var stream = file.OpenReadStream();
            await blobClient.UploadAsync(stream, new BlobHttpHeaders
            { ContentType = file.ContentType });
            return blobClient.Uri.ToString(); // public URL saved to DataBase
        }

        // Deletes an existing blob image by its URL
        public async Task DeleteImageAsync(string imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl)) return;
            var fileName = Path.GetFileName(new Uri(imageUrl).LocalPath);
            var container = _blobServiceClient.GetBlobContainerClient(ContainerName);
            await container.GetBlobClient(fileName).DeleteIfExistsAsync();
        }


    }
}
