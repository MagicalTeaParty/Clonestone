using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ClonestoneMVC.Models
{
    public class ProfileMetadata
    {

        [Required(ErrorMessage = "required!", AllowEmptyStrings = false)]
        [DataType(DataType.EmailAddress, ErrorMessage = "invalid ermailadress")]
        [DisplayName("Newemail1")]
        public string Newemail1 { get; set; }

        [Required(ErrorMessage = "required!", AllowEmptyStrings = false)]
        [DataType(DataType.EmailAddress, ErrorMessage = "invalid ermailadress")]
        [System.ComponentModel.DataAnnotations.Compare("Newemail1", ErrorMessage = "email not identical!")]
        [DisplayName("Newemail2")]
        public string Newemail2 { get; set; }

        [StringLength(maximumLength: 20, MinimumLength = 4, ErrorMessage = "password requirements not met (4-20 digits)")]
        [Required(ErrorMessage = "required!", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [DisplayName("Newpass1")]
        public string Newpass1 { get; set; }

        [Required(ErrorMessage = "required!", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Newpass1", ErrorMessage = "passwords not identical!")]
        [DisplayName("Newpass2")]
        public string Newpass2 { get; set; }

    }
}