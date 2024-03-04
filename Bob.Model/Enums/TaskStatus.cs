using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bob.Model.Enums
{
	public enum TaskStatus
	{
		Completed = 1,
		Incomplete
	}

	public enum TaskActivity
	{
		Created,
		Updated,
		StatusUpdate
	}
}
