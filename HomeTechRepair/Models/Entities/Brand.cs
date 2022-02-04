using System.Collections.Generic;

namespace HomeTechRepair.Models.Entities
{
    public class Brand : BaseEntity
    {
		public List<Product> Products { get; set; }
	}
}
