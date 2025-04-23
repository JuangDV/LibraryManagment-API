using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationLayer.DTOs;
using ApplicationLayer.Interfaces;
using DataAccessLayer.Context;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using static System.Reflection.Metadata.BlobBuilder;

namespace BusinessLayer.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _appDbContext;
        private readonly DbSet<T> _dbSet;
        public Repository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _dbSet = appDbContext.Set<T>();
        }


        public async Task CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _appDbContext.SaveChangesAsync();
        }


        public async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _appDbContext.SaveChangesAsync();
            }
        }

        public async Task<PagedRepositoryDTO<T>> GetAllAsync(int page, int pageDivisor)
        {
            const int entityMaxLoad = 10; // MAXIMO DE ENTIDADES QUE PUEDEN CARGARSE POR PAGINA
            var entityPage = new PagedRepositoryDTO<T>();
            int entitiesCount = await _dbSet.CountAsync();
            if (pageDivisor > entityMaxLoad)
            {
                throw new ArgumentOutOfRangeException(nameof(pageDivisor), $"Solo puede cargarse un maximo de {entityMaxLoad} registros por pagina");
            }
            else if (entitiesCount > 0)
            {
                decimal totalPages = (decimal)Math.Ceiling(entitiesCount / (float)pageDivisor);

                if (page < 1 || totalPages < page) page = (int)totalPages; // devolver la ultima pagina

                var entitiesList = await _dbSet.Skip((page - 1) * pageDivisor).Take(pageDivisor).ToListAsync(); // Obtener solo las entidades de una pagina
                entityPage = new PagedRepositoryDTO<T>(page, entitiesList); // Devolver un DTO con las entidades y la pagina correcta
            }
            else
            {
                entityPage = new PagedRepositoryDTO<T>(-1, new List<T>());
            }

            return entityPage;
        }

        public async Task<T> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
