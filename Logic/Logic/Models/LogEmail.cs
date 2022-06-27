using System;
using System.Collections.Generic;

namespace Logic.Models
{
    public partial class LogEmail
    {
        public int Id { get; set; }
        public int? IdMember { get; set; }
        public string? Message { get; set; }
        public DateTime? DateSent { get; set; }
    }
}
