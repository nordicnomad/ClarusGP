using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ClarusBlogBoard.Models
{
    public class ConsultingMetaData
    {
        
        [UIHint("MultilineText")]
        [AllowHtml]
        public string ConsultingContent1 { get; set; }

        [UIHint("MultilineText")]
        [AllowHtml]
        public string ConsultingContent2 { get; set; }

        [UIHint("MultilineText")]
        [AllowHtml]
        public string ConsultingContent3 { get; set; }

    }

    [MetadataType(typeof(ConsultingMetaData))]
    public partial class ContentConsulting
    { }
}