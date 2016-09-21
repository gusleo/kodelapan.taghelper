using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Kodelapan.BootstrapTagHelper
{
    [HtmlTargetElement("span", Attributes = ForAttributeName)]
    public class DisplayForTagHelper : TagHelper
    {
        private const string ForAttributeName = "item-for";
        private const string ForDisplayFormat = "display-format";
        private const string ForCultureInfo = "culture-info";
        private const string ForDecimalSeparator = "decimal-separator";
        private const string ForGroupSeparator = "group-separator";
        private const string ForSymbol = "symbol";
        private const string ForDecimalDigit = "decimal-digit";

        private string _cultureInfo;
        private string _symbol;
        private string _decimalSeparator;
        private string _groupSeparator;
        private int _decimalDigit;

        [HtmlAttributeName(ForAttributeName)]
        public ModelExpression For { get; set; }

        [HtmlAttributeName(ForDisplayFormat)]
        public string DisplayFormat { get; set; }

        [HtmlAttributeName(ForCultureInfo)]
        public string CultureInfo {
            get
            {
                return String.IsNullOrEmpty(_cultureInfo) ? "en-GB" : _cultureInfo;
            }
            set
            {
                _cultureInfo = value;
            }
        }
        [HtmlAttributeName(ForDecimalSeparator)]
        public string DecimalSeparator
        {
            get
            {
                return String.IsNullOrEmpty(_decimalSeparator) ? "." : _decimalSeparator;
            }
            set
            {
                _decimalSeparator = value;
            }
        }
        [HtmlAttributeName(ForGroupSeparator)]
        public string GroupSeparator
        {
            get
            {
                return String.IsNullOrEmpty(_groupSeparator) ? "," : _groupSeparator;
            }
            set
            {
                _groupSeparator = value;
            }
        }
        [HtmlAttributeName(ForSymbol)]
        public string Symbol
        {
            get
            {
                return String.IsNullOrEmpty(_symbol) ? "$" : _symbol;
            }
            set
            {
                _symbol = value;
            }
        }
        [HtmlAttributeName(ForDecimalDigit)]
        public int DecimalDigit
        {
            get
            {
                return _decimalDigit;
            }
            set
            {
                _decimalDigit = value;
            }

        }

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
            if ( !String.IsNullOrEmpty(DisplayFormat) )
            {
                string _text = "";
                Type type = For.ModelExplorer.ModelType;
                if ( type == typeof(System.DateTime) )
                {
                    _text = ((DateTime)For.ModelExplorer.Model).ToString(DisplayFormat);
                   
                }
                else if (type == typeof(System.Decimal)){
                    var cultureInfo = new CultureInfo(CultureInfo);
                    var numberFormatInfo = (NumberFormatInfo)cultureInfo.NumberFormat.Clone();
                    numberFormatInfo.CurrencySymbol = Symbol;
                    numberFormatInfo.CurrencyDecimalSeparator = DecimalSeparator;
                    numberFormatInfo.CurrencyDecimalDigits = DecimalDigit;
                    numberFormatInfo.CurrencyGroupSeparator = GroupSeparator;
                    numberFormatInfo.NumberDecimalSeparator = DecimalSeparator;
                    numberFormatInfo.NumberGroupSeparator = GroupSeparator;
                    numberFormatInfo.PercentDecimalSeparator = DecimalSeparator;
                    numberFormatInfo.PercentGroupSeparator = GroupSeparator;
                    _text = ((Decimal)For.ModelExplorer.Model).ToString(DisplayFormat, numberFormatInfo);
                }
                else
                {
                    _text = For.ModelExplorer.GetSimpleDisplayText();
                   
                }
                output.Content.SetContent(_text);

            }
            else
            {
                var text = For.ModelExplorer.GetSimpleDisplayText();
                output.Content.SetContent(text);
            }
           
            
        }
    }
}
