using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace TagHelperSamles
{
    [HtmlTargetElement("table", Attributes = $"{items}, {editPage}")]
    public class TableTagHelper : TagHelper
    {
        private const string items = "items";
        private const string editPage = "edit-page";

        [HtmlAttributeName(items)]
        public IEnumerable<object> Items { get; set; }

        [HtmlAttributeName(editPage)]
        public string? EditPage { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            TagBuilder table = new("table");
            table.GenerateId(context.UniqueId, "id");
            Dictionary<string, object> attrs = context.AllAttributes.Where(a => a.Name != items)
                .ToDictionary(a => a.Name, a => a.Value);
            table.MergeAttributes(attrs);

            if (Items.Any())
            {
                object first = Items.First();
                var properties = first.GetType().GetProperties();
                var header = new TagBuilder("tr");
                foreach (var property in properties)
                {
                    var th = new TagBuilder("th");
                    th.InnerHtml.Append(property.Name);
                    header.InnerHtml.AppendHtml(th);
                }
                table.InnerHtml.AppendHtml(header);
                foreach (var obj in Items)
                {
                    var item = new TagBuilder("tr");
                    foreach (var property in properties)
                    {
                        var td = new TagBuilder("td");
                        td.InnerHtml.Append(property.GetValue(obj)?.ToString() ?? "");
                        item.InnerHtml.AppendHtml(td);
                    }
                    if (EditPage != null && EditPage != "")
                    {
                        var td = new TagBuilder("td");

                    }
                    table.InnerHtml.AppendHtml(item);
                }
            }

            output.Content.AppendHtml(table.InnerHtml);
        }
    }
}
