using Microsoft.AspNetCore.Razor.TagHelpers;

namespace TagHelpers.TagHelpers
{
    public class CurrentDateTagHelper : TagHelper
    {
        public string Format { get; set; } = "yyyy-MM-dd";

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "span";
            var formattedDate = DateTime.Now.ToString(Format);
            output.Content.SetContent(formattedDate);
        }
    }
}
