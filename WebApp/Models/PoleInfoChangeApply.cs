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
    
    public partial class PoleInfoChangeApply
    {
        public int ID { get; set; }
        public string PoleID { get; set; }
        public Nullable<int> TypeID { get; set; }
        public Nullable<System.DateTime> TaskAddeddate { get; set; }
        public Nullable<System.DateTime> TaskPerformeddate { get; set; }
        public Nullable<int> TaskAssainUserID { get; set; }
        public string ImageMapPath { get; set; }
        public Nullable<double> AdjacentPoleHeight { get; set; }
        public string TransFormerLoading { get; set; }
        public string Notes { get; set; }
        public string ImagesTakenpath { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<bool> IsChanged { get; set; }
        public Nullable<System.DateTime> TaskCompleteDate { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
    
        public virtual PoleType PoleType { get; set; }
        public virtual UserInformation UserInformation { get; set; }
        public virtual UserInformation UserInformation1 { get; set; }
    }
}