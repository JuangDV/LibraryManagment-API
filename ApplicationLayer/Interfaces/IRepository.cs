using ApplicationLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Interfaces
{
    /// <summary>
    /// Repositorio generico para las peticiones basicas CRUD de una entidad
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository <T> where T : class
    {

        /// <summary>
        /// Obtiene un maximo de 3 entidades en la base de datos de la pagina indicada
        /// </summary>
        /// <param name="page"></param>
        /// <returns>Pagina y Lista de Entidades</returns>
        public Task<PagedRepositoryDTO<T>> GetAllAsync(int page);

        /// <summary>
        /// Obtiene una entidad especifica por su identificador en la base de datos
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Entidad o NULL sí no existe</returns>
        public Task<T> GetByIdAsync(int id);

        /// <summary>
        /// Crea una entidad y la agrega a la base de datos
        /// </summary>
        /// <param name="entity"></param>
        public Task CreateAsync(T book);

        /// <summary>
        /// Actualiza una entidad en la base de datos
        /// </summary>
        /// <param name="entity"></param>
        public Task UpdateAsync(T book);

        /// <summary>
        /// Borra una entidad de la base de datos a partir de su identificador sí existe
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task DeleteAsync(int id);
    }
}
