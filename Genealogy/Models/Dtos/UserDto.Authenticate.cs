using System;

namespace Genealogy.Models
{
    public class AuthenticateUserDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

    }
}