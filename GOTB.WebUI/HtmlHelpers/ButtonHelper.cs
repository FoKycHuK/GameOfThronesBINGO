using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace GoTB.WebUI.HtmlHelpers
{
    public static class ButtonHelper
    {
        public static MvcHtmlString CreateButtonIfNeeded(this HtmlHelper html, bool needButton,
            string method, string controller, string textButton, string textIfNotNeeded,
            Dictionary<string, string> hiddenParams = null)
        {
            StringBuilder result = new StringBuilder();
            if (needButton)
            {
                TagBuilder tagForm = new TagBuilder("form");
                tagForm.Attributes["action"] = controller + method;
                tagForm.Attributes["method"] = "post";
                var inner = new StringBuilder();
                if (hiddenParams != null)
                {
                    foreach (var hiddenParam in hiddenParams)
                    {
                        var tag = new TagBuilder("input");
                        tag.Attributes["type"] = "hidden";
                        tag.Attributes["id"] = tag.Attributes["name"] = hiddenParam.Key;
                        tag.Attributes["value"] = hiddenParam.Value;
                        inner.Append(tag.ToString());
                    }
                }
                TagBuilder tagBut = new TagBuilder("input");
                tagBut.Attributes["type"] = "submit";
                tagBut.Attributes["value"] = textButton;
                inner.Append(tagBut);
                tagForm.InnerHtml = inner.ToString();
                result.Append(tagForm.ToString());
            }
            else
            {
                TagBuilder div = new TagBuilder("div");
                div.InnerHtml = textIfNotNeeded;
                result.Append(div.ToString());
            }
            return MvcHtmlString.Create(result.ToString());
        }
    }
}