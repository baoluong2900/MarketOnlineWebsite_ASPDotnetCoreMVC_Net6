using System;
using System.Collections.Generic;

namespace MarketOnlineWebsite.Models
{
    public partial class Shipper
    {
        public int ShipperId { get; set; }
        public int? SupplierId { get; set; }
        public string? ShipperName { get; set; }
        public string? Phone { get; set; }
        public DateTime? Birthday { get; set; }
        public string? Email { get; set; }
        public int? LocationId { get; set; }
        public int? District { get; set; }
        public int? Ward { get; set; }
        public string? Avatar { get; set; }
        public DateTime? ShipDate { get; set; }
        public bool? Active { get; set; }
    }
}
