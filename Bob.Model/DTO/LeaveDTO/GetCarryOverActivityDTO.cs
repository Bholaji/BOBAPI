using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Bob.Model.DTO.LeaveDTO
{
	public class GetCarryOverActivityDTO
	{
		public DateTime EffectiveDate { get; set; }
		public double Amount { get; set; }
		[MaxLength(500)]
		public string Description { get; set; }
		public DateTime UpdatedOn { get; set; }
	}
}
