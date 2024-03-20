using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bob.Model.Entities
{
	public class UserTimeOff : BaseLeaveEntity
	{
		public double Holdidays { get; set; }
		public int Sickness_paid { get; set; }
		public string WorkFromHome { get; set; }
		public string Sickness_unpaid { get; set; }
		public int Birthday { get; set; }
		public int Compassionate { get; set; }
		public int MovingDay { get; set; }
		public Guid UserId { get; set; }
	}
}
