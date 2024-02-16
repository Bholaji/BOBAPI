using AutoMapper;
using Bob.Core.Services.IServices;
using Bob.DataAccess.Repository.IRepository;
using Bob.Model;
using Bob.Model.DTO;
using Bob.Model.DTO.UserDTO;
using Bob.Model.Entities;
using Microsoft.Extensions.Logging;

namespace Bob.Core.Services
{
	public class UserService : IUserService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly ILogger<UserService> _logger;
		// public DbSet<User> _userDb { get; set; }
		public UserService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UserService> logger)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_logger = logger;
		}

		public async Task<APIResponse<List<UserResponseDTO>>> GetUsers(int pageNumber = 1, int pageSize = 0)
		{
			IEnumerable<User> users = await _unitOfWork.User.GetAllAsync(pageSize: pageSize, pageNumber: pageNumber);

			return new APIResponse<List<UserResponseDTO>>
			{
				IsSuccess = true,
				Message = ResponseMessage.IsSuccess,
				Result = _mapper.Map<List<UserResponseDTO>>(users)
			};
		}

		public async Task<APIResponse<UserResponseDTO>> GetUser(Guid id)
		{
			var user = await _unitOfWork.User.GetAsync(u => u.Id == id);

			if (user == null)
			{
				return new APIResponse<UserResponseDTO>
				{
					IsSuccess = false,
					Message = ResponseMessage.IsError,
					Result = default
				};
			}


			return new APIResponse<UserResponseDTO>
			{
				IsSuccess = true,
				Message = ResponseMessage.IsSuccess,
				Result = _mapper.Map<UserResponseDTO>(user)

			};
		}

		public async Task<APIResponse<UserCompositeDTO>> CreateUser(UserCompositeDTO userCompositeDTO)
		{
			User user = _mapper.Map<User>(userCompositeDTO.User);
			var today = DateTime.Now;
			user.CreationDate = today;
			user.ModificationDate = today;
			user.SetFullName();
			var employeeId = await GetEmployeeId();

			_unitOfWork.BeginTransaction();

			await _unitOfWork.User.CreateAsync(user);

			UserAddress userAddress;
			UserPayroll userpayroll;
			UserSocial userSocial;
			UserFinancial userFinancial;
			UserContact userContact;
			UserEmploymentInformation userEmploymentInformation;



			if (user.UserAddress is null)
			{
				userAddress = _mapper.Map<UserAddress>(userCompositeDTO.UserAddress);
				userAddress.UserId = user.Id;
				userAddress.OrganizationId = user.OrganizationId;
				await _unitOfWork.Address.CreateAsync(userAddress);
			}

			if (user.UserPayroll is null)
			{
				userpayroll = _mapper.Map<UserPayroll>(userCompositeDTO.UserPayroll);
				userpayroll.UserId = user.Id;
				userpayroll.OrganizationId = user.OrganizationId;
				await _unitOfWork.Payroll.CreateAsync(userpayroll);
			}

			if (user.UserSocial is null)
			{
				userSocial = _mapper.Map<UserSocial>(userCompositeDTO.UserSocial);
				userSocial.UserId = user.Id;
				userSocial.OrganizationId = user.OrganizationId;
				await _unitOfWork.Social.CreateAsync(userSocial);
			}

			if (user.UserFinancial is null)
			{
				userFinancial = _mapper.Map<UserFinancial>(userCompositeDTO.UserFinancial);
				userFinancial.UserId = user.Id;
				userFinancial.OrganizationId = user.OrganizationId;
				await _unitOfWork.Financial.CreateAsync(userFinancial);
			}

			if (user.userContact is null)
			{
				userContact = _mapper.Map<UserContact>(userCompositeDTO.UserContact);
				userContact.UserId = user.Id;
				userContact.OrganizationId = user.OrganizationId;
				await _unitOfWork.Contact.CreateAsync(userContact);
			}

			if (user.UserEmploymentInformation is null)
			{
				userEmploymentInformation = _mapper.Map<UserEmploymentInformation>(userCompositeDTO.UserEmploymentInformation);
				userEmploymentInformation.UserId = user.Id;
				userEmploymentInformation.OrganizationId = user.OrganizationId;
				userEmploymentInformation.EmployeeID = employeeId;
				await _unitOfWork.EmploymentInformation.CreateAsync(userEmploymentInformation);
			}

			await _unitOfWork.SaveAsync();

			_unitOfWork.CommitTransaction();

			var resultDTO = new UserCompositeDTO
			{
				User = _mapper.Map<UserRequestDTO>(user)
			};


			return new APIResponse<UserCompositeDTO>
			{
				IsSuccess = true,
				Message = ResponseMessage.IsSuccess,
				Result = resultDTO
			};
		}

		private async Task<int> GetEmployeeId()
		{
			int employeeId = await _unitOfWork.User.CountAsync();

			var users = await _unitOfWork.User.GetAllAsync();

			if (employeeId != 0)
			{
				return employeeId + 1;
			}
			else
			{
				return 1;
			}
		}
		public async Task<APIResponse<UpdateUserRequest>> UpdateUser(Guid id, UpdateUserRequest userCompositeDTO)
		{
			User oldUser = await _unitOfWork.User.GetAsync(x => x.Id == id);

			oldUser.FirstName = userCompositeDTO.FirstName ?? oldUser.FirstName;
			oldUser.Surname = userCompositeDTO.Surname ?? oldUser.Surname;
			oldUser.FullName = userCompositeDTO.FullName ?? oldUser.FullName;
			oldUser.DispalyName = userCompositeDTO.DispalyName ?? oldUser.DispalyName;
			oldUser.MiddleName = userCompositeDTO.MiddleName ?? oldUser.MiddleName;
			oldUser.Email = userCompositeDTO.Email ?? oldUser.Email;
			oldUser.Prefix = userCompositeDTO.Prefix ?? oldUser.Prefix;
			oldUser.Pronouns = userCompositeDTO.Pronouns ?? oldUser.Pronouns;
			oldUser.Nationality1 = userCompositeDTO.Nationality1 ?? oldUser.Nationality1;
			oldUser.Nationality2 = userCompositeDTO.Nationality2 ?? oldUser.Nationality2;
			oldUser.Language1 = userCompositeDTO.Language1 ?? oldUser.Language1;
			oldUser.Language2 = userCompositeDTO.Language2 ?? oldUser.Language2;
			oldUser.DateOfBirth = userCompositeDTO.DateOfBirth ?? oldUser.DateOfBirth;
			oldUser.OrganizationId = userCompositeDTO.OrganizationId ?? oldUser.OrganizationId;
			oldUser.RoleId = userCompositeDTO.RoleId ?? oldUser.RoleId;

			_unitOfWork.User.UpdateAsync(oldUser);
			await _unitOfWork.SaveAsync();


			return new APIResponse<UpdateUserRequest>
			{
				IsSuccess = true,
				Message = ResponseMessage.IsSuccess,
				Result = _mapper.Map<UpdateUserRequest>(oldUser)
			};
		}
		public async Task<APIResponse<UserAddressDTO>> UpdateAddress(Guid id, UserAddressDTO userCompositeDTO)
		{
			var userAddress = await _unitOfWork.Address.GetAsync(u => u.Id == id);

			userAddress.AddressLine1 = userCompositeDTO.AddressLine1 ?? userAddress.AddressLine1;
			userAddress.AddressLine2 = userCompositeDTO.AddressLine2 ?? userAddress.AddressLine2;
			userAddress.City = userCompositeDTO.City ?? userAddress.City;
			userAddress.PostalCode = userCompositeDTO.PostalCode ?? userAddress.PostalCode;
			userAddress.Country = userCompositeDTO.Country ?? userAddress.Country;
			userAddress.State = userCompositeDTO.State ?? userAddress.State;
			userAddress.ModifiedBy = userCompositeDTO.ModifiedBy ?? userAddress.ModifiedBy;
			userAddress.OrganizationId = userCompositeDTO.OrganizationId ?? userAddress.OrganizationId;
			_unitOfWork.Address.UpdateAsync(userAddress);
			await _unitOfWork.SaveAsync();


			return new APIResponse<UserAddressDTO>
			{
				IsSuccess = true,
				Message = ResponseMessage.IsSuccess,
				Result = _mapper.Map<UserAddressDTO>(userAddress)
			};
		}

		public async Task<APIResponse<UserPayrollDTO>> UpdatePayroll(Guid id, UserPayrollDTO userCompositeDTO)
		{
			var userpayroll = await _unitOfWork.Payroll.GetAsync(u => u.Id == id);

			userpayroll.EffectiveDate = userCompositeDTO.EffectiveDate ?? userpayroll.EffectiveDate;
			userpayroll.BaseSalary = userCompositeDTO.BaseSalary ?? userpayroll.BaseSalary;
			userpayroll.SalaryPayPeriod = userCompositeDTO.SalaryPayPeriod ?? userpayroll.SalaryPayPeriod;
			userpayroll.SalaryPayFrequency = userCompositeDTO.SalaryPayFrequency ?? userpayroll.SalaryPayFrequency;
			userpayroll.OrganizationId = userCompositeDTO.OrganizationId ?? userpayroll.OrganizationId;
			_unitOfWork.Payroll.UpdateAsync(userpayroll);
			await _unitOfWork.SaveAsync();

			return new APIResponse<UserPayrollDTO>
			{
				IsSuccess = true,
				Message = ResponseMessage.IsSuccess,
				Result = _mapper.Map<UserPayrollDTO>(userpayroll)
			};
		}
		public async Task<APIResponse<UserSocialDTO>> UpdateSocial(Guid id, UserSocialDTO userCompositeDTO)
		{
			var userSocial = await _unitOfWork.Social.GetAsync(u => u.Id == id);

			userSocial.About = userCompositeDTO.About ?? userSocial.About;
			userSocial.Socials = userCompositeDTO.Socials ?? userSocial.Socials;
			userSocial.Hobbies = userCompositeDTO.Hobbies ?? userSocial.Hobbies;
			userSocial.Superpowers = userCompositeDTO.Superpowers ?? userSocial.Superpowers;
			userSocial.FoodPrefrence = userCompositeDTO.FoodPrefrence ?? userSocial.FoodPrefrence;
			userSocial.OrganizationId = userCompositeDTO.OrganizationId ?? userSocial.OrganizationId;
			_unitOfWork.Social.UpdateAsync(userSocial);
			await _unitOfWork.SaveAsync();

			return new APIResponse<UserSocialDTO>
			{
				IsSuccess = true,
				Message = ResponseMessage.IsSuccess,
				Result = _mapper.Map<UserSocialDTO>(userSocial)
			};
		}
		public async Task<APIResponse<UserFinancialDTO>> UpdateFinancial(Guid id, UserFinancialDTO userCompositeDTO)
		{
			var userFinancial = await _unitOfWork.Financial.GetAsync(u => u.Id == id);

			userFinancial.AccountName = userCompositeDTO.AccountName ?? userFinancial.AccountName;
			userFinancial.RatingNumber = userCompositeDTO.RatingNumber ?? userFinancial.RatingNumber;
			userFinancial.AccountNumber = userCompositeDTO.AccountNumber ?? userFinancial.AccountNumber;
			userFinancial.BankName = userCompositeDTO.BankName ?? userFinancial.BankName;
			userFinancial.BankAccountType = userCompositeDTO.BankAccountType ?? userFinancial.BankAccountType;
			userFinancial.BankAddress = userCompositeDTO.BankAddress ?? userFinancial.BankAddress;
			userFinancial.OrganizationId = userCompositeDTO.OrganizationId ?? userFinancial.OrganizationId;
			_unitOfWork.Financial.UpdateAsync(userFinancial);
			await _unitOfWork.SaveAsync();


			return new APIResponse<UserFinancialDTO>
			{
				IsSuccess = true,
				Message = ResponseMessage.IsSuccess,
				Result = _mapper.Map<UserFinancialDTO>(userFinancial)
			};
		}
		public async Task<APIResponse<UserContactDTO>> UpdateContact(Guid id, UserContactDTO userCompositeDTO)
		{
			var userContact = await _unitOfWork.Contact.GetAsync(u => u.Id == id);

			userContact.PersonalEmail = userCompositeDTO.PersonalEmail ?? userContact.PersonalEmail;
			userContact.PhoneNumber = userCompositeDTO.PhoneNumber ?? userContact.PhoneNumber;
			userContact.MobileNumber = userCompositeDTO.MobileNumber ?? userContact.MobileNumber;
			userContact.PassportNumber = userCompositeDTO.PassportNumber ?? userContact.PassportNumber;
			userContact.NationalId = userCompositeDTO.NationalId ?? userContact.NationalId;
			userContact.SSN = userCompositeDTO.SSN ?? userContact.SSN;
			userContact.TaxIdNumber = userCompositeDTO.TaxIdNumber ?? userContact.TaxIdNumber;
			userContact.OrganizationId = userCompositeDTO.OrganizationId ?? userContact.OrganizationId;


			_unitOfWork.Contact.UpdateAsync(userContact);
			await _unitOfWork.SaveAsync();


			return new APIResponse<UserContactDTO>
			{
				IsSuccess = true,
				Message = ResponseMessage.IsSuccess,
				Result = _mapper.Map<UserContactDTO>(userContact)
			};
		}
		public async Task<APIResponse<UserEmploymentInformationDTO>> UpdateEmploymentInformation(Guid id, UserEmploymentInformationDTO userCompositeDTO)
		{
			var userEmploymentInformation = await _unitOfWork.EmploymentInformation.GetAsync(u => u.Id == id);

			userEmploymentInformation.EffectiveDate = userCompositeDTO.EffectiveDate ?? userEmploymentInformation.EffectiveDate;
			userEmploymentInformation.EmploymentDate = userCompositeDTO.EmploymentDate ?? userEmploymentInformation.EmploymentDate;
			userEmploymentInformation.Type = userCompositeDTO.Type ?? userEmploymentInformation.Type;
			userEmploymentInformation.WeeklyHours = userCompositeDTO.WeeklyHours ?? userEmploymentInformation.WeeklyHours;
			userEmploymentInformation.WorkingPattern = userCompositeDTO.WorkingPattern ?? userEmploymentInformation.WorkingPattern;
			userEmploymentInformation.OrganizationId = userCompositeDTO.OrganizationId ?? userEmploymentInformation.OrganizationId;
			userEmploymentInformation.DepartmentId = userCompositeDTO.DepartmentId ?? userEmploymentInformation.DepartmentId;


			_unitOfWork.EmploymentInformation.UpdateAsync(userEmploymentInformation);
			await _unitOfWork.SaveAsync();

			return new APIResponse<UserEmploymentInformationDTO>
			{
				IsSuccess = true,
				Message = ResponseMessage.IsSuccess,
				Result = _mapper.Map<UserEmploymentInformationDTO>(userEmploymentInformation)
			};
		}
	}
}
