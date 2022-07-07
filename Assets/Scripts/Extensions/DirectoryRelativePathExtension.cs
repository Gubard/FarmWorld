using System.IO;
using Models;

namespace Extensions
{
    public static class DirectoryRelativePathExtension
    {
        public static DirectoryRelativePath Combine(this DirectoryRelativePath directoryRelativePath, string part)
        {
            var path = Path.Combine(directoryRelativePath.ToString(), part);

            return new DirectoryRelativePath(path);
        }
        
        public static FileRelativePath ToFileRelativePath(this DirectoryRelativePath directoryRelativePath, string part)
        {
            var path = Path.Combine(directoryRelativePath.ToString(), part);

            return new FileRelativePath(path);
        }
    }
}