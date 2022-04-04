using System;
using System.Collections.Generic;

namespace MarketOnlineWebsite.Models
{
    public partial class Supplier
    {
        public int SupplierId { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Companyname { get; set; }
        public string? Salt { get; set; }
        public string? ContactTitle { get; set; }
        public string? Addresss { get; set; }
        public string? PostalCode { get; set; }
        public string? Fax { get; set; }
        public string? PaymentMethods { get; set; }
        public int? LocationId { get; set; }
        public string? DiscountType { get; set; }
        public string? Notes { get; set; }
        public string? CurrentOrder { get; set; }
        public string? Logo { get; set; }
        public DateTime? LastLogin { get; set; }
        public DateTime? CreateDate { get; set; }
        public bool? Active { get; set; }

        public virtual Location? Location { get; set; }
    }
}
