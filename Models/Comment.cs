//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EasyManage.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Comment
    {
        public int CommentId { get; set; }
        public string Comments { get; set; }
        public System.DateTime ThisDateTime { get; set; }
        //public Nullable<int> OrderId { get; set; }
        //public Nullable<int> Rating { get; set; }

        public int OrderId { get; set; }

        public int? Rating { get; set; }



    }
}
