using System;
using System.Collections.Generic;

namespace MarketOnlineWebsite.Models
{
    public partial class Supplier
    {
        public Supplier()
        {
            Products = new HashSet<Product>();
        }

        public int SupplierId { get; set; }
        public int? AccountId { get; set; }
        public string? EmailContact { get; set; }
        public string? Companyname { get; set; }
        public string? ContactTitle { get; set; }
        public string? Addresss { get; set; }
        public string? PostalCode { get; set; }
        public string? Fax { get; set; }
        public string? PaymentMethods { get; set; }
        public int? LocationId { get; set; }
        public int? District { get; set; }
        public int? Ward { get; set; }
        public string? DiscountType { get; set; }
        public string? Notes { get; set; }
        public string? CurrentOrder { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? Logo { get; set; }
        public bool? Active { get; set; }

        public virtual Account? Account { get; set; }
        public virtual Location? Location { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
