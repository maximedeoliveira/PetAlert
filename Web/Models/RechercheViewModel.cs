using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Model;

namespace Web.Models
{
    public class RechercheViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nom de l'animal")]
        public String Nom { get; set; }

        [Required]
        [Display(Name = "Description de l'animal")]
        public String Description { get; set; }

        [Required]
        [Display(Name = "Sexe de l'animal")]
        public String Sexe { get; set; }

        [Required]
        [Display(Name = "Race de l'animal")]
        public String Race { get; set; }

        [Required]
        [Display(Name = "Date de la disparition")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy}")]
        public DateTime Date_recherche { get; set; }

        [Required]
        [Display(Name = "Lieu de la disparition")]
        public String Adresse { get; set; }

        [Display(Name ="Désactiver l'annonce")]
        public Boolean Desactiver { get; set; }

        [Display(Name ="Animal retrouvé")]
        public Boolean Trouver { get; set; }

        [Required]
        [Display(Name = "Catégorie de l'animal")]
        public int SelectedTypeAnimaux { get; set; }
        public IEnumerable<SelectListItem> ListTypeAnimaux { get; set; }

        [Display(Name = "Image")]
        public HttpPostedFileBase Image { get; set; }
    } 
}