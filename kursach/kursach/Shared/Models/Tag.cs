using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace kursach.Shared.Models
{
    [Table("tags")]
    public partial class Tag
    {
        public Tag()
        {
            Products = new HashSet<Product>();
            ReferenceProducts = new HashSet<ReferenceProduct>();
            TagsSubcategories = new HashSet<TagsSubcategory>();
        }

        [Key]
        [Column("id", TypeName = "varchar(64)")]
        public string Id { get; set; }
        [Required]
        [Column("name", TypeName = "varchar(255)")]
        public string Name { get; set; }
        [Column("views")]
        public int Views { get; set; }

        [InverseProperty(nameof(Product.Tag))]
        public virtual ICollection<Product> Products { get; set; }
        [InverseProperty(nameof(ReferenceProduct.Tag))]
        public virtual ICollection<ReferenceProduct> ReferenceProducts { get; set; }
        [InverseProperty(nameof(TagsSubcategory.Tag))]
        public virtual ICollection<TagsSubcategory> TagsSubcategories { get; set; }
    }
}
