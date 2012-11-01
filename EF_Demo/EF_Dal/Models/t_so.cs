using System;
using System.Collections.Generic;

namespace EF_Dal.Models
{
    public class t_so
    {
        public t_so()
        {
            this.t_so_dtl = new List<t_so_dtl>();
        }

        public System.Guid id { get; set; }
        public string so_no { get; set; }
        public Nullable<System.DateTime> so_date { get; set; }
        public Nullable<System.Guid> b_custom_id { get; set; }
        public Nullable<decimal> tax_rate { get; set; }
        public string remark { get; set; }
        public System.DateTime last_time { get; set; }
        public string last_user { get; set; }
        public virtual b_customer b_customer { get; set; }
        public virtual ICollection<t_so_dtl> t_so_dtl { get; set; }
    }
}
