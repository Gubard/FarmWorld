using Models;
using TMPro;

namespace Extensions
{
    public static class TextMeshProExtension
    {
        public static void SetOptions(this TextMeshPro textMeshPro, TextMeshProOptions options)
        {
            textMeshPro.text = options.Text;
            textMeshPro.fontSize = options.FontSize;
            textMeshPro.alignment = options.Alignment;
            textMeshPro.fontStyle = options.FontStyle;
        }
    }
}