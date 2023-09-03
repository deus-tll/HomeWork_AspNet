using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace TagHelpers.TagHelpers
{
    public class LinkToActionTagHelper : TagHelper
    {
        public string Controller { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;
        [HtmlAttributeName("target-blank")]
        public bool IsTargetBlank { get; set; }

        [ViewContext]
        [HtmlAttributeNotBound]
        public required ViewContext ViewContext { get; set; }
        private readonly LinkGenerator _linkGenerator;

        public LinkToActionTagHelper(LinkGenerator linkGenerator)
        {
            _linkGenerator = linkGenerator;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var url = _linkGenerator.GetUriByAction(ViewContext.HttpContext, Action, Controller);

            output.TagName = "a";
            output.Attributes.SetAttribute("href", url);

            if (IsTargetBlank)
            {
                output.Attributes.SetAttribute("target", "_blank");
            }
        }
    }
}
