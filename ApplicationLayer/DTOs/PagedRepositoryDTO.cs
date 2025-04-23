using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.DTOs
{
    public class PagedRepositoryDTO<T> where T: class
    {
        public PagedRepositoryDTO(){}
        public PagedRepositoryDTO(int page, IEnumerable<T> entities)
        {
            this.Page = page;
            this.entities = entities;
        }

        public int Page { get; set; } = 1;
        public IEnumerable<T> entities { get; set; } = new List<T>();
    }
}
