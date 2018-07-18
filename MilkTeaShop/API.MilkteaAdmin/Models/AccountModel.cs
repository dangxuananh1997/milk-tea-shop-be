namespace API.MilkteaAdmin.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;

    [DataContract(Name = "Account")]
    public class AccountModel
    {
        [DataMember, StringLength(255, MinimumLength = 2)]
        public string Username { get; set; }

        [DataMember, StringLength(255, MinimumLength = 2)]
        public string Password { get; set; }

        [DataMember]
        [Compare("OldPassword")]
        public string NewPassword { get; set; }

        [DataMember]
        public string OldPassword { get; set; }
    }

    public class RegisterModel
    {
        [Required]
        //[Display(Name = "Email")]
        public string Username { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}