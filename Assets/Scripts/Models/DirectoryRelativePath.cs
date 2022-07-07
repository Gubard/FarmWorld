using System.IO;

namespace Models
{
    public class DirectoryRelativePath
    {
        private readonly string _path;

        public DirectoryRelativePath(string path)
        {
            _path = path;
        }

        public DirectoryInfo ToDirectory()
        {
            return new DirectoryInfo(ToString());
        }

        public override string ToString()
        {
            return _path;
        }
    }
}