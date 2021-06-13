using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medicaly.Models
{
    public class SupportForm
    {
        public string Nama { get; set; }
        public string Email { get; set; }
        public string Topic { get; set; }
        public string Detail { get; set; }
        public string FilePendukung { get; set; }
        public HttpPostedFileBase ImageUpload { get; set; }
    }
}