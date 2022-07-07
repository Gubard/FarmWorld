using System.IO;

namespace Models
{
    public class FileRelativePath
    {
        private DirectoryRelativePath Path { get; }
        private FileName Name { get; }

        public FileRelativePath(string path)
        {
            Path = new DirectoryRelativePath(System.IO.Path.GetDirectoryName(path));
            Name = new FileName(System.IO.Path.GetFileName(path));
        }
        
        public FileRelativePath(DirectoryRelativePath path, FileName name)
        {
            Path = path;
            Name = name;
        }

        public FileInfo ToFile()
        {
            return new FileInfo(ToString());
        }

        public override string ToString()
        {
            return System.IO.Path.Combine(Path.ToString(), Name.ToString());
        }
    }
}