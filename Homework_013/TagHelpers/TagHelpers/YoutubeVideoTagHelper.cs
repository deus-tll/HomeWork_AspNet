using Microsoft.AspNetCore.Razor.TagHelpers;

namespace TagHelpers.TagHelpers
{
    public class YoutubeVideoTagHelper : TagHelper
    {
        public string Url { get; set; } = string.Empty;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "iframe";
            output.Attributes.SetAttribute("src", Url);
            output.Attributes.SetAttribute("width", 600);
            output.Attributes.SetAttribute("height", 350);
        }

    }
}
