﻿using Microsoft.AspNetCore.Razor.TagHelpers;

namespace TagHelpers.TagHelpers
{
    public class TooltipButtonTagHelper : TagHelper
    {
        public string Text { get; set; } = string.Empty;
        public string Tooltip { get; set; } = string.Empty;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "button";
            output.Attributes.SetAttribute("type", "button");
            output.Attributes.SetAttribute("data-toggle", "tooltip");
            output.Attributes.SetAttribute("title", Tooltip);
            output.Content.SetContent(Text);
        }
    }
}
