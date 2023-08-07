using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savoir.Models
{
   
    public class UserRole
    {
        [Key]
        public int UserId { get; set; }
        public string RoleType { get; set; }
        public int OrganizationId { get; set; }
        public int CompanyId { get; set; }
    }
}
