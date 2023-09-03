using Microsoft.AspNetCore.Razor.TagHelpers;

namespace TagHelpers.TagHelpers
{
    public class YoutubeVideoTagHelper : TagHelper
    {
        public string Url { get; set; } = string.Empty;
        public string Title { get; set; } = "YouTube video player";
        public int FrameBorder { get; set; } = 0;
        public string Allow { get; set; } = "accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share";
        public bool AllowFullScreen { get; set; } = true;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            Url = Url.Replace("https://youtu.be/", "https://www.youtube.com/embed/");

            output.TagName = "iframe";
            output.Attributes.SetAttribute("src", Url);
            output.Attributes.SetAttribute("width", "600");
            output.Attributes.SetAttribute("height", "315");
            output.Attributes.SetAttribute("title", Title);
            output.Attributes.SetAttribute("frameborder", FrameBorder);
            output.Attributes.SetAttribute("allow", Allow);

            if (AllowFullScreen)
            {
                output.Attributes.SetAttribute("allowfullscreen", "true");
            }
        }
    }
}
