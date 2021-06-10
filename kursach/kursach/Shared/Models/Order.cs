using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace kursach.Shared.Models
{
    [Table("orders")]
    [Index(nameof(Customerid), Name = "fkIdx_32")]
    public partial class Order
    {
        public Order()
        {
            OrderProducts = new HashSet<OrderProduct>();
        }

        [Key]
        [Column("id", TypeName = "varchar(64)")]
        public string Id { get; set; }
        [Required]
        [Column("customerid", TypeName = "varchar(64)")]
        public string Customerid { get; set; }
        [Column("order_number")]
        public int OrderNumber { get; set; }
        [Column("order_date", TypeName = "timestamp")]
        public DateTime OrderDate { get; set; }
        [Required]
        [Column("address", TypeName = "varchar(255)")]
        public string Address { get; set; }
        [Required]
        [Column("order_status", TypeName = "varchar(45)")]
        public string OrderStatus { get; set; }
        [Column("isConfirmed")]
        public bool IsConfirmed { get; set; }
        [Column("isPaid")]
        public bool IsPaid { get; set; }
        [Column("delivery_date", TypeName = "timestamp")]
        public DateTime DeliveryDate { get; set; }

        [ForeignKey(nameof(Customerid))]
        [InverseProperty("Orders")]
        public virtual Customer Customer { get; set; }
        [InverseProperty(nameof(OrderProduct.Order))]
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
