using Bob.Model.DTO.LeaveDTO;
using Bob.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bob.Core.Strategy
{
	public interface IEditLeaveRequestStrategy
	{
		Task HandleEdit(EditRequestLeaveDTO DTO, LeaveRequest leaveRequest);
	}
}
