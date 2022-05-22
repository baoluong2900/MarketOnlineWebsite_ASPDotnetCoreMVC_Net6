using System;
using System.Collections.Generic;

namespace MarketOnlineWebsite.Models
{
    public partial class Page
    {
        public int PageId { get; set; }
        public string? PageName { get; set; }
        public string? Contents { get; set; }
        public string? Thumb { get; set; }
        public bool Published { get; set; }
        public string? Title { get; set; }
        public string? Alias { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? Ordering { get; set; }
        public bool? Active { get; set; }
    }
}
