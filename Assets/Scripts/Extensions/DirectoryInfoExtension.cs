using System.IO;

namespace Extensions
{
    public static class DirectoryInfoExtension
    {
        public static DirectoryInfo Combine(this DirectoryInfo directory, string part)
        {
            var path = Path.Combine(directory.FullName, part);

            return new DirectoryInfo(path);
        }
        
        public static FileInfo ToFile(this DirectoryInfo directory, string fileName)
        {
            var path = Path.Combine(directory.FullName, fileName);

            return new FileInfo(path);
        }
    }
}