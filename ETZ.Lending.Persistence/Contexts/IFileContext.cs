using System.Collections.Generic;
using System.IO;

namespace ETZ.Lending.Persistence.Contexts
{
    public interface IFileContext : IContext
    {
        public string Name { get; }
        public string FullName { get; }
        public IEnumerable<FileInfo> GetFiles();
        public IEnumerable<DirectoryInfo> GetDirectories();
        public IEnumerable<FileSystemInfo> GetFileSystemInfos();
        public void Write(string path, Stream stream);
        public void Delete(string path);
    }
}