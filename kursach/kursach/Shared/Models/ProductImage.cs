using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace kursach.Shared.Models
{
    [Table("product_images")]
    [Index(nameof(Productid), Name = "fkIdx_211")]
    [Index(nameof(Imageid), Name = "fkIdx_216")]
    public partial class ProductImage
    {
        [Key]
        [Column("productid", TypeName = "varchar(64)")]
        public string Productid { get; set; }
        [Key]
        [Column("imageid", TypeName = "varchar(64)")]
        public string Imageid { get; set; }

        [ForeignKey(nameof(Imageid))]
        [InverseProperty("ProductImages")]
        public virtual Image Image { get; set; }
        [ForeignKey(nameof(Productid))]
        [InverseProperty("ProductImages")]
        public virtual Product Product { get; set; }
    }
}
