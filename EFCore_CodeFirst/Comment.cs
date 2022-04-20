using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore_CodeFirst
{
    [Table("Comment")]
    public class Comment
    {
        [Key]
        [Column("Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column("Title")]
        public string? Title { get; set; }
        [Column("CreatedDate")]
        public DateTime CreatedDate { get; set; }=DateTime.Now;
        [Column("CommentStr")]
        public string? CommentStr { get; set; }
    }
}
