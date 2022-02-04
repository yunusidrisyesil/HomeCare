using System.Collections.Generic;

namespace HomeTechRepair.Models.Entities
{
    public class Category : BaseEntity
    {
		public List<Product> Products { get; set; }
	}
}
