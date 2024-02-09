using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuctionService.Models;
using Microsoft.AspNetCore.Mvc;

namespace AuctionService.Repositories.AuctionsRepository
{
    public interface IAuctionsRepository
    {
        Task<List<Auction>> GetAuctionsAsync();
    }
}