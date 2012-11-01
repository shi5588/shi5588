using System;
using System.Collections.Generic;

namespace EF_Dal.Models
{
    public class b_cst_item_relate
    {
        public System.Guid id { get; set; }
        public System.Guid b_item_id { get; set; }
        public System.Guid b_cst_item_id { get; set; }
        public decimal unit_rate { get; set; }
        public string remark { get; set; }
        public virtual b_cst_item b_cst_item { get; set; }
        public virtual b_item b_item { get; set; }
    }
}
