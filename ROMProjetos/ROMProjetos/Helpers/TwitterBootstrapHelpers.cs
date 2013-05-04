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
            message.Append(string.Format("<div class=\"alert alert-{0}\" style=\"margin:10px 0px\">{1}</div>", type.ToString().ToLower(), text));
            return new MvcHtmlString(message.ToString());
        }


        public static MvcHtmlString PrioridadeDaTarefa(this HtmlHelper html, string prioridade)
        {
            var icone = new StringBuilder();

            var classe = "";
            switch (prioridade)
            {
                case "Baixa":
                    classe = "icon-arrow-down green";
                    break;
                case "Normal":
                    classe = "icon-exchange lightblue";
                    break;
                case "Alta":
                    classe = "icon-arrow-up red";
                    break;
                case "Critica":
                    classe = "icon-warning-sign orange";
                    break;
                case "Bloqueante":
                    classe = "icon-ban-circle red";
                    break;
                default:
                    classe = "";
                    break;
            }
            icone.Append(string.Format("<i class=\"{0}\" title=\"{1}\"></i>", classe, prioridade));
            return new MvcHtmlString(icone.ToString());
        }
    }
}