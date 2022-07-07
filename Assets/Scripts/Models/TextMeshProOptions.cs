using TMPro;

namespace Models
{
    public readonly struct TextMeshProOptions
    {
        public static readonly TextMeshProOptions Default =
            new TextMeshProOptions(string.Empty, 1, FontStyles.Normal, TextAlignmentOptions.TopLeft);

        public string Text { get; }
        public float FontSize { get; }
        public FontStyles FontStyle { get; }
        public TextAlignmentOptions Alignment { get; }

        public TextMeshProOptions(string text, float fontSize, FontStyles fontStyle, TextAlignmentOptions alignment)
        {
            Text = text;
            FontSize = fontSize;
            FontStyle = fontStyle;
            Alignment = alignment;
        }
    }
}