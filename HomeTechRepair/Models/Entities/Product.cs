using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeTechRepair.Models.Entities
{
    public class Product : BaseEntity
    {
        public Guid BrandId { get; set; }
        public Guid CategoryId { get; set; }
        [ForeignKey(nameof(BrandId))]
        public virtual Brand Brand { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public virtual Category Category { get; set; }
    }
}
