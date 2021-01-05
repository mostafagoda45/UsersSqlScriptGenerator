using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestAPi
{
    public class Context : IdentityDbContext<User, Role, string, IdentityUserClaim<string>,
    UserRole, IdentityUserLogin<string>,
    IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public Context(DbContextOptions<Context> options)
            : base(options)
        {

        }
    }
}
