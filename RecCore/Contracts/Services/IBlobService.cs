using RecCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecCore.Contracts.Services
{
    public interface IBlobService
    {
        public Task<BlobResponseModel> GetBlobAsync(string name);
        public Task<IEnumerable<string>> ListBlobsAsync();

        public Task UploadFileBlobAsync(string filePath, string fileName);

        public Task DeleteBlobAsync(string blobName);
    }
}
