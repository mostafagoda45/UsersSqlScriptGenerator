using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestAPi
{
    public class User : IdentityUser
    {
        public DateTime Createdate { get; set; }
        public DateTime Updatedate { get; set; }
        public string Creator { get; set; }
        public string Updator { get; set; }
        public bool IsActive { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? RegisterDate { get; set; }
        public string Code { get; set; }
        public bool? IsForceResetPassword { get; set; }
        public bool LoggedIn { get; set; }
        public DateTime? LastLogIn { get; set; }
        public Guid? ExamId { get; set; }
    }
}
