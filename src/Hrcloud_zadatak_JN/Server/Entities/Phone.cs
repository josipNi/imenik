using Imenik_JN.Server.Data.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Imenik_JN.Server.Entities
{
    public class Phone: EntityAudit, IEntityBase
    {
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"^(\+\d{9,15})+$")] // matches phone number in format +9..15 characters.
        public string PhoneNumber { get; set; }

        [Required]
        public int UserId  { get; set; }
    }
}
