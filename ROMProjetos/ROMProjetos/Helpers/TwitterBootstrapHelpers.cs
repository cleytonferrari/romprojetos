using System.Text;
using System.Web.Mvc;

namespace ROMProjetos.Helpers
{
    public enum MessageType
    {
        Error,
        Success,
        Info
    }

    public static class TwitterBootstrapHelpers
    {
        public static MvcHtmlString Message(this HtmlHelper html, MessageType type, dynamic text)
        {
            return CreateMessage(type, text);
        }

        private static MvcHtmlString CreateMessage(MessageType type, string text)
        {
            if (string.IsNullOrEmpty(text)) return new MvcHtmlString(string.Empty);

            var message = new StringBuilder();
            message.Append(string.Format("<div class=\"alert alert-{0}\">{1}</div>", type.ToString().ToLower(), text));
            return new MvcHtmlString(message.ToString());
        }
    }
}