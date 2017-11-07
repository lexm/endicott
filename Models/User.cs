using System;
using System.ComponentModel.DataAnnotations;

namespace endicott.Models
{
    public abstract class BaseEntity
    {
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
    public class User : BaseEntity
    {
        public int userid { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Description { get; set; }
    }
}