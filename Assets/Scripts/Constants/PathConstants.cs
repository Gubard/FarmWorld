using Extensions;
using Models;

namespace Constants
{
    public static class PathConstants
    {
        public const string AssetsFolderName = "Assets";
        public const string JonathanFoustFolderName = "Jonathan Foust";
        public const string MeshesFolderName = "Meshes";
        public const string MaterialsFolderName = "Materials";
        public static readonly DirectoryRelativePath AssetsPath = new (AssetsFolderName);
        public static readonly DirectoryRelativePath MaterialsDirectory = AssetsPath.Combine(MaterialsFolderName);

        public static readonly DirectoryRelativePath JonathanFoustAssetsDirectory =
            AssetsPath.Combine(JonathanFoustFolderName);

        public static readonly DirectoryRelativePath JonathanFoustMeshesDirectory =
            JonathanFoustAssetsDirectory.Combine(MeshesFolderName);

        public static readonly DirectoryRelativePath JonathanFoustMaterialsDirectory =
            JonathanFoustAssetsDirectory.Combine(MaterialsFolderName);
    }
}