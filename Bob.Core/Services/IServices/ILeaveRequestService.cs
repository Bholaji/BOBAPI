using Bob.Model.DTO.LeaveDTO;
using Bob.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bob.Core.Services.IServices
{
	public interface ILeaveRequestService
	{
		Task<APIResponse<string>> RequestLeave(LeaveRequestDTO DTO);
		Task<APIResponse<string>> EditRequestLeave(EditRequestLeaveDTO DTO);
		Task<APIResponse<string>> ToogleStatusApproval(LeaveApprovalDTO DTO);
		Task<APIResponse<List<GetCarryOverActivityDTO>>> GetCarryOverActivityForAUser(Guid userId);
		Task<APIResponse<List<GetLeaveRequestDTO>>> GetLeaveRequestForAUser(Guid userId);
		Task<APIResponse<GetCarryOverActivityDTO>> GetLeaveBalnceBasedOnActivityType(GetCarryOverActivityRequestDTO DTO);
		Task<APIResponse<GetLeaveDaysAccuralDTO>> GetLeaveDaysAccuralBasedOnActivityType(GetCarryOverActivityRequestDTO DTO);
	}
}
