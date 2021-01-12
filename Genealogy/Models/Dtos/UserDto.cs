using System;

namespace Genealogy.Models
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string StartDate { get; set; }
        public string FinishDate { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
    }
}