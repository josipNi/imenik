using Imenik_JN.Server.Data.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Imenik_JN.Server.Entities
{
    public class User : EntityAudit, IEntityBase
    {
        public User()
        {
            TagCollection = new HashSet<Tag>();
            EmailCollection = new HashSet<Email>();
            PhoneCollection = new HashSet<Phone>();
        }

        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Address { get; set; }

        public virtual ICollection<Tag> TagCollection { get; set; }
        public virtual ICollection<Email> EmailCollection { get; set; }
        public virtual ICollection<Phone> PhoneCollection { get; set; }
    }
}
