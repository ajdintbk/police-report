using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace police_alert.Domain
{
    public class Report
    {
        public int Id { get; set; }
        public IdentityUser User { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
    }
}
