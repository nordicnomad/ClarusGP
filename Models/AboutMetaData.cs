using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ClarusBlogBoard.Models
{
    public class AboutMetaData
    {
        
        [UIHint("MultilineText")]
        [AllowHtml]
        public string AboutContent1 { get; set; }

        [UIHint("MultilineText")]
        [AllowHtml]
        public string AboutContent2 { get; set; }

        [UIHint("MultilineText")]
        [AllowHtml]
        public string AboutContent3 { get; set; }

        [UIHint("MultilineText")]
        [AllowHtml]
        public string AboutContent4 { get; set; }

        [UIHint("MultilineText")]
        [AllowHtml]
        public string AboutContent5 { get; set; }
    }

    [MetadataType(typeof(AboutMetaData))]
    public partial class ContentAbout
    { }
}