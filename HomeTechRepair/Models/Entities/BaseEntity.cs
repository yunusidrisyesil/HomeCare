using System;
using System.ComponentModel.DataAnnotations;

namespace HomeTechRepair.Models.Entities
{
	public abstract class BaseEntity
	{
		[Key]
		public Guid Id { get; set; }
		[StringLength(50)]
		public string Name { get; set; }
	}
}
