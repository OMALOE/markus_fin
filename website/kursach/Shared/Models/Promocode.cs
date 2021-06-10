using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace kursach.Shared.Models
{
    [Table("promocodes")]
    [Index(nameof(Companyid), Name = "fkIdx_228")]
    public partial class Promocode
    {
        [Key]
        [Column("id", TypeName = "varchar(64)")]
        public string Id { get; set; }
        [Required]
        [Column("type", TypeName = "varchar(45)")]
        public string Type { get; set; }
        [Required]
        [Column("name", TypeName = "varchar(25)")]
        public string Name { get; set; }
        [Column("condition", TypeName = "varchar(64)")]
        public string Condition { get; set; }
        [Required]
        [Column("companyid", TypeName = "varchar(64)")]
        public string Companyid { get; set; }
        [Column("discount")]
        public decimal Discount { get; set; }

        [ForeignKey(nameof(Companyid))]
        [InverseProperty("Promocodes")]
        public virtual Company Company { get; set; }
    }
}
