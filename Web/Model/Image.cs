using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Model
{
    public class Image
    {
        public int Id { get; set; }
        public String Url { get; set; }

        public Image() { }
        public Image(int Id, String url)
        {
            this.Id = Id;
            this.Url = Url;
        }
    }
}