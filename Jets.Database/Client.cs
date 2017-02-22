using System;
using System.ComponentModel.DataAnnotations;

namespace Jets.Database
{
    public class Client
    {
        [Key]
        public Guid Id { get; set; }

        [Required, MinLength(5), MaxLength(100)]        
        public string Username { get; set; }

        [Required, MinLength(7), MaxLength(16)]
        public string Password { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public Guid AccessToken { get; set; }

        public DateTime TokenExpire { get; set; }
    }
}
