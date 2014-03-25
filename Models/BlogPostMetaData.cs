using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ClarusBlogBoard.Models
{
    public class BlogPostMetaData
    {

        [UIHint("MultilineText")]
        [AllowHtml]
        public string BlogArticle { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public string DatePublished { get; set; }

    }

    [MetadataType(typeof(BlogPostMetaData))]
    public partial class BlogPost
    { }
}