using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ClarusBlogBoard.Models
{
    public class JobPostMetaData
    {

        [UIHint("MultilineText")]
        [AllowHtml]
        public string JobDescription { get; set; }

        [DataType(DataType.Date)]
        public string DatePosted { get; set; }

        [DataType(DataType.Date)]
        public string ClosingDate { get; set; }

    }

    [MetadataType(typeof(JobPostMetaData))]
    public partial class JobPost
    { }
}