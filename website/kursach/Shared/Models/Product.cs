using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace kursach.Shared.Models
{
    [Table("products")]
    [Index(nameof(Tagid), Name = "fkIdx_264")]
    [Index(nameof(Referenceid), Name = "fkIdx_276")]
    [Index(nameof(Companyid), Name = "fkIdx_62")]
    public partial class Product
    {
        public Product()
        {
            OrderProducts = new HashSet<OrderProduct>();
            ProductImages = new HashSet<ProductImage>();
            VariationOptions = new HashSet<VariationOption>();
        }

        [Key]
        [Column("id", TypeName = "varchar(64)")]
        public string Id { get; set; }
        [Required]
        [Column("fullname", TypeName = "varchar(255)")]
        public string Fullname { get; set; }
        [Column("price")]
        public decimal Price { get; set; }
        [Column("available")]
        public int Available { get; set; }
        [Required]
        [Column("description", TypeName = "text")]
        public string Description { get; set; }
        [Column("views")]
        public int Views { get; set; }
        [Column("creation_date", TypeName = "datetime")]
        public DateTime? CreationDate { get; set; }
        [Required]
        [Column("companyid", TypeName = "varchar(64)")]
        public string Companyid { get; set; }
        [Required]
        [Column("tagid", TypeName = "varchar(64)")]
        public string Tagid { get; set; }
        [Column("referenceid", TypeName = "varchar(64)")]
        public string Referenceid { get; set; }

        [ForeignKey(nameof(Companyid))]
        [InverseProperty("Products")]
        public virtual Company Company { get; set; }
        [ForeignKey(nameof(Referenceid))]
        [InverseProperty(nameof(ReferenceProduct.Products))]
        public virtual ReferenceProduct Reference { get; set; }
        [ForeignKey(nameof(Tagid))]
        [InverseProperty("Products")]
        public virtual Tag Tag { get; set; }
        [InverseProperty(nameof(OrderProduct.Product))]
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
        [InverseProperty(nameof(ProductImage.Product))]
        public virtual ICollection<ProductImage> ProductImages { get; set; }
        [InverseProperty(nameof(VariationOption.Product))]
        public virtual ICollection<VariationOption> VariationOptions { get; set; }
    }
}
