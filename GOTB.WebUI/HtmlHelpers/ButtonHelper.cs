using System.Text;
using System.Web.Mvc;
using GoTB.WebUI.Models;

namespace GoTB.WebUI.HtmlHelpers
{
    public static class ButtonHelper
    {
        public static MvcHtmlString CreateButtonIfNeeded(this HtmlHelper html, ButtonViewModel bvm)
        {
            StringBuilder result = new StringBuilder();
            if (bvm.NeedToShowButton)
            {
                TagBuilder tagForm = new TagBuilder("form");
                tagForm.Attributes["action"] = string.Format("/{0}/{1}", bvm.Controller, bvm.Method);
                tagForm.Attributes["method"] = "post";
                if (bvm.HiddenParams != null && bvm.HiddenParams.ContainsKey("ajax"))
                {
                    tagForm.Attributes["data-ajax-update"] = bvm.HiddenParams["ajax"];
                    tagForm.Attributes["data-ajax-mode"] = "replace";
                    tagForm.Attributes["data-ajax"] = "true";
                    tagForm.Attributes["id"] = "form0";
                }


                var inner = new StringBuilder();
                if (bvm.HiddenParams != null)
                {
                    foreach (var hiddenParam in bvm.HiddenParams)
                    {
                        if (hiddenParam.Key == "ajax")
                            continue;
                        var tag = new TagBuilder("input");
                        tag.Attributes["type"] = "hidden";
                        tag.Attributes["id"] = tag.Attributes["name"] = hiddenParam.Key;
                        tag.Attributes["value"] = hiddenParam.Value;
                        inner.Append(tag.ToString());
                    }
                }
                TagBuilder tagBut = new TagBuilder("input");
                tagBut.Attributes["type"] = "submit";
                tagBut.Attributes["value"] = bvm.TextButton;
                inner.Append(tagBut);
                tagForm.InnerHtml = inner.ToString();
                result.Append(tagForm.ToString());
            }
            else
            {
                TagBuilder div = new TagBuilder("div");
                div.InnerHtml = bvm.TextIfNotNeeded;
                result.Append(div.ToString());
            }
            return MvcHtmlString.Create(result.ToString());
        }
    }
}