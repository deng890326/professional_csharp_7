using Markdig;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace TagHelperSamles
{
    [HtmlTargetElement("markdown", TagStructure = TagStructure.NormalOrSelfClosing)]
    [HtmlTargetElement(Attributes = "markdownfile")]
    public class MarkdownTagHelper : TagHelper
    {
        public MarkdownTagHelper(IWebHostEnvironment env) => _env = env;

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            string markdown = "";
            if (MarkdownFile != null)
            {
                string fileName = Path.Combine(_env.WebRootPath, MarkdownFile);
                if (File.Exists(fileName))
                {
                    markdown = File.ReadAllText(fileName);
                }
            }
            else
            {
                markdown = (await output.GetChildContentAsync()).GetContent();
            }
            output.Content.SetHtmlContent(Markdown.ToHtml(markdown));
        }

        [HtmlAttributeName]
        public string? MarkdownFile { get; set; }

        private readonly IWebHostEnvironment _env;
    }
}