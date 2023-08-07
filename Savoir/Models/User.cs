using System.ComponentModel.DataAnnotations;

namespace Savoir.Models.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public int OrganizationId { get; set; }
        public int CompanyId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Designation { get; set; }
    }
}
