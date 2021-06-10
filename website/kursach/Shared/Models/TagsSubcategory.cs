using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace kursach.Shared.Models
{
    [Table("tags_subcategories")]
    [Index(nameof(SubcategoryId), Name = "fk_tags_has_subcategories_subcategories1_idx")]
    [Index(nameof(TagId), Name = "fk_tags_has_subcategories_tags1_idx")]
    public partial class TagsSubcategory
    {
        [Key]
        [Column("tag_id", TypeName = "varchar(64)")]
        public string TagId { get; set; }
        [Key]
        [Column("subcategory_id", TypeName = "varchar(64)")]
        public string SubcategoryId { get; set; }

        [ForeignKey(nameof(SubcategoryId))]
        [InverseProperty("TagsSubcategories")]
        public virtual Subcategory Subcategory { get; set; }
        [ForeignKey(nameof(TagId))]
        [InverseProperty("TagsSubcategories")]
        public virtual Tag Tag { get; set; }
    }
}
