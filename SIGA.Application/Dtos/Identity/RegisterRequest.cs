using System.ComponentModel.DataAnnotations;

namespace SIGA.Application.Dtos.Identity
{
    public class RegisterRequest
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public char Gender { get; set; }
        public string Email { get; set; }
        public string? Role { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string? Phone { get; set; }
        public string State { get; set; }
        public DateTime DateOfEntry { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? LastLogin { get; set; }
        public string? Foto { get; set; }

    }
}
