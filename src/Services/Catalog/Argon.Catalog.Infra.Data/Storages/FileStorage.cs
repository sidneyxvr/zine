using Argon.Catalog.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Argon.Catalog.Infra.Data.Storages
{
    public class FileStorage : IFileStorage
    {
        public Task<IEnumerable<Image>?> AddAsync(
            List<(Stream Image, string FileName)> images, 
            CancellationToken cancellationToken = default)
        {
            if (images is null)
            {
                throw new ArgumentNullException(nameof(images));
            }

            throw new NotImplementedException();
        }
    }
}
