using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace kursach.Shared.Models
{
    [Table("company_admins")]
    [Index(nameof(Customerid), Name = "fkIdx_289")]
    [Index(nameof(Companyid), Name = "fkIdx_77")]
    public partial class CompanyAdmin
    {
        [Key]
        [Column("companyid", TypeName = "varchar(64)")]
        public string Companyid { get; set; }
        [Key]
        [Column("customerid", TypeName = "varchar(64)")]
        public string Customerid { get; set; }
        [Required]
        [Column("role", TypeName = "varchar(45)")]
        public string Role { get; set; }

        [ForeignKey(nameof(Companyid))]
        [InverseProperty("CompanyAdmins")]
        public virtual Company Company { get; set; }
        [ForeignKey(nameof(Customerid))]
        [InverseProperty("CompanyAdmins")]
        public virtual Customer Customer { get; set; }
    }
}
