using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore_CodeFirst
{
    [Table("Book")]
    public class Book
    {
        [Key]
        [Column("Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column("Title")]
        public string? Title { get; set; }
        [Column("Price")]
        public double? Price { get; set; }
        [Column("PubTime")]
        public DateTime? PubTime { get; set; }=DateTime.Now;
        [Column("AuthorName")]
        public string? AuthorName { get; set; }
    }
}
