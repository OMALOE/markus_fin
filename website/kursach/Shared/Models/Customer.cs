using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace kursach.Shared.Models
{
    [Table("customers")]
    [Index(nameof(Avatar), Name = "FK_285")]
    public partial class Customer
    {
        public Customer()
        {
            CompanyAdmins = new HashSet<CompanyAdmin>();
            Orders = new HashSet<Order>();
            Sessions = new HashSet<Session>();
        }

        [Key]
        [Column("id", TypeName = "varchar(64)")]
        public string Id { get; set; }
        [Required]
        [Column("firstname", TypeName = "varchar(64)")]
        public string Firstname { get; set; }
        [Required]
        [Column("lastname", TypeName = "varchar(64)")]
        public string Lastname { get; set; }
        [Required]
        [Column("phone", TypeName = "varchar(45)")]
        public string Phone { get; set; }
        [Required]
        [Column("email", TypeName = "varchar(255)")]
        public string Email { get; set; }
        [Required]
        [Column("password", TypeName = "varchar(45)")]
        public string Password { get; set; }
        [Column("birthdate", TypeName = "date")]
        public DateTime Birthdate { get; set; }
        [Column("avatar", TypeName = "varchar(64)")]
        public string Avatar { get; set; }
        [Required]
        [Column("bio", TypeName = "varchar(64)")]
        public string Bio { get; set; }

        [ForeignKey(nameof(Avatar))]
        [InverseProperty(nameof(Image.Customers))]
        public virtual Image AvatarNavigation { get; set; }
        [InverseProperty(nameof(CompanyAdmin.Customer))]
        public virtual ICollection<CompanyAdmin> CompanyAdmins { get; set; }
        [InverseProperty(nameof(Order.Customer))]
        public virtual ICollection<Order> Orders { get; set; }
        [InverseProperty(nameof(Session.Customer))]
        public virtual ICollection<Session> Sessions { get; set; }
    }
}
