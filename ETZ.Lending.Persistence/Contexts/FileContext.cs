using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;

namespace ETZ.Lending.Persistence.Contexts
{
    public class FileContext : IFileContext
    {
        private readonly DirectoryInfo _directory;
        private readonly IDictionary<string, Stream> _modificationQueue;
        private readonly IList<string> _deleteQueue;

        public FileContext(DirectoryInfo directory)
        {
            _directory = directory;
            _modificationQueue = new Dictionary<string, Stream>();
            _deleteQueue = new List<string>();
        }

        public string Name => _directory.Name;

        public string FullName => _directory.FullName;

        private int Affected => _modificationQueue.Count + _deleteQueue.Count;

        public IEnumerable<FileInfo> GetFiles()
        {
            return _directory.GetFiles();
        }

        public IEnumerable<DirectoryInfo> GetDirectories()
        {
            return _directory.GetDirectories();
        }

        public IEnumerable<FileSystemInfo> GetFileSystemInfos()
        {
            return _directory.GetFileSystemInfos();
        }

        public void Write(string path, Stream stream)
        {
            _modificationQueue.Add(path, stream);
        }

        public void Delete(string path)
        {
            _deleteQueue.Add(path);
        }

        public int SaveChanges()
        {
            foreach (var (path, stream) in _modificationQueue)
            {
                using var fileSteam = new FileStream(path, FileMode.OpenOrCreate);
                stream.CopyTo(fileSteam);
            }

            foreach (var path in _deleteQueue)
            {
                File.Delete(path);
            }

            var affected = Affected;

            _modificationQueue.Clear();
            _deleteQueue.Clear();

            return affected;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            foreach (var (path, stream) in _modificationQueue)
            {
                await using var fileSteam = new FileStream(path, FileMode.OpenOrCreate);
                await stream.CopyToAsync(fileSteam, cancellationToken);
            }

            foreach (var path in _deleteQueue)
            {
                FileSystem.DeleteFile(Path.GetFullPath(path));
            }

            var affected = Affected;

            _modificationQueue.Clear();
            _deleteQueue.Clear();

            return affected;
        }
    }
}