using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebSampleApp
{
    public static class RequestAndResponse
    {
        public static string GetInfo(this HttpRequest request)
        {
            StringBuilder infoBuilder = new StringBuilder()
                .Append("scheme".Div(request.Scheme))
                .Append("host".Div(request.Host.Value ?? "no host"))
                .Append("path".Div(request.Path.Value ?? "no path"))
                .Append("query string".Div(request.QueryString.Value ?? "no query string"))
                .Append("method".Div(request.Method ?? "no method"))
                .Append("protocol".Div(request.Protocol ?? "no protocol"));

            return infoBuilder.ToString();
        }

        public static string GetHeaderInfo(this HttpRequest request)
        {
            StringBuilder infoBuilder = new();
            foreach (var header in request.Headers) 
            {
                infoBuilder.Append(header.Key.Div(string.Join(";", header.Value)));
            }
            return infoBuilder.ToString();
        }

        public static string QueryString(this HttpRequest request)
        {
            string xStr = request.Query["x"];
            string yStr = request.Query["y"];

            if (xStr== null || yStr == null)
            {
                return "x and y must be set".Div();
            }

            if (int.TryParse(xStr, out int x) == false)
            {
                return $"Error parsing {xStr}".Div();
            }

            if (int.TryParse(yStr, out int y) == false)
            {
                return $"Error parsing {yStr}".Div();
            }

            return $"{x}+{y}={x + y}".Div();
        }

        public static string GetContent(this HttpRequest request)
        {
            return (string?)request.Query["data"] ?? "no data";
        }

        public static string GetContentEncoded(this HttpRequest request)
        {
            return HtmlEncoder.Default.Encode(GetContent(request));
        }

        public static string GetForm(this HttpRequest request)
        {
            return request.Method switch
            {
                "GET" =>
                GetForm(),
                "POST" =>
                ShowForm(request),
                _ =>
                throw new NotImplementedException(),
            };
        }

        private static string ShowForm(HttpRequest request)
        {
            if (request.HasFormContentType)
            {
                StringBuilder formBuilder = new();
                foreach(var item in request.Form)
                {
                    formBuilder.Append(item.Key.Div(HtmlEncoder.Default.Encode(item.Value)));
                }
                return formBuilder.ToString();
            }
            return "no form".Div();
        }

        private static string GetForm()
        {
            StringBuilder formBuilder = new StringBuilder()
                .Append("<form method=\"post\" action=\"form\">")
                .Append("<input type=\"text\" name=\"text1\"/>")
                .Append("<input type=\"submit\" value=\"Submit\"/>")
                .Append("</form>");

            return formBuilder.ToString();
        }

        public static string WriteCookie(this HttpResponse response)
        {
            response.Cookies.Append("color", "red", new CookieOptions()
            {
                Path = "/cookies",
                Expires = DateTime.Now.AddDays(1)
            });
            return "cookie written".Div();
        }

        public static string ReadCookie(this HttpRequest request)
        {
            StringBuilder cookieBuilder = new();
            foreach (var cookie in request.Cookies)
            {
                cookieBuilder.Append(cookie.Key.Div(cookie.Value));
            }
            return cookieBuilder.ToString();
        }

        public static string GetJson(this HttpResponse response)
        {
            var b = new
            {
                Title = "Professional C# 7",
                Publisher = "Wrox Press",
                Author = "Christian Nagel"
            };
            string json = JsonSerializer.Serialize(b, b.GetType());
            response.ContentType = "application/json";
            return json;
        }

    }
}
