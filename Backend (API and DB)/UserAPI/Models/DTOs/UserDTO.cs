namespace UserAPI.Models.DTOs
{
    public class UserDTO
    {
        public int UserID { get; set; }
        public string? Password { get; set; }
        public string? NewPassword { get; set; }    
        public string? Role { get; set; }
        public string? Token { get; set; }
    }
}
