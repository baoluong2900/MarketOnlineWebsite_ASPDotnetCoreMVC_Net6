using System;
using System.Collections.Generic;

namespace MarketOnlineWebsite.Models
{
    public partial class Location
    {
        public Location()
        {
            Customers = new HashSet<Customer>();
            Suppliers = new HashSet<Supplier>();
        }

        public int LocationId { get; set; }
        public string? Name { get; set; }
        public int? Parent { get; set; }
        public int? Levels { get; set; }
        public string? Slug { get; set; }
        public string? NameWithType { get; set; }
        public int? Type { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<Supplier> Suppliers { get; set; }
    }
}
