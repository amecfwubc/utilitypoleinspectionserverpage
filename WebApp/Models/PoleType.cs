//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PoleType
    {
        public PoleType()
        {
            this.PoleInfoes = new HashSet<PoleInfo>();
            this.PoleInfoChangeApplies = new HashSet<PoleInfoChangeApply>();
        }
    
        public int ID { get; set; }
        public string TypeName { get; set; }
        public Nullable<int> UserID { get; set; }
    
        public virtual ICollection<PoleInfo> PoleInfoes { get; set; }
        public virtual UserInformation UserInformation { get; set; }
        public virtual ICollection<PoleInfoChangeApply> PoleInfoChangeApplies { get; set; }
    }
}
