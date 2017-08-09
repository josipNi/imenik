using Imenik_JN.Server.Data.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Imenik_JN.Server.Entities
{
    public class Email: EntityAudit, IEntityBase
    {
        public int Id { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Required]
        public string EmailAddress { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}
