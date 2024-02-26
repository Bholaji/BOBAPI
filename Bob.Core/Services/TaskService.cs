
using AutoMapper;
using Bob.Core.Exceptions;
using Bob.Core.Services.IServices;
using Bob.DataAccess.Repository.IRepository;
using Bob.Migrations.Data;
using Bob.Model;
using Bob.Model.DTO.PaginationDTO;
using Bob.Model.DTO.PostDTO;
using Bob.Model.DTO.TaskDTO;
using Bob.Model.Entities;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Bob.Core.Services
{
	public class TaskService : ITaskService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly ILogger<TaskService> _logger;
		private readonly ApplicationDbContext _db;
		public TaskService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<TaskService> logger, ApplicationDbContext db)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_logger = logger;
			_db = db;
		}

		public async Task<APIResponse<TaskRequestDTO>> CreateTask(TaskRequestDTO DTO)
		{

			User currentUser = await _unitOfWork.User.GetAsync(u => u.Id == DTO.UserId);

			if (currentUser is null)
			{
				throw new NotFoundException($"{nameof(User)} {ResponseMessage.NotFound}");
			}

			UserTask task = _mapper.Map<UserTask>(DTO);

			task.UserId = currentUser.Id;


			await _unitOfWork.UserTask.CreateAsync(task);
			await _db.SaveTaskChanges(task);

			var response = new APIResponse<TaskRequestDTO>
			{
				IsSuccess = true,
				Message = ResponseMessage.IsSuccess,
				Result = _mapper.Map<TaskRequestDTO>(task)
			};

			response.Result.TaskId = task.TaskId;
			return response;

		}

		public async Task<APIResponse<UpdateTaskDTO>> UpdateTask(UpdateTaskDTO DTO)
		{
			User user = await _unitOfWork.User.GetAsync(u => u.Id == DTO.UserId);
			UserTask task = await _unitOfWork.UserTask.GetAsync(u => u.TaskId == DTO.TaskId);

			if (user is null)
			{
				throw new NotFoundException($"{nameof(User)} {ResponseMessage.NotFound}");
			}

			if (task is null)
			{
				throw new NotFoundException($"{nameof(UserTask)} {ResponseMessage.NotFound}");
			}

			task.TaskName = DTO.TaskName ?? task.TaskName;
			task.RequestedFor = DTO.RequestedFor ?? task.RequestedFor;
			task.TaskDescription = DTO.TaskDescription ?? task.TaskDescription;
			task.TaskList = DTO.TaskList ?? task.TaskList;
			task.DueDate = DTO.DueDate ?? task.DueDate;
			task.StartDate = DTO.StartDate ?? task.StartDate;
			task.TaskStatus = DTO.TaskStatus ?? task.TaskStatus;

			_unitOfWork.UserTask.Update(task);

			await _db.SaveTaskChanges(task);

			var response = new APIResponse<UpdateTaskDTO>
			{
				IsSuccess = true,
				Message = ResponseMessage.IsSuccess,
				Result = _mapper.Map<UpdateTaskDTO>(task)
			};

			response.Result.TaskId = task.TaskId;
			return response;

		}

		public async Task<APIResponse<ToogleStatusDTO>> ToogleStatus(ToogleStatusDTO DTO)
		{
			UserTask task = await _unitOfWork.UserTask.GetAsync(u => u.TaskId == DTO.TaskId);

			if (task is null)
			{
				throw new NotFoundException($"{nameof(UserTask)} {ResponseMessage.NotFound}");
			}

			task.TaskStatus = DTO.TaskStatus ?? task.TaskStatus;

			_unitOfWork.UserTask.Update(task);

			await _db.SaveTaskChanges(task);

			var response = new APIResponse<ToogleStatusDTO>
			{
				IsSuccess = true,
				Message = ResponseMessage.IsSuccess,
				Result = _mapper.Map<ToogleStatusDTO>(task)
			};

			response.Result.TaskId = task.TaskId;
			return response;
		}

		public async Task<APIResponse<List<GetUserTaskDTO>>> GetUserTasks(TaskPaginationDTO DTO)
		{
			User user = await _unitOfWork.User.GetAsync(u => u.Id == DTO.UserId);

			if (user is null)
			{
				throw new NotFoundException($"{nameof(User)} {ResponseMessage.NotFound}");
			}

			IEnumerable<UserTask> task = await _unitOfWork.UserTask
				.GetAllAsync(u => u.UserId == DTO.UserId, pageNumber: DTO.PageNumber, pageSize: DTO.PageSize);

			return new APIResponse<List<GetUserTaskDTO>>
			{
				IsSuccess = true,
				Message = ResponseMessage.IsSuccess,
				Result = _mapper.Map<List<GetUserTaskDTO>>(task)
			};

		}

		public async Task<APIResponse<GetUserTaskDTO>> GetATask(TaskPaginationDTO DTO)
		{
			User user = await _unitOfWork.User.GetAsync(u => u.Id == DTO.UserId);

			if (user is null)
			{
				throw new NotFoundException($"{nameof(User)} {ResponseMessage.NotFound}");
			}
			UserTask task = await _unitOfWork.UserTask.GetAsync(u => u.TaskId == DTO.TaskId);

			if (task is null)
			{
				throw new NotFoundException($"{nameof(UserTask)} {ResponseMessage.NotFound}");
			}

			return new APIResponse<GetUserTaskDTO>
			{
				IsSuccess = true,
				Message = ResponseMessage.IsSuccess,
				Result = _mapper.Map<GetUserTaskDTO>(task)
			};

		}
	}

}
