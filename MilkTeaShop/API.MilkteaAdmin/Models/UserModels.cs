using Core.ObjectModel.ConstantManager;
using System.ComponentModel.DataAnnotations;

namespace API.MilkteaAdmin.Models
{
    public class UserVM
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Avatar { get; set; }
    }

    public class UserCM
    {
        [Required]
        [Phone]
        public string Username { get; set; }

        [Required]
        [StringLength(192, MinimumLength = 5)]
        public string FullName { get; set; }
    }

    public class UserUM
    {
        [Required]
        [RegularExpression(@"^(\d{1,5})\b", ErrorMessage = ErrorMessage.INVALID_ID)]
        public int Id { get; set; }
        //public string Username { get; set; }

        [StringLength(192, MinimumLength = 5)]
        public string FullName { get; set; }

        [StringLength(192, MinimumLength = 5)]
        public string Address { get; set; }

        [Phone]
        public string Phone { get; set; }

        public string Avatar { get; set; }
    }
}