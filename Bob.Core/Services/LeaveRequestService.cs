using AutoMapper;
using Bob.Core.Exceptions;
using Bob.Core.Services.IServices;
using Bob.DataAccess.Repository.IRepository;
using Bob.Migrations.Data;
using Bob.Model;
using Bob.Model.DTO.LeaveDTO;
using Bob.Model.Entities;
using Bob.Model.Enums;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

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
				Result = "Status CHanged"
			};

		}

		public async Task<APIResponse<string>> RequestLeave(LeaveRequestDTO DTO)
		{
			var user = await _unitOfWork.User.GetAsync(u => u.Id == DTO.RequesterId);
			 
			if (user is null)
			{
				throw new NotFoundException($"{nameof(User)} {ResponseMessage.NotFound}");
			}

			LeaveRequest leaveRequest = _mapper.Map<LeaveRequest>(DTO);

			var numberOfDaysRequested = Math.Ceiling((leaveRequest.EndDate - leaveRequest.StartDate).TotalDays + 1);

			// If the start date and end date are the same, use only Duration1
			if (leaveRequest.StartDate.Date == leaveRequest.EndDate.Date)
			{
				leaveRequest.Duration2 = null;
				// Use Duration1
				if (DTO.Duration1 == LeaveRequestDuration.Half_Day)
				{
					// Handle half-day
					numberOfDaysRequested = 0.5;
					leaveRequest.StartDate = leaveRequest.StartDate.Date;
					leaveRequest.EndDate = leaveRequest.StartDate.Date.AddHours(4.5);
					leaveRequest.Duration = LeaveRequestDuration.Half_Day;
				}
				else if (DTO.Duration1 == LeaveRequestDuration.All_Day)
				{
					// Handle full-day
					numberOfDaysRequested = 1;
					leaveRequest.StartDate = leaveRequest.StartDate.Date;
					leaveRequest.EndDate = leaveRequest.EndDate.Date;
					leaveRequest.Duration = LeaveRequestDuration.All_Day;
				}
			}
			// If the start date and end date are different, use Duration1 for start date and Duration2 for end date
			else
			{
				// Use Duration1 for start date
				if (DTO.Duration1 == LeaveRequestDuration.Half_Day)
				{
					leaveRequest.Duration = LeaveRequestDuration.Half_Day;
					// Handle half-day start date
					if (DTO.Duration2 == LeaveRequestDuration.All_Day)
					{
						// Handle full-day end date
						numberOfDaysRequested = numberOfDaysRequested - 0.5;
						leaveRequest.StartDate = leaveRequest.StartDate.Date.AddHours(12);
						leaveRequest.EndDate = leaveRequest.EndDate.Date;
						leaveRequest.Duration2 = LeaveRequestDuration.All_Day;
					}
					else if (DTO.Duration2 == LeaveRequestDuration.Half_Day)
					{
						// Handle half-day end date
						numberOfDaysRequested = numberOfDaysRequested - 1;
						leaveRequest.StartDate = leaveRequest.StartDate.Date.AddHours(12);
						leaveRequest.EndDate = leaveRequest.EndDate.Date.AddHours(12);
						leaveRequest.Duration2 = LeaveRequestDuration.Half_Day;
					}
				}

				else if (DTO.Duration1 == LeaveRequestDuration.All_Day )
				{
					leaveRequest.Duration = LeaveRequestDuration.All_Day;
					// Handle full-day start date
					if (DTO.Duration2 == LeaveRequestDuration.All_Day)
					{
						// Handle full-day end date
						leaveRequest.StartDate = leaveRequest.StartDate.Date;
						leaveRequest.EndDate = leaveRequest.EndDate.Date;
						leaveRequest.Duration2 = LeaveRequestDuration.All_Day;
					}
					else if (DTO.Duration2 == LeaveRequestDuration.Half_Day)
					{
						// Handle half-day end date
						numberOfDaysRequested = numberOfDaysRequested - 0.5;
						leaveRequest.StartDate = leaveRequest.StartDate.Date;
						leaveRequest.EndDate = leaveRequest.EndDate.Date.AddHours(12);
						leaveRequest.Duration2 = LeaveRequestDuration.Half_Day;
					}
				}
			}

			leaveRequest.LeaveRequestStatus = LeaveRequestStatus.pending;
			leaveRequest.DaysRequested = numberOfDaysRequested;

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

			leaveRequest.Duration = DTO.Duration1;

			if (leaveRequest.Duration2 is not null)
			{
				leaveRequest.Duration2 = DTO.Duration2;
			}

			double numberOfDaysRequested = (leaveRequest.EndDate - leaveRequest.StartDate).TotalDays + 1;

			// If the start date and end date are the same, use only Duration1
			if (leaveRequest.StartDate.Date == leaveRequest.EndDate.Date)
			{
				//leaveRequest.Duration2 = null;
				// Use Duration1
				if (leaveRequest.Duration == LeaveRequestDuration.Half_Day)
				{
					// Handle half-day
					numberOfDaysRequested = 0.5;
					leaveRequest.StartDate = leaveRequest.StartDate.Date;
					leaveRequest.EndDate = leaveRequest.StartDate.Date.AddHours(12);
					leaveRequest.Duration = LeaveRequestDuration.Half_Day;
				}
				else if (leaveRequest.Duration == LeaveRequestDuration.All_Day)
				{
					// Handle full-day
					numberOfDaysRequested = 1;
					leaveRequest.StartDate = leaveRequest.StartDate.Date;
					leaveRequest.EndDate = leaveRequest.EndDate.Date;
					leaveRequest.Duration = LeaveRequestDuration.All_Day;
				}
			}
			// If the start date and end date are different, use Duration1 for start date and Duration2 for end date
			else
			{
				// Use Duration1 for start date
				if (DTO.Duration1 == LeaveRequestDuration.Half_Day)
				{
					// Handle half-day start date
					if (DTO.Duration2 == LeaveRequestDuration.All_Day)
					{
						// Handle full-day end date
						leaveRequest.StartDate = leaveRequest.StartDate.Date.AddHours(12);
						leaveRequest.EndDate = leaveRequest.EndDate.Date;
						numberOfDaysRequested = ((leaveRequest.EndDate - leaveRequest.StartDate).TotalDays + 1);
					}
					else if (DTO.Duration2 == LeaveRequestDuration.Half_Day)
					{
						// Handle half-day end date
						
						leaveRequest.StartDate = leaveRequest.StartDate.Date.AddHours(12);
						leaveRequest.EndDate = leaveRequest.EndDate.Date.AddHours(12);
						numberOfDaysRequested = ((leaveRequest.EndDate - leaveRequest.StartDate).TotalDays);
					}
				}

				else if (DTO.Duration1 == LeaveRequestDuration.All_Day)
				{
					// Handle full-day start date
					if (DTO.Duration2 == LeaveRequestDuration.All_Day)
					{
						// Handle full-day end date
						
						leaveRequest.StartDate = leaveRequest.StartDate.Date;
						leaveRequest.EndDate = leaveRequest.EndDate.Date;
						numberOfDaysRequested = ((leaveRequest.EndDate - leaveRequest.StartDate).TotalDays + 1);
					}
					else if (DTO.Duration2 == LeaveRequestDuration.Half_Day)
					{
						// Handle half-day end date
						leaveRequest.StartDate = leaveRequest.StartDate.Date;
						leaveRequest.EndDate = leaveRequest.EndDate.Date.AddHours(12);
						numberOfDaysRequested = ((leaveRequest.EndDate - leaveRequest.StartDate).TotalDays);
					}
				}
			}
			leaveRequest.DaysRequested = numberOfDaysRequested;
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
