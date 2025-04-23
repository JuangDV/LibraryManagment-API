using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entities;

namespace ApplicationLayer.DTOs
{
    public class WithoutIdBookRequest
    {
        [Required, MaxLength(100)]
        public required string Title { get; set; }
        [Required, MaxLength(100)]
        public required string Author { get; set; }
        [Required]
        public required int YearPublication { get; set; }
        [Required, MaxLength(13), MinLength(13)]
        public required string ISBN { get; set; }
        [MaxLength(50)]
        public string Genre { get; set; } = "Generic";
        public bool Available { get; set; } = true;
    }
}
