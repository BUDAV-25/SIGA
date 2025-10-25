using Microsoft.AspNetCore.Identity;

namespace SIGA.Identity.Shared.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public char Gender { get; set; }
        public string? Foto { get; set; }
        public string State { get; set; }
        public DateTime DateOfEntry { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastLogin { get; set; }
    }
}
