using System;

namespace CMS_appBackend.Contracts
{
    public interface ISoftDelete
    {
        DateTime? DeletedOn { get; set; }
        int? DeletedBy { get; set; }
        bool ? IsDeleted { get; set; }  
    }
}
