using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace kursach.Shared.Models
{
    [Table("variation_options")]
    [Index(nameof(ProductId), Name = "fk_variation_options_products1_idx")]
    public partial class VariationOption
    {
        [Key]
        [Column("id", TypeName = "varchar(64)")]
        public string Id { get; set; }
        [Key]
        [Column("product_id", TypeName = "varchar(64)")]
        public string ProductId { get; set; }
        [Key]
        [Column("name", TypeName = "varchar(255)")]
        public string Name { get; set; }
        [Required]
        [Column("value", TypeName = "varchar(45)")]
        public string Value { get; set; }

        [ForeignKey(nameof(ProductId))]
        [InverseProperty("VariationOptions")]
        public virtual Product Product { get; set; }
    }
}
