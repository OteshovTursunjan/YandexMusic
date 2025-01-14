using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Minio;
using Minio.DataModel.Args;
using Minio.Exceptions;
using System;
using System.IO;
using System.Threading.Tasks;
using YandexMusic.DataAccess.DTOs;
using YandexMusic.DataAccess.Repository;
using YandexMusic.DataAccess.Repository.lmpl;
using YandexMusics.Core.Entities.Music;

namespace YandexMusic.Application.Services.Impl
{
    public class MinioService : IMinoService
    {
        private readonly MinioClient _minioClient;
        private readonly string _bucketName;
        private readonly string _endpoint;
        private readonly IMusicRepository musicRepository;

        public MinioService(IConfiguration configuration , IMusicRepository musicRepository)
        {
            this.musicRepository = musicRepository;
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            _endpoint = configuration["MinioSettings:Endpoint"]
                ?? throw new InvalidOperationException("Minio endpoint configuration is missing");
            var accessKey = configuration["MinioSettings:AccessKey"]
                ?? throw new InvalidOperationException("Minio access key configuration is missing");
            var secretKey = configuration["MinioSettings:SecretKey"]
                ?? throw new InvalidOperationException("Minio secret key configuration is missing");
            _bucketName = configuration["MinioSettings:BucketName"]
                ?? throw new InvalidOperationException("Minio bucket name configuration is missing");

            _minioClient = (MinioClient)new MinioClient()
                .WithEndpoint(_endpoint)
                .WithCredentials(accessKey, secretKey)
                .Build();

            EnsureBucketExists().GetAwaiter().GetResult();
        }

        private async Task EnsureBucketExists()
        {
            try
            {
                var bucketExists = await _minioClient.BucketExistsAsync(
                    new BucketExistsArgs().WithBucket(_bucketName));
                if (!bucketExists)
                {
                    await _minioClient.MakeBucketAsync(
                        new MakeBucketArgs().WithBucket(_bucketName));
                }
            }
            catch (MinioException ex)
            {
                throw new InvalidOperationException($"Failed to initialize Minio bucket: {ex.Message}", ex);
            }
        }

        public async Task UploadFileAsync(string  fileName, MusicDTO musicDTO, Stream fileStream)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentException("File name cannot be empty", nameof(fileName));

            if (fileStream == null || !fileStream.CanRead)
                throw new ArgumentException("Invalid file stream", nameof(fileStream));

            try
            {
                var putObjectArgs = new PutObjectArgs()
                    .WithBucket(_bucketName)
                    .WithObject(fileName)
                    .WithStreamData(fileStream)
                    .WithObjectSize(fileStream.Length)
                    .WithContentType(GetContentType(fileName));
                var fileUrl = $"http://9001/{_bucketName}/{fileName}";
                //http://localhost:9001/browser/yandexmuics/%D0%A4%D0%9E%D0%93%D0%95%D0%9B%D0%AC-%D0%9C%D0%90%D0%9B%D0%AC%D0%A7%D0%98%D0%9A.mp3
                await _minioClient.PutObjectAsync(putObjectArgs);
                var newMusic = new Musics
                {
                    Name = musicDTO.Name,
                    Path = fileUrl,
                    AuthorId = musicDTO.AuthotId,
                    GenreId = musicDTO.GenreId,
                };
                await musicRepository.AddAsync(newMusic);
                
            }
            catch (MinioException ex)
            {
                throw new InvalidOperationException($"Failed to upload file {fileName}: {ex.Message}", ex);
            }
        }

        public async Task<Stream> GetFileAsync(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentException("File name cannot be empty", nameof(fileName));

            try
            {
                if (!await FileExistsAsync(fileName))
                    throw new FileNotFoundException($"File {fileName} not found in bucket {_bucketName}");

                var memoryStream = new MemoryStream();
                var getObjectArgs = new GetObjectArgs()
                    .WithBucket(_bucketName)
                    .WithObject(fileName)
                    .WithCallbackStream(stream =>
                    {
                        stream.CopyTo(memoryStream);
                    });

                await _minioClient.GetObjectAsync(getObjectArgs);
                memoryStream.Position = 0;
                return memoryStream;
            }
            catch (MinioException ex)
            {
                throw new InvalidOperationException($"Failed to retrieve file {fileName}: {ex.Message}", ex);
            }
        }

        public async Task<bool> FileExistsAsync(string fileName)
        {
            try
            {
                await _minioClient.StatObjectAsync(
                    new StatObjectArgs()
                        .WithBucket(_bucketName)
                        .WithObject(fileName));
                return true;
            }
            catch (MinioException)
            {
                return false;
            }
        }

        private string GetContentType(string fileName)
        {
            var extension = Path.GetExtension(fileName).ToLowerInvariant();
            return extension switch
            {
                ".mp3" => "audio/mpeg",
                ".wav" => "audio/wav",
                ".ogg" => "audio/ogg",
                ".m4a" => "audio/mp4",
                _ => "application/octet-stream"
            };
        }
    }
}
