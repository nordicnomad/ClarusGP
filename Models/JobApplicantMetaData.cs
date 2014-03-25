using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ClarusBlogBoard.Models
{
    public class JobApplicantMetaData
    {
        [Required]
        public string ApplicantFirstName { get; set; }

        [Required]
        public string ApplicantLastName { get; set; }

        [Required]
        public string ApplicantEmail { get; set; }

        [Required]
        public string ApplicantCity { get; set; }

        [Required]
        public int ApplicantStateID { get; set; }

        [UIHint("MultilineText")]
        [AllowHtml]
        public string ResumeText { get; set; }
    }

    [MetadataType(typeof(JobApplicantMetaData))]
    public partial class JobApplicant
    { }
}