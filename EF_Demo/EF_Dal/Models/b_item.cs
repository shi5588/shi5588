using System;
using System.Collections.Generic;

namespace EF_Dal.Models
{
    public class b_item
    {
        public b_item()
        {
            this.b_cst_item_relate = new List<b_cst_item_relate>();
            this.t_so_dtl = new List<t_so_dtl>();
        }

        public System.Guid id { get; set; }
        public string item_no { get; set; }
        public string item_name { get; set; }
        public string units { get; set; }
        public Nullable<decimal> min_stock { get; set; }
        public string remark { get; set; }
        public virtual ICollection<b_cst_item_relate> b_cst_item_relate { get; set; }
        public virtual ICollection<t_so_dtl> t_so_dtl { get; set; }
    }
}
