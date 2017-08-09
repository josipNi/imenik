using Imenik_JN.Server.Data.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Imenik_JN.Server.Entities
{
    public class Tag : EntityAudit, IEntityBase
    {
        public int Id { get; set; }

        [MaxLength(255)]
        [Required]
        public string TagName { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}
