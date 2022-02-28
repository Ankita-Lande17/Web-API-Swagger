using System;
using System.Collections.Generic;

namespace NS.EMS.API1.Entities
{
    public partial class Login
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool? IsActive { get; set; }
        public string Role { get; set; }
    }
}
