using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ClarusBlogBoard.Models
{
    public class ProjectMetaData
    {
        
        [UIHint("MultilineText")]
        [AllowHtml]
        public string ProjectContent1 { get; set; }

        [UIHint("MultilineText")]
        [AllowHtml]
        public string ProjectContent2 { get; set; }

        [UIHint("MultilineText")]
        [AllowHtml]
        public string ProjectContent3 { get; set; }

        [UIHint("MultilineText")]
        [AllowHtml]
        public string ProjectContent4 { get; set; }

        [UIHint("MultilineText")]
        [AllowHtml]
        public string ProjectContent5 { get; set; }
    }

    [MetadataType(typeof(ProjectMetaData))]
    public partial class ContentProject
    { }
}