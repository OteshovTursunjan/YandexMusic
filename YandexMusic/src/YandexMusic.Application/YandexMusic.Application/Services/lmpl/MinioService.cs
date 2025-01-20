using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Minio;
using Minio.DataModel.Args;
using Minio.Exceptions;
using System.IO;
using YandexMusic.DataAccess.DTOs;
using YandexMusic.DataAccess.Persistance;
using YandexMusic.DataAccess.Repository;
using YandexMusics.Core.Entities.Music;

namespace YandexMusic.Application.Services.Impl
{
    public class MinioService : IMinoService
    {
        private readonly MinioClient _minioClient;
        private readonly string _bucketName;
        private readonly string _endpoint;
        private readonly IMusicRepository musicRepository;
        private readonly DatabaseContext _databaseContext;

        public MinioService(IConfiguration configuration, IMusicRepository musicRepository,DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
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

        public async Task UploadFileAsync(string fileName, MusicDTO musicDTO, Stream fileStream)
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
                var fileUrl = $"http://localhost:9001/browser/yandexmuics/{fileName}";
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

        public async Task<(MemoryStream Stream, string ContentType)> GetMusic()
        {
            try
            {
               
                var randomMusic = await _databaseContext.Musics
                    .FromSqlRaw("SELECT * FROM \"Musics\" ORDER BY RANDOM() LIMIT 1")
                    .AsNoTracking() 
                    .FirstOrDefaultAsync();

                if (randomMusic == null)
                {
                    throw new Exception("No music was found.");
                }

              
                var fileName = GetFileNameFromUrl(randomMusic.Path);
                var memory = new MemoryStream();

                var getobjects = new GetObjectArgs()
                    .WithBucket(_bucketName)
                    .WithObject(fileName)
                    .WithCallbackStream(stream =>
                    {
                        stream.CopyTo(memory);
                        memory.Position = 0; 
                    });

                await _minioClient.GetObjectAsync(getobjects);

                return (memory, "audio/mpeg");
            }
            catch (MinioException ex)
            {
                throw new Exception($"Error fetching file from MinIO: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred: {ex.Message}");
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
        public async Task<(MemoryStream Stream, string ContentType)> GetMusicAsync(Guid id)
        {
            var music = await musicRepository.GetFirstAsync(u => u.Id == id);
            if (music == null)
            {
                throw new Exception("Music does not exist");
            }

            var fileUrl = music.Path;
            var fileName = GetFileNameFromUrl(fileUrl);

            Console.WriteLine($"Attempting to fetch file: {fileName} from bucket: {_bucketName}");

            try
            {
                var memoryStream = new MemoryStream();
                var getObject = new GetObjectArgs()
                    .WithBucket(_bucketName)
                    .WithObject(fileName)
                    .WithCallbackStream(stream =>
                    {
                         stream.CopyTo(memoryStream);
                       
                    });

                await _minioClient.GetObjectAsync(getObject);
                memoryStream.Position = 0;

                Console.WriteLine($"Stream length: {memoryStream.Length}");

                return (memoryStream, "audio/mpeg");
            }
            catch (MinioException ex)
            {
                Console.WriteLine($"MinIO Exception: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                throw new Exception($"Error fetching file from MinIO: {ex.Message}");
            }
        }

        public async Task DeleteFileAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("ID cannot be empty", nameof(id));

            var music = await musicRepository.GetFirstAsync(u => u.Id == id);
            if (music == null)
                throw new InvalidOperationException($"Music record with ID {id} not found.");

            var fileUrl = music.Path;
            var fileName = GetFileNameFromUrl(fileUrl);

            if (string.IsNullOrWhiteSpace(fileName))
                throw new InvalidOperationException("Invalid file URL in database record.");

            try
            {
                var removeObjectArgs = new RemoveObjectArgs()
                    .WithBucket(_bucketName)
                    .WithObject(fileName);

                await _minioClient.RemoveObjectAsync(removeObjectArgs);
                await musicRepository.DeleteAsync(music);
             
            }
            catch (MinioException ex)
            {
                throw new InvalidOperationException($"Failed to delete file {fileName} from Minio: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to delete record with ID {id}: {ex.Message}", ex);
            }
        }
        private string GetFileNameFromUrl(string url)
        {
            return url?.Split('/').LastOrDefault();
        }
        private string GetObjectKeyFromPath(string path)
        {
            var uri = new Uri(path);
            var segments = uri.AbsolutePath.Split('/');
            return string.Join("/", segments[3..]);
        }
    }
}
