using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Models;

namespace Web.Model
{
    public class Recherche
    {
        public int Id { get; set; }
        public virtual TypeAnimaux Type{ get; set; }
        public String Nom { get; set; }
        public String Description { get; set; }
        public String Sexe { get; set; }
        public String Race { get; set; }
        public String Adresse { get; set; }
        public Boolean Desactiver { get; set; }
        public Boolean Trouver { get; set; }
        public DateTime Date_recherche { get; set; }
        public DateTime Date_ajout { get; set; }
        public virtual List<Image> Images { get; set; }
        public virtual List<Message> Messages { get; set; }
        public virtual ApplicationUser User { get; set; }

        public Recherche()
        {
            this.Images = new List<Image>();
            this.Messages = new List<Message>();
        }
    }
}