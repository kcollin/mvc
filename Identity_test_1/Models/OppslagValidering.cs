using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Identity_test_1.Models
{
    public class OppslagValidering
    {

        [Required(ErrorMessage = "Tittel må angis")]
        public String tittel { get; set; }

        [Required(ErrorMessage = "Ingress må angis")]
        public String ingress  { get; set; }

        [Required]
        public int kategoriID { get; set; }
    }

    [MetadataType(typeof(OppslagValidering))]
    public partial class Oppslag
    {

    }

}