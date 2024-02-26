using Bob.Model.DTO.TaskDTO;
using Bob.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bob.Model.DTO.PaginationDTO;
namespace Bob.Core.Services.IServices
{
	public interface ITaskService
	{
		Task<APIResponse<TaskRequestDTO>> CreateTask(TaskRequestDTO DTO);
		Task<APIResponse<ToogleStatusDTO>> ToogleStatus(ToogleStatusDTO DTO);
		Task<APIResponse<List<GetUserTaskDTO>>> GetUserTasks(TaskPaginationDTO DTO);
		Task<APIResponse<GetUserTaskDTO>> GetATask(TaskPaginationDTO DTO);
		Task<APIResponse<UpdateTaskDTO>> UpdateTask(UpdateTaskDTO DTO);
	}
}
