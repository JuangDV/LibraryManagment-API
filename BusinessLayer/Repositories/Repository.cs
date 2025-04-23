using System;
using System.Collections.Generic;
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

        public async Task<PagedRepositoryDTO<T>> GetAllAsync(int page)
        {
            var entityPage = new PagedRepositoryDTO<T>();
            // calcular la cantidad de paginas dividiendo por 3 la cantidad de entidades
            int entitiesCount = await _dbSet.CountAsync();
            if(entitiesCount > 0)
            {
                decimal totalPages = Math.Ceiling((decimal)(entitiesCount / 3.00));

                if (page < 1 || totalPages < page) page = (int)totalPages; // devolver la ultima pagina

                var entitiesList = await _dbSet.Skip((page - 1) * 3).Take(3).ToListAsync(); // Obtener solo las entidades de una pagina
                entityPage = new PagedRepositoryDTO<T>(page, entitiesList); // Devolver un DTO con las entidades y la pagina correcta
            } else
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
