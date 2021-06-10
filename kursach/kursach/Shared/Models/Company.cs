using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace kursach.Shared.Models
{
    [Table("companies")]
    [Index(nameof(Logoid), Name = "fkIdx_93")]
    [Index(nameof(Bannerid), Name = "fkIdx_96")]
    public partial class Company
    {
        public Company()
        {
            CompanyAdmins = new HashSet<CompanyAdmin>();
            Products = new HashSet<Product>();
            Promocodes = new HashSet<Promocode>();
        }

        [Key]
        [Column("id", TypeName = "varchar(64)")]
        public string Id { get; set; }
        [Required]
        [Column("public_name", TypeName = "varchar(255)")]
        public string PublicName { get; set; }
        [Required]
        [Column("description", TypeName = "text")]
        public string Description { get; set; }
        [Required]
        [Column("legal_type", TypeName = "varchar(45)")]
        public string LegalType { get; set; }
        [Required]
        [Column("legal_name", TypeName = "varchar(255)")]
        public string LegalName { get; set; }
        [Required]
        [Column("legal_address", TypeName = "varchar(255)")]
        public string LegalAddress { get; set; }
        [Required]
        [Column("TIN", TypeName = "varchar(25)")]
        public string Tin { get; set; }
        [Column("TRRC", TypeName = "varchar(45)")]
        public string Trrc { get; set; }
        [Required]
        [Column("PSRN", TypeName = "varchar(45)")]
        public string Psrn { get; set; }
        [Required]
        [Column("country", TypeName = "varchar(45)")]
        public string Country { get; set; }
        [Required]
        [Column("city", TypeName = "varchar(255)")]
        public string City { get; set; }
        [Required]
        [Column("delivery_region", TypeName = "varchar(45)")]
        public string DeliveryRegion { get; set; }
        [Required]
        [Column("real_address", TypeName = "varchar(255)")]
        public string RealAddress { get; set; }
        [Column("delivery_delay")]
        public int DeliveryDelay { get; set; }
        [Column("work_hours_start", TypeName = "timestamp")]
        public DateTime WorkHoursStart { get; set; }
        [Column("work_hours_end", TypeName = "timestamp")]
        public DateTime WorkHoursEnd { get; set; }
        [Column("logoid", TypeName = "varchar(64)")]
        public string Logoid { get; set; }
        [Column("bannerid", TypeName = "varchar(64)")]
        public string Bannerid { get; set; }

        [ForeignKey(nameof(Bannerid))]
        [InverseProperty(nameof(Image.CompanyBanners))]
        public virtual Image Banner { get; set; }
        [ForeignKey(nameof(Logoid))]
        [InverseProperty(nameof(Image.CompanyLogos))]
        public virtual Image Logo { get; set; }
        [InverseProperty(nameof(CompanyAdmin.Company))]
        public virtual ICollection<CompanyAdmin> CompanyAdmins { get; set; }
        [InverseProperty(nameof(Product.Company))]
        public virtual ICollection<Product> Products { get; set; }
        [InverseProperty(nameof(Promocode.Company))]
        public virtual ICollection<Promocode> Promocodes { get; set; }
    }
}
