using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace kursach.Shared.Models
{
    [Table("order_products")]
    [Index(nameof(Orderid), Name = "fkIdx_42")]
    [Index(nameof(Productid), Name = "fkIdx_47")]
    public partial class OrderProduct
    {
        [Key]
        [Column("orderid", TypeName = "varchar(64)")]
        public string Orderid { get; set; }
        [Key]
        [Column("productid", TypeName = "varchar(64)")]
        public string Productid { get; set; }
        [Column("amount")]
        public int Amount { get; set; }
        [Column("total_price")]
        public decimal TotalPrice { get; set; }
        [Column("discount")]
        public decimal Discount { get; set; }

        [ForeignKey(nameof(Orderid))]
        [InverseProperty("OrderProducts")]
        public virtual Order Order { get; set; }
        [ForeignKey(nameof(Productid))]
        [InverseProperty("OrderProducts")]
        public virtual Product Product { get; set; }
    }
}
