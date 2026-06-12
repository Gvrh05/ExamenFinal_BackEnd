using HackerRank1.Entities;
using LibraryService.WebAPI.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerRank1.Services
{
    public class FraudService : IFraudService
    {
        private readonly LibraryContext _libraryContext;

        public FraudService(LibraryContext libraryContext)
        {
            _libraryContext = libraryContext;
        }

        public async Task<IEnumerable<Fraud>> GetAll()
        {
            return await _libraryContext.Frauds
                .OrderByDescending(f => f.CreatedAt)
                .ToListAsync();
        }

        public async Task<Fraud> Add(Fraud fraud)
        {
            fraud.CreatedAt = DateTime.UtcNow;  
            await _libraryContext.Frauds.AddAsync(fraud);
            await _libraryContext.SaveChangesAsync();
            return fraud;
        }
    }

    public interface IFraudService
    {
        Task<IEnumerable<Fraud>> GetAll();
        Task<Fraud> Add(Fraud fraud);
    }
}