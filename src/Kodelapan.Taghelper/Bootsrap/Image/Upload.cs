using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Kodelapan.TagHelpers.Bootsrap.Image
{
    [HtmlTargetElement("upload-photo")]
    public class Upload : TagHelper
    {
        [HtmlAttributeName("src")]
        public string Source { get; set; }

        [HtmlAttributeName("opt-class")]
        public string OptClass { get; set; }

        [HtmlAttributeName("alt")]
        public string Alt { get; set; }

        [HtmlAttributeName("file-input-id")]
        public string FileInputId { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            bool isNew = String.IsNullOrEmpty(Source) ? true : false;

            string content = $@"<div class='thumb'>
                <img src='{Source}' alt='{Alt}'>   
                      <input name='file' type='file' style='display:none'/>
                      <div class='overlay-shade'></div>
				<div class='icons-holder'>
					<div class='icons-holder-inner'>
						<div class='social-icons icon-dark icon-circled icon-theme-colored m-5'>
							<a href= 'javascript:'>
								<i class='fa fa-pencil' aria-hidden='true'></i>
							</a>
						</div>
						<span class='uploadtext'>Upload Profile Picture</span>
					</div>
				</div>";

            output.Content.AppendHtml(content);

        }
    }
}
