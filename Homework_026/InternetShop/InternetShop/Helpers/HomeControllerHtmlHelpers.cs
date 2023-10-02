using Library.Models.DataModels;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Security.Claims;

namespace InternetShop.Helpers
{
    public static class HomeControllerHtmlHelpers
    {
        public static IHtmlContent CreateChoosingReason(this IHtmlHelper htmlHelper, string title, string icon)
        {
            var figureTag = new TagBuilder("div");
            figureTag.AddCssClass("d-flex align-items-center mb-4");

            var spanTag = new TagBuilder("span");
            spanTag.AddCssClass($"rounded-circle bg-white p-3 d-flex mr-2 mb-2");
            var iconTag = new TagBuilder("i");
            iconTag.AddCssClass($"fas {icon} fa-2x fa-fw text-primary floating");
            spanTag.InnerHtml.AppendHtml(iconTag);
            
            var figcaptionTag = new TagBuilder("figcaption");
            figcaptionTag.AddCssClass("info");
            var titleHeaderTag = new TagBuilder("h6");
            titleHeaderTag.AddCssClass("title");
            titleHeaderTag.InnerHtml.Append(title);

            figcaptionTag.InnerHtml.AppendHtml(titleHeaderTag);

            figureTag.InnerHtml.AppendHtml(spanTag);
            figureTag.InnerHtml.AppendHtml(figcaptionTag);

            return figureTag;
        }


        public static IHtmlContent CreateCarouselItem(this IHtmlHelper htmlHelper, string src, string bigText, string smallText, bool isActive = false)
        {
            var carouselItemDivTag = new TagBuilder("div");
            carouselItemDivTag.AddCssClass($"carousel-item {(!isActive ? "" : "active")}");
            carouselItemDivTag.Attributes["style"] = "height: 510px;";

            var imgTag = new TagBuilder("img");
            imgTag.AddCssClass("img-fluid");
            imgTag.Attributes["src"] = src;
            imgTag.Attributes["alt"] = "Image";

            var carouselCaptionDivTag = new TagBuilder("div");
            carouselCaptionDivTag.AddCssClass("carousel-caption d-flex flex-column align-items-center justify-content-center");
            
            var carouselCaptionInnerDivTag = new TagBuilder("div");
            carouselCaptionInnerDivTag.AddCssClass("p-3");
            carouselCaptionInnerDivTag.Attributes["style"] = "max-width: 700px;";

            var smallTextTag = new TagBuilder("h4");
            smallTextTag.AddCssClass("text-light text-uppercase font-weight-medium mb-3");
            smallTextTag.InnerHtml.Append(smallText);

            var bigTextTag = new TagBuilder("h3");
            bigTextTag.AddCssClass("display-4 text-white font-weight-semi-bold mb-4");
            bigTextTag.InnerHtml.Append(bigText);

            var linkTag = new TagBuilder("a");
            linkTag.AddCssClass("btn btn-light py-2 px-3");
            linkTag.InnerHtml.Append("Shop Now");
            linkTag.Attributes["href"] = "/Product/Shop";

            carouselCaptionInnerDivTag.InnerHtml.AppendHtml(smallTextTag);
            carouselCaptionInnerDivTag.InnerHtml.AppendHtml(bigTextTag);
            carouselCaptionInnerDivTag.InnerHtml.AppendHtml(linkTag);

            carouselCaptionDivTag.InnerHtml.AppendHtml(carouselCaptionInnerDivTag);

            carouselItemDivTag.InnerHtml.AppendHtml(imgTag);
            carouselItemDivTag.InnerHtml.AppendHtml(carouselCaptionDivTag);

            return carouselItemDivTag;
        }


