using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contracts.Models
{
    public class AuctionUpdated
    {
        public string Id { get; set; } = string.Empty;
        public string? ImageUrl { get; set; } 
        public string? Title { get; set; } 
        public string? Description { get; set; } 
        public string? Tags { get; set; } 
        public int? ReservePrice { get; set; } = 0;
        public DateTime? EndDate { get; set; }
    }
}