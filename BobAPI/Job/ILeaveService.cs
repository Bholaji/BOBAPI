using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BobAPI.Job
{
	public interface ILeaveService
	{
        Task CreateUserTimeOff();
        Task EndOfYearLeaveAccrual();
        Task SystemApproveLeave();
	}
}
