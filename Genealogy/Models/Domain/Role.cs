using System;
using System.Collections.Generic;

namespace Genealogy.Models
{
    public class Role
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<User> Users { get; set; }
        public Role()
        {
            Users = new List<User>();
        }
    }
}