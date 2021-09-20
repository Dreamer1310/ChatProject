using System;
using System.Collections.Generic;

#nullable disable

namespace ChatPresentation.Models.DB
{
    public partial class UserRole
    {
        public long Id { get; set; }
        public string RoleName { get; set; }
    }
}
