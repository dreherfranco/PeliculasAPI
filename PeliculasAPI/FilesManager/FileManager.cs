using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Extensions.Configuration;
using PeliculasAPI.FilesManager.Interface;
using System;
using System.IO;
using System.Threading.Tasks;

namespace PeliculasAPI.FilesManager
{
    public class FileManager: IFileManager
    {
        private readonly string connectionString;

        public FileManager(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("AzureStorage");
        }

        public async Task<string> SaveFile(byte[] content, string extension, string container, string contentType)
        {
            var account = CloudStorageAccount.Parse(connectionString);
            var client = account.CreateCloudBlobClient();
            var referenceContainer = client.GetContainerReference(container);

            await referenceContainer.CreateIfNotExistsAsync();
            await referenceContainer.SetPermissionsAsync(new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            });

            var filename = $"{Guid.NewGuid()}{extension}";
            var blob = referenceContainer.GetBlockBlobReference(filename);
            await blob.UploadFromByteArrayAsync(content, 0, content.Length);
            blob.Properties.ContentType = contentType;
            await blob.SetPropertiesAsync();

            return blob.Uri.ToString();
        }

        public async Task<string> EditFile(byte[] content, string extension, string container, string route, string contentType)
        {
            await DeleteFile(route, container);
            return await SaveFile(content, extension, container, contentType);
        }

        public async Task DeleteFile(string route, string container)
        {
            if(route != null)
            {
                var account = CloudStorageAccount.Parse(connectionString);
                var client = account.CreateCloudBlobClient();
                var referenceContainer = client.GetContainerReference(container);
                
                var blobName = Path.GetFileName(route);
                var blob = referenceContainer.GetBlobReference(blobName);
                await blob.DeleteIfExistsAsync();
            }
        }
    }
}
