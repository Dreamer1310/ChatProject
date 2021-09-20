using System;
using System.Collections.Generic;

#nullable disable

namespace ChatPresentation.Models.DB
{
    public partial class User
    {
        public long Id { get; set; }
        public string GoogleId { get; set; }
        public string FacebookId { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Avatar { get; set; }
        public DateTime RegisteredOn { get; set; }
        public DateTime LastVisitedOn { get; set; }
        public string Password { get; set; }
    }
}
