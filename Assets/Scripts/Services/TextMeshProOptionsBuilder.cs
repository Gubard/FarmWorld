using Models;
using TMPro;

namespace Services
{
    public class TextMeshProOptionsBuilder
    {
        private string _text;
        private float _fontSize;
        private FontStyles _fontStyle;
        private TextAlignmentOptions _alignment;

        public TextMeshProOptionsBuilder()
        {
            _text = string.Empty;
            _fontSize = 1;
            _fontStyle = FontStyles.Normal;
            _alignment = TextAlignmentOptions.TopLeft;
        }

        public TextMeshProOptionsBuilder SetText(string text)
        {
            _text = text;

            return this;
        }
        
        public TextMeshProOptionsBuilder SetFontSize(float fontSize)
        {
            _fontSize = fontSize;

            return this;
        }
        
        public TextMeshProOptionsBuilder SetFontStyle(FontStyles fontStyle)
        {
            _fontStyle = fontStyle;

            return this;
        }
        
        public TextMeshProOptionsBuilder SetAlignment(TextAlignmentOptions alignment)
        {
            _alignment = alignment;

            return this;
        }

        public TextMeshProOptions Build()
        {
            return new TextMeshProOptions(_text, _fontSize, _fontStyle, _alignment);
        }
    }
}