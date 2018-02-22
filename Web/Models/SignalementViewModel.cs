using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Models
{
    public class SignalementViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Description de l'animal")]
        public String Description { get; set; }

        [Required]
        [Display(Name = "Lieu de la disparition")]
        public String Adresse { get; set; }

        [Display(Name = "Désactiver l'annonce")]
        public Boolean Desactiver { get; set; }

        [Required]
        [Display(Name = "Catégorie de l'animal")]
        public int SelectedTypeAnimaux { get; set; }
        public IEnumerable<SelectListItem> ListTypeAnimaux { get; set; }
        
    }
}