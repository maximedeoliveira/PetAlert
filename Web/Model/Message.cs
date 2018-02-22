using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Models;

namespace Web.Model
{
    public class Message
    {
        public int Id { get; set; }
        public String Texte { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}