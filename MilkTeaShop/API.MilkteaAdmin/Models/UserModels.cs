namespace API.MilkteaAdmin.Models
{
    public class UserVM
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
    }

    public class UserCM
    {
        public string Username { get; set; }
        public string FullName { get; set; }
    }

    public class UserUM
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
    }
}