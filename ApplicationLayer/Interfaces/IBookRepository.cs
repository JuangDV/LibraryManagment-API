using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entities;

namespace ApplicationLayer.Interfaces
{
    /// <summary>
    /// Repositorio para la manipulación de metodos especificos de la clase <see cref="Books"/>
    /// <para>Hereda los metodos de la interfaz <see cref="IRepository{T}"/></para>
    /// </summary>
    public interface IBookRepository : IRepository<Books>
    {
        /// <summary>
        /// Valida la creación o edición de un libro
        /// </summary>
        /// <param name="book"></param>
        /// <returns>TRUE si puede ser creado/editado, de lo contrario FALSE</returns>
        public Task<string> ValidateAsync(Books book);
    }
}
