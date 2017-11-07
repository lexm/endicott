using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace endicott.Models
{
    public class ProfileViewModel : BaseEntity
    {
        public User user { get; set; }
        public List<User> friends { get; set; }
        public List<Connect> invites { get; set; }
    }
}