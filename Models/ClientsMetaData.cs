using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ClarusBlogBoard.Models
{
    public class ClientsMetaData
    {
        
        [UIHint("MultilineText")]
        [AllowHtml]
        public string ClientsContent1 { get; set; }

        [UIHint("MultilineText")]
        [AllowHtml]
        public string ClientsContent2 { get; set; }

        [UIHint("MultilineText")]
        [AllowHtml]
        public string ClientsContent3 { get; set; }

        [UIHint("MultilineText")]
        [AllowHtml]
        public string ClientsContent4 { get; set; }

        [UIHint("MultilineText")]
        [AllowHtml]
        public string ClientsContent5 { get; set; }

        [UIHint("MultilineText")]
        [AllowHtml]
        public string ClientsContent6 { get; set; }

        [UIHint("MultilineText")]
        [AllowHtml]
        public string ClientsContent7 { get; set; }

        [UIHint("MultilineText")]
        [AllowHtml]
        public string ClientsContent8 { get; set; }

        [UIHint("MultilineText")]
        [AllowHtml]
        public string ClientsContent9 { get; set; }

        [UIHint("MultilineText")]
        [AllowHtml]
        public string ClientsContent10 { get; set; }
    }

    [MetadataType(typeof(ClientsMetaData))]
    public partial class ContentClients
    { }
}