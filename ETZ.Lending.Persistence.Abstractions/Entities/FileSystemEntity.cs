using System.IO;

namespace ETZ.Lending.Persistence.Abstractions.Entities
{
    public class FileSystemEntity : Entity<string>
    {
        public string Path { get; set; }

        public Stream Stream { get; set; }
    }
}