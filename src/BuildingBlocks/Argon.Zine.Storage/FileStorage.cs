﻿using Amazon.S3;
using Amazon.S3.Transfer;
using Argon.Zine.Core.Data;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Argon.Storage;

public class FileStorage : IFileStorage
{
    private readonly string _bucketName;
    private readonly TransferUtility _transferUtility;

    public FileStorage(
        string bucketName, 
        TransferUtility transferUtility)
    {
        _bucketName = bucketName;
        _transferUtility = transferUtility;
    }

    public async Task<(string FileName, string Url)> UploadAsync(
        Stream imageStream, string fileName,
        CancellationToken cancellationToken = default)
    {
        var newFileName = $"{Guid.NewGuid()}{Path.GetExtension(fileName)}";

        var uploadRequest = new TransferUtilityUploadRequest
        {
            Key = newFileName,
            BucketName = _bucketName,
            InputStream = imageStream,
            CannedACL = S3CannedACL.PublicRead
        };

        await _transferUtility.UploadAsync(uploadRequest, cancellationToken);

        return (newFileName, $"https://{_bucketName}.s3.amazonaws.com/{newFileName}");
    }
}