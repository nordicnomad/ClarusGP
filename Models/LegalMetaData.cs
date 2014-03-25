using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ClarusBlogBoard.Models
{
    public class LegalMetaData
    {
        
        [UIHint("MultilineText")]
        [AllowHtml]
        public string LegalContent1 { get; set; }

        [UIHint("MultilineText")]
        [AllowHtml]
        public string LegalContent2 { get; set; }

        [UIHint("MultilineText")]
        [AllowHtml]
        public string LegalContent3 { get; set; }

    }

    [MetadataType(typeof(LegalMetaData))]
    public partial class ContentLegal
    { }
}