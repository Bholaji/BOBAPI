using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bob.Model.DTO.LeaveDTO
{
	public class GetLeaveDaysAccuralDTO
	{
		public DateTime AccuralDate { get; set; }
		public string AccuralPeriod { get; set; }
		public double Amount { get; set; }
		public string Note { get; set; }
	}
}
