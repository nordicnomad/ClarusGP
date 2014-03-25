using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ClarusBlogBoard.Models
{
    public class SecurityMetaData
    {
        
        [UIHint("MultilineText")]
        [AllowHtml]
        public string SecurityContent1 { get; set; }

        [UIHint("MultilineText")]
        [AllowHtml]
        public string SecurityContent2 { get; set; }

        [UIHint("MultilineText")]
        [AllowHtml]
        public string SecurityContent3 { get; set; }

        [UIHint("MultilineText")]
        [AllowHtml]
        public string SecurityContent4 { get; set; }
    }

    [MetadataType(typeof(SecurityMetaData))]
    public partial class ContentSecurity
    { }
}