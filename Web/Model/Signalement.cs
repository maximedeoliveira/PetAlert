using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Models;

namespace Web.Model
{
    public class Signalement
    {
        public int Id { get; set; }
        public virtual TypeAnimaux Type{ get; set; }
        public String Description { get; set; }
        public String Adresse { get; set; }
        public Boolean Desactiver { get; set; }
        public DateTime Date_ajout { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}