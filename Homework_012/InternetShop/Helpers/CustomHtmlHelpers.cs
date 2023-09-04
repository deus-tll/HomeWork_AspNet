using InternetShop.Models.DataModels;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InternetShop.Helpers
{
    public static class CustomHtmlHelpers
    {
        public static IHtmlContent ProductLink(this IHtmlHelper htmlHelper, Product product)
        {
            var linkGenerator = htmlHelper.ViewContext.HttpContext.RequestServices.GetRequiredService<LinkGenerator>();
            var link = linkGenerator.GetPathByAction("AboutProduct", "Home", new { id = product.Id });
            var linkTag = new TagBuilder("a");

            linkTag.Attributes["href"] = link;
            linkTag.AddCssClass("list-group-item list-group-item-action px-3 border-0 text-center");
            linkTag.InnerHtml.Append($"{product.Name} [by {product.Brand}]");

            return linkTag;
        }


        public static IHtmlContent SubmitButton(this IHtmlHelper htmlHelper, string buttonText, string value = "")
        {
            var buttonTag = new TagBuilder("button");
            buttonTag.AddCssClass("btn btn-submit me-5 mt-2");
            buttonTag.Attributes["type"] = "submit";
            buttonTag.InnerHtml.Append(buttonText);

            if (!string.IsNullOrEmpty(value))
            {
                buttonTag.Attributes["name"] = "action";
                buttonTag.Attributes["value"] = value;
            }

            return buttonTag;
        }


        public static IHtmlContent SubmitDivButtonBlock(this IHtmlHelper htmlHelper, string buttonText, string buttonValue = "")
        {
            var divTag = new TagBuilder("div");
            divTag.AddCssClass("text-center pt-5");

            var buttonTag = SubmitButton(htmlHelper, buttonText, buttonValue);

            divTag.InnerHtml.AppendHtml(buttonTag);

            return divTag;
        }


        public static IHtmlContent SectionTitle(this IHtmlHelper htmlHelper, string title)
        {
            var divTag = new TagBuilder("div");
            divTag.AddCssClass("section-title");

            var h2Tag = new TagBuilder("h2");
            h2Tag.InnerHtml.Append(title);

            divTag.InnerHtml.AppendHtml(h2Tag);

            return divTag;
        }


        public static IHtmlContent PriceFormatter(this IHtmlHelper htmlHelper, decimal price, string currencySymbol)
        {
            var formattedPrice = string.Format("{0} {1:N2}", currencySymbol, price);
            return new HtmlString(formattedPrice);
        }


        public static IHtmlContent DateFormatter(this IHtmlHelper htmlHelper, DateTime date, string format)
        {
            var formattedDate = date.ToString(format);
            return new HtmlString(formattedDate);
        }
    }
}
