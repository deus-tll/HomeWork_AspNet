using Microsoft.AspNetCore.Razor.TagHelpers;

namespace TagHelpers.TagHelpers
{
    public class UpperCaseTagHelper : TagHelper
    {
        public string Text { get; set; } = string.Empty;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (!string.IsNullOrEmpty(Text))
            {
                output.TagName = "span";
                output.Content.SetContent(Text.ToUpper());
            }
        }
    }
}
