using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationLayer.Interfaces;
using DataAccessLayer.Context;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Repositories
{
    public class BookRepository : Repository<Books>, IBookRepository
    {
        private readonly DbSet<Books> _dbSet;
        private readonly AppDbContext _appDbContext;
        public BookRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
            _dbSet = appDbContext.Set<Books>();
        }

        public async Task<string> ValidateAsync(Books book)
        {
            string errorMessage = String.Empty;
            if (await _dbSet.Where(b => b.Id != book.Id).AnyAsync(b => b.ISBN == book.ISBN)) errorMessage += $"El ISBN '{book.ISBN}' ya esta en uso.\n";

            

            return errorMessage;
        }
    }
}
