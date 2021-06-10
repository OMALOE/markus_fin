using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace kursach.Shared.Models
{
    [Table("reference_products")]
    [Index(nameof(Tagid), Name = "fkIdx_273")]
    public partial class ReferenceProduct
    {
        public ReferenceProduct()
        {
            Products = new HashSet<Product>();
        }

        [Key]
        [Column("id", TypeName = "varchar(64)")]
        public string Id { get; set; }
        [Required]
        [Column("name", TypeName = "varchar(255)")]
        public string Name { get; set; }
        [Required]
        [Column("description", TypeName = "text")]
        public string Description { get; set; }
        [Required]
        [Column("options", TypeName = "text")]
        public string Options { get; set; }
        [Required]
        [Column("tagid", TypeName = "varchar(64)")]
        public string Tagid { get; set; }

        [ForeignKey(nameof(Tagid))]
        [InverseProperty("ReferenceProducts")]
        public virtual Tag Tag { get; set; }
        [InverseProperty(nameof(Product.Reference))]
        public virtual ICollection<Product> Products { get; set; }
    }
}
