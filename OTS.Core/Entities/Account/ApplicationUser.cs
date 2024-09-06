using OTS.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using OTS.Common.Helpers;

namespace OTS.Core.Entities.Account
{
    public class ApplicationUser : IdentityUser<string>, ISoftDeletable
    {
        [Required]
        [StringLength(50)]
        public string? FirstName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(50)]
        public string? LastName { get; set; }

        public bool IsDeleted { get; set; }
       public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }

    }
}

