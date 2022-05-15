using System;
using System.Collections.Generic;

namespace MarketOnlineWebsite.Models
{
    public partial class Advertisement
    {
        public int AdvertisementsId { get; set; }
        public string? SubTitle { get; set; }
        public string? Title { get; set; }
        public string? ImageBg { get; set; }
        public string? ImageProduct { get; set; }
        public string? UrlLink { get; set; }
        public bool Active { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
