using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kodelapan.BootstrapTagHelper
{
    [HtmlTargetElement("span", Attributes = ForAttributeName)]
    public class DisplayDateTimeTagHelper : TagHelper
    {
        private const string ForAttributeName = "asp-for";
        private const string DisplayFormat = "display-format";

        [HtmlAttributeName(ForAttributeName)]
        public ModelExpression For { get; set; }

        [HtmlAttributeName(DisplayFormat)]
        public string ForDisplayFormat { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if ( context == null )
            {
                throw new ArgumentNullException(nameof(context));
            }

            if ( output == null )
            {
                throw new ArgumentNullException(nameof(output));
            }
            if ( !String.IsNullOrEmpty(ForDisplayFormat) )
            {
                if ( For.ModelExplorer.ModelType == typeof(System.DateTime) )
                {
                    string _text = ((DateTime)For.ModelExplorer.Model).ToString(ForDisplayFormat);
                    output.Content.SetContent(_text);
                }
                else
                {
                    var text = For.ModelExplorer.GetSimpleDisplayText();
                    output.Content.SetContent(text);
                }
            }
            else
            {
                var text = For.ModelExplorer.GetSimpleDisplayText();
                output.Content.SetContent(text);
            }
           
            
        }
    }
}
