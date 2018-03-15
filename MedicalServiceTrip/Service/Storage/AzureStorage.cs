using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using Core.Configuration;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Service.Storage
{
    public class AzureStorage : IStorage
    {
        #region Fields

        private readonly MSTConfig _config;
        
        #endregion

        #region Cors

        public  AzureStorage(MSTConfig config)
        {
            _config = config;
        }

        #endregion

        #region Methods

        
        public bool StoreFile(string fileName, string pathToStore,Stream data)
        {
            CloudBlobContainer cloudBlobContainer = GetCloudBlobContainer(pathToStore);
            if (!cloudBlobContainer.Exists())
            {
                cloudBlobContainer.Create();
            }
            CloudBlockBlob blob = cloudBlobContainer.GetBlockBlobReference(fileName);

            blob.UploadFromStream(data);
            return true;
            
        }

        private CloudBlobContainer GetCloudBlobContainer(string containerName)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_config.AzureConnection);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);
            return container;
        }

        #endregion
    }
}
