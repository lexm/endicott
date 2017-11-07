using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace endicott.Models
{
    public abstract class BaseEntity
    {
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
    public class User : BaseEntity
    {
        public User()
        {
            Connectors = new List<Connect>();
            Connectees = new List<Connect>();
        }
        public int userid { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Description { get; set; }
        [InverseProperty("Connector")]
        public List<Connect> Connectors { get; set; }
        [InverseProperty("Connectee")]
        public List<Connect> Connectees { get; set; }
    }
}