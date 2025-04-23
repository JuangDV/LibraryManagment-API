using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Books
    {
        public Books(int id, string title, string author, int yearPublication, string iSBN, string genre, bool available)
        {
            Id = id;
            Title = title;
            Author = author;
            YearPublication = yearPublication;
            ISBN = iSBN;
            Genre = genre;
            Available = available;
        }

        [Required]
        public int Id { get; set; } // PRIMARY KEY
        [Required, MaxLength(100)]
        public string Title { get; set; }
        [Required, MaxLength(100)]
        public string Author { get; set; }
        [Required]
        public int YearPublication { get; set; }
        [Required, MaxLength(13), MinLength(13)]
        public string ISBN { get; set; }
        [MaxLength(50)]
        public string Genre { get; set; } = "Generic";
        public bool Available { get; set; } = true;
    }
}
