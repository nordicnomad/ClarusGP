using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ClarusBlogBoard.Models
{
    public class ApplicationMetaData
    {
        [DataType("date")]
        public DateTime InterviewDate { get; set; }
    }

    [MetadataType(typeof(ApplicationMetaData))]
    public partial class Application
    { }
}