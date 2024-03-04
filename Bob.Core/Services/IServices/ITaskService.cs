using Bob.Model.DTO.TaskDTO;
using Bob.Model;
using Bob.Model.DTO.PaginationDTO;
namespace Bob.Core.Services.IServices
{
	public interface ITaskService
	{
		Task<APIResponse<List<Guid>>> CreateTask(CreateTaskRequestDTO DTO);
		Task<APIResponse<List<UpdateTaskDTO>>> UpdateTask(UpdateTaskDTO DTO);
		Task<APIResponse<List<UpdateTaskWithRequestedFor>>> UpdateTaskWithRequestedFor(UpdateTaskWithRequestedFor DTO);
		Task<APIResponse<string>> ToogleStatus(ToogleStatusDTO DTO);
		Task<APIResponse<List<GetUserTaskDTO>>> GetUserTasks(TaskPaginationDTO DTO);
		Task<APIResponse<GetUserTaskDTO>> GetATask(Guid TaskId);

	}
}
