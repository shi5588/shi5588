using System;
using System.Collections.Generic;

namespace EF_Dal.Models
{
    public class t_so_dtl
    {
        public System.Guid id { get; set; }
        public System.Guid t_so_id { get; set; }
        public System.Guid b_item_id { get; set; }
        public decimal qty { get; set; }
        public Nullable<System.DateTime> last_time { get; set; }
        public string last_user { get; set; }
        public Nullable<System.Guid> b_cst_item_id { get; set; }
        public Nullable<decimal> cst_qty { get; set; }
        public virtual b_cst_item b_cst_item { get; set; }
        public virtual b_item b_item { get; set; }
        public virtual t_so t_so { get; set; }
    }
}
