
using AutoMapper;
using Bob.Core.Exceptions;
using Bob.Core.Services.IServices;
using Bob.DataAccess.Repository.IRepository;
using Bob.Migrations.Data;
using Bob.Model;
using Bob.Model.DTO.PaginationDTO;
using Bob.Model.DTO.TaskDTO;
using Bob.Model.Entities;
using Microsoft.Extensions.Logging;

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

		public async Task<APIResponse<CreateTaskResponse>> CreateTask(CreateTaskRequestDTO DTO)
		{
			IEnumerable<User> users;
			UserTask tasks = null;

			if (DTO.isGeneral is true)
			{
				users = await _unitOfWork.User.GetAllAsync(U => U.OrganizationId == DTO.OrganizationId);

			}
			else
			{
				users = await _unitOfWork.User.GetAllAsync(u => DTO.RequestedFor.Contains(u.Id));
			}

			foreach (var user in users)
			{
				var currentUser = await _unitOfWork.User.GetAsync(u => u.Id == DTO.RequestedBy);
				tasks = _mapper.Map<UserTask>(DTO);

				tasks.RequestedForId = user.Id;

				tasks.RequestedById = currentUser.Id;

				var activityLog = new ActivityLog()
				{
					TaskId = tasks.Id,
					UserId = user.Id,
					Activity = $"Task created by {currentUser.DispalyName} at {DateTime.Now}"
				};

				await _unitOfWork.UserTask.CreateAsync(tasks);
				await _unitOfWork.ActivityLog.CreateAsync(activityLog);

				await _unitOfWork.SaveAsync();
			}

			var response = new APIResponse<CreateTaskResponse>
			{
				IsSuccess = true,
				Message = ResponseMessage.IsSuccess,
				Result = _mapper.Map<CreateTaskResponse>(tasks)
			};

			response.Result.TaskId = tasks.Id;
			return response;

		}

		public async Task<APIResponse<List<UpdateTaskDTO>>> UpdateTask(UpdateTaskDTO DTO)
		{
			IEnumerable<UserTask> tasks;

			if (DTO.isGeneral is true)
			{
				tasks = await _unitOfWork.UserTask.GetAllAsync(U => U.OrganizationId == DTO.OrganizationId);

			}
			else
			{
				tasks = await _unitOfWork.UserTask.GetAllAsync(u => DTO.RequestedFor.Contains(u.RequestedForId));
			}

			foreach (var currentTask in tasks)
			{
				var currentUser = await _unitOfWork.User.GetAsync(u => u.Id == DTO.RequestedBy);

				if (currentUser is null) {

					throw new NotFoundException($"{nameof(User)} {ResponseMessage.NotFound}");
				}

				var activityLog = new ActivityLog()
				{
					TaskId = currentTask.Id,
					UserId = currentTask.RequestedForId,
					Activity = $"Task Updated by {currentUser.DispalyName} at {DateTime.Now}"
				};

				currentTask.TaskName = DTO.TaskName ?? currentTask.TaskName;
				currentTask.TaskDescription = DTO.TaskDescription ?? currentTask.TaskDescription;
				currentTask.TaskList = DTO.TaskList ?? currentTask.TaskList;
				currentTask.DueDate = DTO.DueDate ?? currentTask.DueDate;
				currentTask.StartDate = DTO.StartDate ?? currentTask.StartDate;
				currentTask.TaskStatus = DTO.TaskStatus ?? currentTask.TaskStatus;

				_unitOfWork.UserTask.Update(currentTask);

				await _unitOfWork.ActivityLog.CreateAsync(activityLog);

				await _unitOfWork.SaveAsync();
			}

			return new APIResponse<List<UpdateTaskDTO>>
			{
				IsSuccess = true,
				Message = ResponseMessage.IsSuccess,
				Result = _mapper.Map<List<UpdateTaskDTO>>(tasks)
			};

		}

		public async Task<APIResponse<string>> ToogleStatus(ToogleStatusDTO DTO)
		{
			UserTask task = await _unitOfWork.UserTask.GetAsync(u => u.Id == DTO.TaskId);
			User user = await _unitOfWork.User.GetAsync(u => u.Id == task.RequestedForId);

			if (task is null)
			{
				throw new NotFoundException($"{nameof(UserTask)} {ResponseMessage.NotFound}");
			}

			task.TaskStatus = DTO.TaskStatus;

			var activityLog = new ActivityLog()
			{
				TaskId = task.Id,
				UserId = task.RequestedForId,
				Activity = $"Task Status was updated by {user.DispalyName} at {DateTime.Now}"
			};
			_unitOfWork.UserTask.Update(task);
			await _unitOfWork.ActivityLog.CreateAsync(activityLog);
			await _unitOfWork.SaveAsync();

			return new APIResponse<string>
			{
				IsSuccess = true,
				Message = ResponseMessage.IsSuccess,
				Result = "Status Updated Successfully"
			};
		}

		public async Task<APIResponse<List<GetUserTaskDTO>>> GetUserTasks(TaskPaginationDTO DTO)
		{
			User user = await _unitOfWork.User.GetAsync(u => u.Id == DTO.UserId);

			if (user is null)
			{
				throw new NotFoundException($"{nameof(User)} {ResponseMessage.NotFound}");
			}

			IEnumerable<UserTask> task = await _unitOfWork.UserTask
				.GetAllAsync(u => u.RequestedForId == DTO.UserId, pageNumber: DTO.PageNumber, pageSize: DTO.PageSize);

			var mappedList = _mapper.Map<List<GetUserTaskDTO>>(task);

			return new APIResponse<List<GetUserTaskDTO>>
			{
				IsSuccess = true,
				Message = ResponseMessage.IsSuccess,
				Result = mappedList
			};

		}

		public async Task<APIResponse<GetUserTaskDTO>> GetATask(Guid TaskId)
		{
			UserTask task = await _unitOfWork.UserTask.GetAsync(u => u.Id == TaskId);
			User user = await _unitOfWork.User.GetAsync(u => u.Id == task.RequestedForId);

			if (user is null)
			{
				throw new NotFoundException($"{nameof(User)} {ResponseMessage.NotFound}");
			}
			if (task is null)
			{
				throw new NotFoundException($"{nameof(UserTask)} {ResponseMessage.NotFound}");
			}
			var mappedList = _mapper.Map<GetUserTaskDTO>(task);
			return new APIResponse<GetUserTaskDTO>
			{
				IsSuccess = true,
				Message = ResponseMessage.IsSuccess,
				Result = mappedList
			};

		}
	}

}
