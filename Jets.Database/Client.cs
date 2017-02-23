using System;
using System.ComponentModel.DataAnnotations;

namespace Jets.Database
{
    public class Client
    {
        [Key]
        public Guid Id { get; set; }

        [Required, MinLength(5), MaxLength(16)]        
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public Guid AccessToken { get; set; }

        public DateTime TokenExpire { get; set; }
    }
}
