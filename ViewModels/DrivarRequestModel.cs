using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace divar.Models
{
    public class DivarRequest
    {
        public ExtraData extra_data { get; set; }
        public string callback_url { get; set; }
        public string post_token { get; set; }
        public string user_id { get; set; }
        public string peer_id { get; set; }
        public Supplier supplier { get; set; }
        public Demand demand { get; set; }
    }

    public class ExtraData
    {
        public Location location { get; set; }
    }

    public class Location
    {
        public string latitude { get; set; }
        public string longitude { get; set; }
    }

    public class Supplier
    {
        public string id { get; set; }
    }

    public class Demand
    {
        public string id { get; set; }
    }

}