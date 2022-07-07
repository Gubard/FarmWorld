namespace Models
{
    public class FileName
    {
        public string Name { get; }
        public string Extension { get; }

        public FileName(string name, string extension)
        {
            Name = name;
            Extension = extension;
        }

        public FileName(string fileName)
        {
            var index = fileName.LastIndexOf(".");

            if (index < 0)
            {
                Name = fileName;
            }
            else
            {
                Name = fileName.Substring(0, index);
                Extension = fileName.Substring(index + 1, fileName.Length - index - 1);
            }
        }

        public override string ToString()
        {
            if (Extension is null)
            {
                return Name;
            }

            return $"{Name}.{Extension}";
        }
    }
}