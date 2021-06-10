using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace kursach.Shared.Models
{
    [Table("sessions")]
    [Index(nameof(CustomerId), Name = "FK_session_1_idx")]
    public partial class Session
    {
        [Key]
        [Column("id", TypeName = "varchar(64)")]
        public string Id { get; set; }
        [Required]
        [Column("customer_id", TypeName = "varchar(64)")]
        public string CustomerId { get; set; }
        [Column("start", TypeName = "timestamp")]
        public DateTime? Start { get; set; }

        [ForeignKey(nameof(CustomerId))]
        [InverseProperty("Sessions")]
        public virtual Customer Customer { get; set; }
    }
}