        public static IHtmlContent CreateProductItem(this IHtmlHelper htmlHelper, ProductDb product, ClaimsPrincipal user)
        {
            var productItemDivTag = new TagBuilder("div");
            productItemDivTag.AddCssClass("card product-item border-0 mb-2");
            productItemDivTag.Attributes["style"] = "width:100%; height:95%; max-width:408px; max-height:539.017px;";

            /////////////cardHeaderDivTag/////////////
            var cardHeaderDivTag = new TagBuilder("div");
            cardHeaderDivTag.AddCssClass("card-header product-img position-relative overflow-hidden bg-transparent border p-0 w-100 h-100");

            var discountDivTag = new TagBuilder("div");
            discountDivTag.AddCssClass("position-absolute d-flex align-items-center justify-content-center custom-container-discount");
            var textDiscount = new TagBuilder("h6");
            textDiscount.AddCssClass("custom-text-discount");
            textDiscount.InnerHtml.Append($"-{product.DiscountPercentage}%");

            discountDivTag.InnerHtml.AppendHtml(textDiscount);

            var imgTag = new TagBuilder("img");
            imgTag.AddCssClass("img-fluid");
            imgTag.Attributes.Add("src", product.Thumbnail);

            cardHeaderDivTag.InnerHtml.AppendHtml(discountDivTag);
            cardHeaderDivTag.InnerHtml.AppendHtml(imgTag);
            /////////////cardHeaderDivTag/////////////

            /////////////cardBodyDivTag/////////////
            var cardBodyDivTag = new TagBuilder("div");
            cardBodyDivTag.AddCssClass("card-body border-left border-right text-center p-0 pt-4 pb-3");

            var titleProduct = new TagBuilder("h6");
            titleProduct.AddCssClass("text-truncate mb-3");
            titleProduct.InnerHtml.Append(product.Title);

            var pricesDivTag = new TagBuilder("div");
            pricesDivTag.AddCssClass("d-flex justify-content-center");

            var priceDiscounted = new TagBuilder("h6");
            priceDiscounted.InnerHtml.AppendHtml(PriceFormatter(htmlHelper, product.CalculateDiscountedPrice(), "$"));

            var price = new TagBuilder("h6");
            price.AddCssClass("text-muted ml-2");
            var delTag = new TagBuilder("del");
            delTag.InnerHtml.AppendHtml(PriceFormatter(htmlHelper, product.Price, "$"));
            price.InnerHtml.AppendHtml(delTag);

            pricesDivTag.InnerHtml.AppendHtml(priceDiscounted);
            pricesDivTag.InnerHtml.AppendHtml(price);

            cardBodyDivTag.InnerHtml.AppendHtml(titleProduct);
            cardBodyDivTag.InnerHtml.AppendHtml(pricesDivTag);
            /////////////cardBodyDivTag/////////////

            /////////////cardFooterDivTag/////////////
            var cardFooterDivTag = new TagBuilder("div");
            cardFooterDivTag.AddCssClass("card-footer d-flex justify-content-between bg-light border");

            var linkTag = new TagBuilder("a");
            linkTag.AddCssClass("btn btn-sm text-dark p-0");

            var linkIconTag = new TagBuilder("i");
            linkIconTag.AddCssClass("fas fa-eye text-primary mr-1");

            linkTag.InnerHtml.AppendHtml(linkIconTag);
            linkTag.InnerHtml.Append("View Details");
            linkTag.Attributes["href"] = "/Product/ProductDetails";


            var buttonTag = new TagBuilder("button");
            buttonTag.AddCssClass("btn btn-sm text-dark p-0 add-to-cart");
            buttonTag.Attributes.Add("data-product-id", product.Id.ToString());

            var buttonIconTag = new TagBuilder("i");
            buttonIconTag.AddCssClass("fas fa-shopping-cart text-primary mr-1");

            bool? isAuthenticated = user.Identity?.IsAuthenticated;
            if (isAuthenticated.HasValue && isAuthenticated is true)
            {
                buttonTag.InnerHtml.AppendHtml(buttonIconTag);
                buttonTag.InnerHtml.Append("Add To Cart");
            }
            else
            {
                buttonIconTag.AddCssClass("text-danger");

                var loginLinkTag = new TagBuilder("a");
                loginLinkTag.AddCssClass("btn btn-sm text-danger p-0");
                loginLinkTag.Attributes["href"] = "/Account/Login";
                loginLinkTag.InnerHtml.AppendHtml(buttonIconTag);
                loginLinkTag.InnerHtml.Append("Login to Add to Cart");

                buttonTag = loginLinkTag;
            }

            cardFooterDivTag.InnerHtml.AppendHtml(linkTag);
            cardFooterDivTag.InnerHtml.AppendHtml(buttonTag);
            /////////////cardFooterDivTag/////////////


            productItemDivTag.InnerHtml.AppendHtml(cardHeaderDivTag);
            productItemDivTag.InnerHtml.AppendHtml(cardBodyDivTag);
            productItemDivTag.InnerHtml.AppendHtml(cardFooterDivTag);

            return productItemDivTag;
        }

        public static IHtmlContent PriceFormatter(this IHtmlHelper htmlHelper, decimal price, string currencySymbol)
        {
            var formattedPrice = string.Format("{0}{1:N2}", currencySymbol, price);
            return new HtmlString(formattedPrice);
        }
    }
}
