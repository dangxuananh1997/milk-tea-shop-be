namespace API.MilkteaClient.Models
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
}