using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace kursach.Shared.Models
{
    [Table("subcategories")]
    [Index(nameof(Categoryid), Name = "fkIdx_145")]
    public partial class Subcategory
    {
        public Subcategory()
        {
            TagsSubcategories = new HashSet<TagsSubcategory>();
        }

        [Key]
        [Column("id", TypeName = "varchar(64)")]
        public string Id { get; set; }
        [Required]
        [Column("name", TypeName = "varchar(255)")]
        public string Name { get; set; }
        [Required]
        [Column("categoryid", TypeName = "varchar(64)")]
        public string Categoryid { get; set; }

        [ForeignKey(nameof(Categoryid))]
        [InverseProperty("Subcategories")]
        public virtual Category Category { get; set; }
        [InverseProperty(nameof(TagsSubcategory.Subcategory))]
        public virtual ICollection<TagsSubcategory> TagsSubcategories { get; set; }
    }
}
