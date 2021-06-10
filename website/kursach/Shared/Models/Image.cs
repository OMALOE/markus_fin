using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace kursach.Shared.Models
{
    [Table("images")]
    public partial class Image
    {
        public Image()
        {
            CompanyBanners = new HashSet<Company>();
            CompanyLogos = new HashSet<Company>();
            Customers = new HashSet<Customer>();
            ProductImages = new HashSet<ProductImage>();
        }

        [Key]
        [Column("id", TypeName = "varchar(64)")]
        public string Id { get; set; }
        [Required]
        [Column("path", TypeName = "text")]
        public string Path { get; set; }

        [InverseProperty(nameof(Company.Banner))]
        public virtual ICollection<Company> CompanyBanners { get; set; }
        [InverseProperty(nameof(Company.Logo))]
        public virtual ICollection<Company> CompanyLogos { get; set; }
        [InverseProperty(nameof(Customer.AvatarNavigation))]
        public virtual ICollection<Customer> Customers { get; set; }
        [InverseProperty(nameof(ProductImage.Image))]
        public virtual ICollection<ProductImage> ProductImages { get; set; }
    }
}
