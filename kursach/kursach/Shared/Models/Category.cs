using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace kursach.Shared.Models
{
    [Table("categories")]
    public partial class Category
    {
        public Category()
        {
            Subcategories = new HashSet<Subcategory>();
        }

        [Key]
        [Column("id", TypeName = "varchar(64)")]
        public string Id { get; set; }
        [Required]
        [Column("name", TypeName = "varchar(255)")]
        public string Name { get; set; }
        [Required]
        [Column("symbol", TypeName = "varchar(45)")]
        public string Symbol { get; set; }

        [InverseProperty(nameof(Subcategory.Category))]
        public virtual ICollection<Subcategory> Subcategories { get; set; }
    }
}
