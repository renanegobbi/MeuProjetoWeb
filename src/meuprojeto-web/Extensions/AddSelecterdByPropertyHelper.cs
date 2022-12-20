using Microsoft.AspNetCore.Razor.TagHelpers;
using System;

namespace meuprojeto_web.Extensions
{
    [HtmlTargetElement("*", Attributes = "value-option")]
    public class AddSelecterdByPropertyTagHelper : TagHelper
    {

        [HtmlAttributeName("value-option")]
        public string ValueOption { get; set; }

        [HtmlAttributeName("value-property")]
        public string ValueProperty { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            if (ValueOption != ValueProperty) return;

            if (ValueOption == ValueProperty) {

                output.SuppressOutput();

            }
        }
    }
}