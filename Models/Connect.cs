using System;
using System.ComponentModel.DataAnnotations;

namespace endicott.Models
{
    public class Connect : BaseEntity
    {
        public int connectid { get; set; }
        public int ConnectorId { get; set; }
        public User Connector { get; set; }
        public int ConnecteeId { get; set; }
        public User Connectee { get; set; }
        public bool Accepted { get; set; } 
    }
}