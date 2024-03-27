using AutoMapper;
using Bob.Core.Exceptions;
using Bob.Core.Services.IServices;
using Bob.Core.Strategy;
using Bob.DataAccess.Repository.IRepository;
using Bob.Migrations.Data;
using Bob.Model;
using Bob.Model.DTO.LeaveDTO;
using Bob.Model.Entities;
using Bob.Model.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Bob.Core.Services
{
	public class LeaveRequestService: ILeaveRequestService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly ILogger<LeaveRequestService> _logger;
		private readonly ApplicationDbContext _db;
		public LeaveRequestService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<LeaveRequestService> logger, ApplicationDbContext db)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_logger = logger;
			_db = db;
		}

		public async Task<APIResponse<string>> ToogleStatusApproval(LeaveApprovalDTO DTO)
		{
			var manager =  _db.Managers.Where(u => u.Id == DTO.ManagerId).FirstOrDefault();
			
			if (manager is null)
			{
				throw new NotFoundException($"{nameof(Manager)} {ResponseMessage.NotFound}");
			}

			var user = await _unitOfWork.User.GetAsync(u => u.Id == DTO.RequesterId);

			if (user is null)
			{
				throw new NotFoundException($"{nameof(User)} {ResponseMessage.NotFound}");
			}

			var leaveRequest = _db.LeaveRequests.Where(u => u.RequesterId == DTO.RequesterId).FirstOrDefault();

			leaveRequest.LeaveRequestStatus = DTO.LeaveRequestStatus;

			leaveRequest.ApprovedBy = manager.User.DispalyName; 

			_db.LeaveRequests.Update(leaveRequest);

			await _db.SaveChangesAsync();

			return new APIResponse<string>()
			{
				IsSuccess = true,
				Message = ResponseMessage.IsSuccess,
				Result = "Status Changed"
			};

		}

		public async Task<APIResponse<string>> RequestLeave(LeaveRequestDTO DTO)
		{
			var user = await _unitOfWork.User.GetAsync(u => u.Id == DTO.RequesterId);

			if (user is null)
			{
				throw new NotFoundException($"{nameof(User)} {ResponseMessage.NotFound}");
			}

			var leaveDaysAccural =  await _db.LeaveDaysAccurals
				.Where(u => u.UserId == user.Id && u.ActivityType == DTO.LeavePolicy).FirstOrDefaultAsync();

			if (leaveDaysAccural is null)
			{
				throw new NotFoundException($"{nameof(LeaveDaysAccural)} {ResponseMessage.NotFound}");
			}

			double totalLeaveDays = leaveDaysAccural.Amount;

			LeaveRequest leaveRequest = _mapper.Map<LeaveRequest>(DTO);

			var numberOfDaysRequested = Math.Ceiling((leaveRequest.EndDate - leaveRequest.StartDate).TotalDays + 1);
			
			if (numberOfDaysRequested > totalLeaveDays)
			{
				return new APIResponse<string>()
				{
					IsSuccess = false,
					Message = "Insufficient leave balance. Please choose fewer days or check your leave balance.",
					Result = null
				};
			}

			ILeaveRequestStrategy strategy;
			

			if (DTO.StartDate.Date == DTO.EndDate.Date)
			{
				strategy = new SameDayStrategy();
			}
			else if (DTO.Duration1 == LeaveRequestDuration.All_Day)
			{
				strategy = new AllDayStrategy();
			}
			else // Duration1 == Half_Day
			{
				strategy = new HalfDayStrategy();
			}

			await strategy.HandleRequest(DTO, leaveRequest, numberOfDaysRequested);

			leaveRequest.LeaveRequestStatus = LeaveRequestStatus.pending;

			await _db.LeaveRequests.AddAsync(leaveRequest);
			await _db.SaveChangesAsync();

			return new APIResponse<string>()
			{
				IsSuccess = true,
				Message = ResponseMessage.IsSuccess,
				Result = "Leave Request submitted successfully"
			};
		}

		public async Task<APIResponse<string>> EditRequestLeave(EditRequestLeaveDTO DTO)
		{
			var leaveRequest = _db.LeaveRequests
				.Where(u => u.Id == DTO.LeaveRequestId && u.RequesterId == DTO.RequesterId).FirstOrDefault();

			if (leaveRequest is null)
			{
				throw new NotFoundException($"{nameof(LeaveRequest)} {ResponseMessage.NotFound}");
			}

			var leaveDaysAccural = await _db.LeaveDaysAccurals
				.Where(u => u.UserId == leaveRequest.RequesterId && u.ActivityType == leaveRequest.LeavePolicy).FirstOrDefaultAsync();

			if (leaveDaysAccural is null)
			{
				throw new NotFoundException($"{nameof(LeaveDaysAccural)} {ResponseMessage.NotFound}");
			}

			double totalLeaveDays = leaveDaysAccural.Amount;

			var numberOfDaysRequested = Math.Ceiling((leaveRequest.EndDate - leaveRequest.StartDate).TotalDays + 1);

			if (numberOfDaysRequested > totalLeaveDays)
			{
				return new APIResponse<string>()
				{
					IsSuccess = false,
					Message = "Insufficient leave balance. Please choose fewer days or check your leave balance.",
					Result = null
				};
			}

			IEditLeaveRequestStrategy strategy;

			if (DTO.StartDate == DTO.EndDate)
			{
				strategy = new EditSameDayStrategy();
			}
			else if (DTO.Duration1 == LeaveRequestDuration.All_Day)
			{
				strategy = new EditAllDayStrategy();
			}
			else // Duration1 == Half_Day
			{
				strategy = new EditHalfDayStrategy();
			}

			await strategy.HandleEdit(DTO, leaveRequest);

			_db.LeaveRequests.Update(leaveRequest);
			await _db.SaveChangesAsync();

			return new APIResponse<string>()
			{
				IsSuccess = true,
				Message = ResponseMessage.IsSuccess,
				Result = "Leave Request edited successfully"
			};
		}

		public async Task<APIResponse<List<GetCarryOverActivityDTO>>> GetCarryOverActivityForAUser(Guid userId)
		{
			var user = await _unitOfWork.User.GetAsync(u => u.Id == userId);

			if (user is null)
			{
				throw new NotFoundException($"{nameof(User)} {ResponseMessage.NotFound}");
			}

			var carryOver = _db.CarryOverActivities.Where(u => u.UserId == userId).ToList();

			return new APIResponse<List<GetCarryOverActivityDTO>>
			{
				IsSuccess = true,
				Message = ResponseMessage.IsSuccess,
				Result = _mapper.Map<List<GetCarryOverActivityDTO>>(carryOver)
			};
		}

		public async Task<APIResponse<List<GetLeaveRequestDTO>>> GetLeaveRequestForAUser(Guid userId)
		{
			var user = await _unitOfWork.User.GetAsync(u => u.Id == userId);

			if (user is null)
			{
				throw new NotFoundException($"{nameof(User)} {ResponseMessage.NotFound}");
			}
			var leaveRequest = _db.LeaveRequests.Where(u => u.RequesterId == userId).ToList();

			if (leaveRequest is null)
			{
				throw new NotFoundException($"{nameof(LeaveRequest)} {ResponseMessage.NotFound}");
			}

			return new APIResponse<List<GetLeaveRequestDTO>>
			{
				IsSuccess = true,
				Message = ResponseMessage.IsSuccess,
				Result = _mapper.Map<List<GetLeaveRequestDTO>>(leaveRequest)
			};
		}

		public async Task<APIResponse<GetCarryOverActivityDTO>> GetLeaveBalnceBasedOnActivityType(GetCarryOverActivityRequestDTO DTO)
		{
			var user = await _unitOfWork.User.GetAsync(u => u.Id == DTO.UserId);

			if (user is null)
			{
				throw new NotFoundException($"{nameof(User)} {ResponseMessage.NotFound}");
			}

			var carryOver = _db.CarryOverActivities.Where(u => u.UserId == DTO.UserId && u.ActivityType == DTO.LeavePolicy).FirstOrDefault();

			return new APIResponse<GetCarryOverActivityDTO>
			{
				IsSuccess = true,
				Message = ResponseMessage.IsSuccess,
				Result = _mapper.Map<GetCarryOverActivityDTO>(carryOver)
			};

		}

		public async Task<APIResponse<GetLeaveDaysAccuralDTO>> GetLeaveDaysAccuralBasedOnActivityType(GetCarryOverActivityRequestDTO DTO)
		{
			var user = await _unitOfWork.User.GetAsync(u => u.Id == DTO.UserId);

			if (user is null)
			{
				throw new NotFoundException($"{nameof(User)} {ResponseMessage.NotFound}");
			}

			var carryOver = _db.CarryOverActivities.Where(u => u.UserId == DTO.UserId && u.ActivityType == DTO.LeavePolicy).FirstOrDefault();

			return new APIResponse<GetLeaveDaysAccuralDTO>
			{
				IsSuccess = true,
				Message = ResponseMessage.IsSuccess,
				Result = _mapper.Map<GetLeaveDaysAccuralDTO>(carryOver)
			};

		}
	}
}
