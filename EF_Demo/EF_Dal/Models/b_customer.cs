using System;
using System.Collections.Generic;

namespace EF_Dal.Models
{
    public class b_customer
    {
        public b_customer()
        {
            this.t_so = new List<t_so>();
        }

        public System.Guid id { get; set; }
        public string custom_no { get; set; }
        public string custom_name { get; set; }
        public string addr { get; set; }
        public string remark { get; set; }
        public virtual ICollection<t_so> t_so { get; set; }
    }
}
