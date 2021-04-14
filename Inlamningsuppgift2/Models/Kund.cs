using System;
using System.Collections.Generic;

#nullable disable
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace Inlamningsuppgift2.Models
{
    public partial class Kund
    {
        public Kund()
        {
            Bestallnings = new HashSet<Bestallning>();
        }

        public int KundId { get; set; }


        [Required(ErrorMessage = "Namn är obligatorisk")]
        public string Namn { get; set; }


        [Required(ErrorMessage = "Gatuadress är obligatorisk")]
        public string Gatuadress { get; set; }


        [DisplayName("Postnummer")]
        [Required(ErrorMessage = "Postnummer är obligatorisk")]
        [Range(11111,99999, ErrorMessage ="Postnummer ska vara 5 sifra")]
        public string Postnr { get; set; }


        [Required(ErrorMessage = "Postort är obligatorisk")]
        public string Postort { get; set; }


        [Required(ErrorMessage = "Email är obligatorisk")]
        [EmailAddress(ErrorMessage = "Felaktig emailadress")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Telefon är obligatorisk")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Telefon får inehålla bara siffror dvs specialtecken")]
        public string Telefon { get; set; }


        [DisplayName("Användarnamn")]
        [Required(ErrorMessage = "Användarnamn är obligatorisk")]
        public string AnvandarNamn { get; set; }


        [DisplayName("lösenord")]
        [Required(ErrorMessage = "Lösenord är obligatorisk")]
        public string Losenord { get; set; }


        public virtual ICollection<Bestallning> Bestallnings { get; set; }
    }
}
