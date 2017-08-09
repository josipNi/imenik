using System;

namespace Imenik_JN.Server.Data.Interfaces
{
    public interface IAuditableEntity
    {
        //string CreatedBy { get; set; }

        DateTime Created { get; set; }

        //string ModifiedBy { get; set; }

        DateTime Modified { get; set; }
    }
}
