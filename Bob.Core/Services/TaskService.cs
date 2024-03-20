
using AutoMapper;
using Bob.Core.Exceptions;
using Bob.Core.Services.IServices;
using Bob.DataAccess.Repository.IRepository;
using Bob.Migrations.Data;
using Bob.Model;
using Bob.Model.DTO.PaginationDTO;
using Bob.Model.DTO.TaskDTO;
using Bob.Model.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TaskStatus = Bob.Model.Enums.TaskStatus;

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

		public async Task<APIResponse<List<Guid>>> CreateTask(CreateTaskRequestDTO DTO)
		{
			IEnumerable<User> users;
			UserTask tasks = null;

			if (DTO.isGeneral)
			{
				users = await _unitOfWork.User.GetAllAsync(U => U.OrganizationId == DTO.OrganizationId);

			}
			else
			{
				users = await _unitOfWork.User.GetAllAsync(u => DTO.RequestedFor.Contains(u.Id));

				List<Guid> NonExistentUser = DTO.RequestedFor.Except(users.Select(u => u.Id)).ToList();
				if (NonExistentUser.Count > 0)
				{
					return new APIResponse<List<Guid>>
					{
						IsSuccess = false,
						Message = ResponseMessage.IsError,
						Result = NonExistentUser
					};
				}
			}
			var currentUser = await _unitOfWork.User.GetAsync(u => u.Id == DTO.RequestedBy);

			var newTask = new TaskJob()
			{
				TaskName = DTO.TaskName,
				TaskDescription = DTO.TaskDescription,
				TaskList = DTO.TaskList,
				OrganizationId = DTO.OrganizationId
			};

			var taskJobs = new List<TaskJob>();
			var userTasks = new List<UserTask>();
			var activityLogs = new List<ActivityLog>();

			foreach (var user in users)
			{
				tasks = _mapper.Map<UserTask>(DTO);

				tasks.RequestedById = currentUser.Id;

				tasks.RequestedForId = user.Id;

				tasks.TaskJobId = newTask.Id;

				var activityLog = new ActivityLog()
				{
					TaskId = tasks.Id,
					UserId = user.Id,
					Activity = $"Task created by {currentUser.DispalyName} at {DateTime.Now}"
				};
				taskJobs.Add(newTask);
				userTasks.Add(tasks);
				activityLogs.Add(activityLog);
			}
		    await _db.TaskJobs.AddRangeAsync(taskJobs);
			await _db.Tasks.AddRangeAsync(userTasks);
			await _db.ActivityLogs.AddRangeAsync(activityLogs);
			await _unitOfWork.SaveAsync();

			return new APIResponse<List<Guid>>
			{
				IsSuccess = true,
				Message = ResponseMessage.IsSuccess,
				Result = null
			};
		}

		public async Task<APIResponse<List<UpdateTaskDTO>>> UpdateTask(UpdateTaskDTO DTO)
		{
			TaskJob currentTask = await _unitOfWork.TaskJob
				.GetAsync(u => u.OrganizationId == DTO.OrganizationId && u.Id == DTO.TaskJobId);

			IEnumerable<UserTask> userTask = await _unitOfWork.UserTask.GetAllAsync(u => u.TaskJobId == DTO.TaskJobId);

			var currentUser = await _unitOfWork.User.GetAsync(u => u.Id == DTO.RequestedBy);

			if (currentUser is null)
			{

				throw new NotFoundException($"{nameof(User)} {ResponseMessage.NotFound}");
			}

			foreach(var task in userTask)
			{
				var activityLog = new ActivityLog()
				{
					TaskId = currentTask.Id,
					UserId = task.RequestedForId,
					Activity = $"Task Updated by {currentUser.DispalyName} at {DateTime.Now}"
				};

				await _unitOfWork.ActivityLog.CreateAsync(activityLog);
			}

			currentTask.TaskName = DTO.TaskName ?? currentTask.TaskName;
			currentTask.TaskDescription = DTO.TaskDescription ?? currentTask.TaskDescription;
			currentTask.TaskList = DTO.TaskList ?? currentTask.TaskList;
			currentTask.DueDate = DTO.DueDate ?? currentTask.DueDate;
			currentTask.StartDate = DTO.StartDate ?? currentTask.StartDate;

			_unitOfWork.TaskJob.Update(currentTask);

			
			await _unitOfWork.SaveAsync();

			return new APIResponse<List<UpdateTaskDTO>>
			{
				IsSuccess = true,
				Message = ResponseMessage.IsSuccess,
				Result = null
			};

		}

		public async Task<APIResponse<List<UpdateTaskWithRequestedFor>>> UpdateTaskWithRequestedFor(UpdateTaskWithRequestedFor DTO)
		{
			IEnumerable<TaskJob> taskJobs;

			IEnumerable<UserTask> userTask = null;

			var currentUser = await _unitOfWork.User.GetAsync(u => u.Id == DTO.RequestedBy);

			if (currentUser is null)
			{

				throw new NotFoundException($"{nameof(User)} {ResponseMessage.NotFound}");
			}

			if (DTO.isGeneral)
			{
				userTask = await _unitOfWork.UserTask.GetAllAsync(u => u.TaskJobId == DTO.TaskJobId);

				var userIdsWithTask = userTask.Select(ut => ut.RequestedForId);

				var usersWithoutTask = await _db.Users
					.Where(u => !userIdsWithTask.Contains(u.Id) && u.OrganizationId == DTO.OrganizationId)
					.ToListAsync();

				TaskJob singleTaskJob = await _unitOfWork.TaskJob
					.GetAsync(u => u.OrganizationId == DTO.OrganizationId && u.Id == DTO.TaskJobId);

				foreach (var user in usersWithoutTask)
				{
					var newUserTask = new UserTask
					{
						RequestedById = DTO.RequestedBy,
						RequestedForId = user.Id,
						TaskJobId = singleTaskJob.Id,
						OrganizationId = DTO.OrganizationId,
						TaskStatus = TaskStatus.Incomplete
					};

					var activityLog = new ActivityLog()
					{
						TaskId = singleTaskJob.Id,
						UserId = user.Id,
						Activity = $"Task created by {currentUser.DispalyName} at {DateTime.Now}"
					};

					await _unitOfWork.UserTask.CreateAsync(newUserTask);
					await _unitOfWork.ActivityLog.CreateAsync(activityLog);
				}
			}
			else
			{
				TaskJob taskJob = await _unitOfWork.TaskJob.GetAsync(u => u.Id == DTO.TaskJobId);

				IEnumerable<User> users = await _unitOfWork.User.GetAllAsync(u => DTO.RequestedFor.Contains(u.Id));

				userTask = await _unitOfWork.UserTask
					.GetAllAsync(u => u.TaskJobId == DTO.TaskJobId);


				var requestedFor = DTO.RequestedFor;

				//create for these guys only
				var createFor = DTO.RequestedFor.Except(userTask.Select(x => x.RequestedForId));

				var deleteFor = userTask.Select(x => x.RequestedForId).Except(requestedFor);

				
				if (deleteFor.Any())
				{
					var idsString = string.Join(",", deleteFor.Select(x => $"'{x}'"));


					var sql = $"DELETE FROM Tasks WHERE RequestedForId IN ({idsString})";

					_db.Database.ExecuteSqlRaw(sql);
				}

			   users = await _unitOfWork.User.GetAllAsync(u => createFor.Contains(u.Id));


				foreach (var user in users)
				{
					UserTask tasks;

					tasks = _mapper.Map<UserTask>(DTO);

					tasks.RequestedById = currentUser.Id;

					tasks.RequestedForId = user.Id;

					tasks.TaskJobId = taskJob.Id;

					tasks.TaskStatus = TaskStatus.Incomplete;

					var activityLog = new ActivityLog()
					{
						TaskId = tasks.Id,
						UserId = user.Id,
						Activity = $"Task created by {currentUser.DispalyName} at {DateTime.Now}"
					};

					await _unitOfWork.UserTask.CreateAsync(tasks);
					await _unitOfWork.ActivityLog.CreateAsync(activityLog);
				}

			}

			/*foreach (var taskJob in taskJobs)
			{
				var userTaskId = userTask.FirstOrDefault(u => u.TaskJobId == taskJob.Id);

				var activityLog = new ActivityLog()
				{
					TaskId = userTaskId.Id,
					UserId = userTaskId.RequestedForId,
					Activity = $"Task Updated by {currentUser.DispalyName} at {DateTime.Now}"
				};

				taskJob.TaskName = DTO.TaskName ?? taskJob.TaskName;
				taskJob.TaskDescription = DTO.TaskDescription ?? taskJob.TaskDescription;
				taskJob.TaskList = DTO.TaskList ?? taskJob.TaskList;
				taskJob.DueDate = DTO.DueDate ?? taskJob.DueDate;
				taskJob.StartDate = DTO.StartDate ?? taskJob.StartDate;

				_unitOfWork.TaskJob.Update(taskJob);

				await _unitOfWork.ActivityLog.CreateAsync(activityLog);

				await _unitOfWork.SaveAsync();
			}*/

			await _unitOfWork.SaveAsync();

			return new APIResponse<List<UpdateTaskWithRequestedFor>>
			{
				IsSuccess = true,
				Message = ResponseMessage.IsSuccess,
				Result = null
			};

		}

		public async Task<APIResponse<string>> ToogleStatus(ToogleStatusDTO DTO)
		{
			UserTask userTask = await _unitOfWork.UserTask.GetAsync(u => u.Id == DTO.TaskId);

			User user = await _unitOfWork.User.GetAsync(u => u.Id == userTask.RequestedForId);

			if (userTask is null)
			{
				throw new NotFoundException($"{nameof(UserTask)} {ResponseMessage.NotFound}");
			}

			if (user is null)
			{
				throw new NotFoundException($"{nameof(User)} {ResponseMessage.NotFound}");
			}

			userTask.TaskStatus = DTO.TaskStatus;

			var activityLog = new ActivityLog()
			{
				TaskId = userTask.Id,
				UserId = userTask.RequestedForId,
				Activity = $"Task Status was updated by {user.DispalyName} at {DateTime.Now}"
			};

			_unitOfWork.UserTask.Update(userTask);
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
				.GetAllAsync(u => u.RequestedForId == DTO.UserId);

			IEnumerable<Guid> taskIds = task.Select(u => u.TaskJobId);

			IEnumerable<TaskJob> taskJobs = await _unitOfWork.TaskJob
				.GetAllAsync(u => taskIds.Contains(u.Id), pageNumber: DTO.PageNumber, pageSize: DTO.PageSize);

			var mappedList = _mapper.Map<List<GetUserTaskDTO>>(taskJobs);

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
			TaskJob taskJob = await _unitOfWork.TaskJob.GetAsync(u => u.Id == task.TaskJobId);

			if (task is null)
			{
				throw new NotFoundException($"{nameof(UserTask)} {ResponseMessage.NotFound}");
			}

			var mappedList = _mapper.Map<GetUserTaskDTO>(taskJob);

			return new APIResponse<GetUserTaskDTO>
			{
				IsSuccess = true,
				Message = ResponseMessage.IsSuccess,
				Result = mappedList
			};

		}
	}

}


//if (entityIds.Any())
//{
//	var parameterNames = entityIds.Select((_, index) => $"@p{index}").ToArray();
//	var parameters = entityIds.Select((id, index) => new SqlParameter(parameterNames[index], id)).ToArray();

//	var tableName = _context.Model.FindEntityType(typeof(TEntity)).GetTableName();
//	var idColumnName = _context.Model.FindEntityType(typeof(TEntity)).FindPrimaryKey().Properties.First().Name;

//	var sql = $"DELETE FROM {tableName} WHERE {idColumnName} IN ({string.Join(",", parameterNames)})";

//	_context.Database.ExecuteSqlRaw(sql, parameters);
//}