using AutoMapper;
using Bob.Core.Exceptions;
using Bob.Core.Services.IServices;
using Bob.DataAccess.Repository.IRepository;
using Bob.Model;
using Bob.Model.DTO;
using Bob.Model.DTO.PaginationDTO;
using Bob.Model.DTO.UserDTO;
using Bob.Model.Entities;
using Bob.Model.Entities.Home;
using Microsoft.Extensions.Logging;

namespace Bob.Core.Services
{
	public class UserService : IUserService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly ILogger<UserService> _logger;
		public UserService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UserService> logger)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_logger = logger;
		}

		public async Task<APIResponse<List<UserResponseDTO>>> GetUsers(PaginationDTO DTO)
		{
			IEnumerable<User> users = await _unitOfWork.User.GetAllAsync(pageSize: DTO.PageSize, pageNumber: DTO.PageNumber);

			return new APIResponse<List<UserResponseDTO>>
			{
				IsSuccess = true,
				Message = ResponseMessage.IsSuccess,
				Result = _mapper.Map<List<UserResponseDTO>>(users)
			};
		}
		public async Task<APIResponse<UserResponseDTO>> GetUser(Guid userId)
		{
			var user = await _unitOfWork.User.GetAsync(u => u.Id == userId);

			if (user is null)
			{
				throw new NotFoundException($"{nameof(User)} {ResponseMessage.NotFound}");
			}

			return new APIResponse<UserResponseDTO>
			{
				IsSuccess = true,
				Message = ResponseMessage.IsSuccess,
				Result = _mapper.Map<UserResponseDTO>(user)

			};
		}
		private async Task<int> GenerateEmployeeId(Guid organizationId) => (await _unitOfWork.User.CountAsync(u => u.OrganizationId == organizationId))+ 1;
		public async Task<APIResponse<UserCompositeDTO>> CreateUser(UserCompositeDTO userCompositeDTO)
		{
			User user = _mapper.Map<User>(userCompositeDTO.User);

			var organization = await _unitOfWork.OrganizationRepository.GetAsync(u => u.Id == userCompositeDTO.User.OrganizationId);

			if (organization is null)
			{
				throw new NotFoundException($"{nameof(Organization)} {ResponseMessage.NotFound}");
			}

			var today = DateTime.Now;
			user.CreationDate = today;
			user.ModificationDate = today;
			user.SetFullName();
			var employeeId = await GenerateEmployeeId(user.OrganizationId);

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

		public async Task<APIResponse<UpdateUserRequest>> UpdateUser( UpdateUserRequest DTO)
		{
			User oldUser = await _unitOfWork.User.GetAsync(x => x.Id == DTO.UserId);

			if (oldUser is null)
			{
				throw new NotFoundException($"{nameof(User)} {ResponseMessage.NotFound}");
			}

			oldUser.FirstName = DTO.FirstName ?? oldUser.FirstName;
			oldUser.Surname = DTO.Surname ?? oldUser.Surname;
			oldUser.FullName = DTO.FullName ?? oldUser.FullName;
			oldUser.DispalyName = DTO.DispalyName ?? oldUser.DispalyName;
			oldUser.MiddleName = DTO.MiddleName ?? oldUser.MiddleName;
			oldUser.Email = DTO.Email ?? oldUser.Email;
			oldUser.Prefix = DTO.Prefix ?? oldUser.Prefix;
			oldUser.Pronouns = DTO.Pronouns ?? oldUser.Pronouns;
			oldUser.Nationality1 = DTO.Nationality1 ?? oldUser.Nationality1;
			oldUser.Nationality2 = DTO.Nationality2 ?? oldUser.Nationality2;
			oldUser.Language1 = DTO.Language1 ?? oldUser.Language1;
			oldUser.Language2 = DTO.Language2 ?? oldUser.Language2;
			oldUser.DateOfBirth = DTO.DateOfBirth ?? oldUser.DateOfBirth;
			oldUser.OrganizationId = DTO.OrganizationId ?? oldUser.OrganizationId;
			oldUser.RoleId = DTO.RoleId ?? oldUser.RoleId;

			_unitOfWork.User.UpdateAsync(oldUser);
			await _unitOfWork.SaveAsync();


			return new APIResponse<UpdateUserRequest>
			{
				IsSuccess = true,
				Message = ResponseMessage.IsSuccess,
				Result = _mapper.Map<UpdateUserRequest>(oldUser)
			};
		}
		public async Task<APIResponse<UserAddressDTO>> UpdateAddress(UserAddressDTO DTO)
		{
			var userAddress = await _unitOfWork.Address.GetAsync(u => u.Id == DTO.AddressId);

			if (userAddress is null)
			{
				throw new NotFoundException($"{nameof(UserAddress)} {ResponseMessage.NotFound}");
			}
			userAddress.AddressLine1 = DTO.AddressLine1 ?? userAddress.AddressLine1;
			userAddress.AddressLine2 = DTO.AddressLine2 ?? userAddress.AddressLine2;
			userAddress.City = DTO.City ?? userAddress.City;
			userAddress.PostalCode = DTO.PostalCode ?? userAddress.PostalCode;
			userAddress.Country = DTO.Country ?? userAddress.Country;
			userAddress.State = DTO.State ?? userAddress.State;
			userAddress.ModifiedBy = DTO.ModifiedBy ?? userAddress.ModifiedBy;
			userAddress.OrganizationId = DTO.OrganizationId ?? userAddress.OrganizationId;
			_unitOfWork.Address.UpdateAsync(userAddress);
			await _unitOfWork.SaveAsync();


			return new APIResponse<UserAddressDTO>
			{
				IsSuccess = true,
				Message = ResponseMessage.IsSuccess,
				Result = _mapper.Map<UserAddressDTO>(userAddress)
			};
		}
		public async Task<APIResponse<UserPayrollDTO>> UpdatePayroll(UserPayrollDTO DTO)
		{
			var userpayroll = await _unitOfWork.Payroll.GetAsync(u => u.Id == DTO.PayrollId);

			if (userpayroll is null)
			{
				throw new NotFoundException($"{nameof(UserPayroll)} {ResponseMessage.NotFound}");
			}
			userpayroll.EffectiveDate = DTO.EffectiveDate ?? userpayroll.EffectiveDate;
			userpayroll.BaseSalary = DTO.BaseSalary ?? userpayroll.BaseSalary;
			userpayroll.SalaryPayPeriod = DTO.SalaryPayPeriod ?? userpayroll.SalaryPayPeriod;
			userpayroll.SalaryPayFrequency = DTO.SalaryPayFrequency ?? userpayroll.SalaryPayFrequency;
			userpayroll.OrganizationId = DTO.OrganizationId ?? userpayroll.OrganizationId;
			_unitOfWork.Payroll.UpdateAsync(userpayroll);
			await _unitOfWork.SaveAsync();

			return new APIResponse<UserPayrollDTO>
			{
				IsSuccess = true,
				Message = ResponseMessage.IsSuccess,
				Result = _mapper.Map<UserPayrollDTO>(userpayroll)
			};
		}
		public async Task<APIResponse<UserSocialDTO>> UpdateSocial(UserSocialDTO DTO)
		{
			var userSocial = await _unitOfWork.Social.GetAsync(u => u.Id == DTO.SocialId);

			if (userSocial is null)
			{
				throw new NotFoundException($"{nameof(UserSocial)} {ResponseMessage.NotFound}");
			}
			userSocial.About = DTO.About ?? userSocial.About;
			userSocial.Socials = DTO.Socials ?? userSocial.Socials;
			userSocial.Hobbies = DTO.Hobbies ?? userSocial.Hobbies;
			userSocial.Superpowers = DTO.Superpowers ?? userSocial.Superpowers;
			userSocial.FoodPrefrence = DTO.FoodPrefrence ?? userSocial.FoodPrefrence;
			userSocial.OrganizationId = DTO.OrganizationId ?? userSocial.OrganizationId;
			_unitOfWork.Social.UpdateAsync(userSocial);
			await _unitOfWork.SaveAsync();

			return new APIResponse<UserSocialDTO>
			{
				IsSuccess = true,
				Message = ResponseMessage.IsSuccess,
				Result = _mapper.Map<UserSocialDTO>(userSocial)
			};
		}
		public async Task<APIResponse<UserFinancialDTO>> UpdateFinancial(UserFinancialDTO DTO)
		{
			var userFinancial = await _unitOfWork.Financial.GetAsync(u => u.Id == DTO.FinancialId);

			if (userFinancial is null)
			{
				throw new NotFoundException($"{nameof(UserFinancial)} {ResponseMessage.NotFound}");
			}
			userFinancial.AccountName = DTO.AccountName ?? userFinancial.AccountName;
			userFinancial.RatingNumber = DTO.RatingNumber ?? userFinancial.RatingNumber;
			userFinancial.AccountNumber = DTO.AccountNumber ?? userFinancial.AccountNumber;
			userFinancial.BankName = DTO.BankName ?? userFinancial.BankName;
			userFinancial.BankAccountType = DTO.BankAccountType ?? userFinancial.BankAccountType;
			userFinancial.BankAddress = DTO.BankAddress ?? userFinancial.BankAddress;
			userFinancial.OrganizationId = DTO.OrganizationId ?? userFinancial.OrganizationId;
			_unitOfWork.Financial.UpdateAsync(userFinancial);
			await _unitOfWork.SaveAsync();


			return new APIResponse<UserFinancialDTO>
			{
				IsSuccess = true,
				Message = ResponseMessage.IsSuccess,
				Result = _mapper.Map<UserFinancialDTO>(userFinancial)
			};
		}
		public async Task<APIResponse<UserContactDTO>> UpdateContact(UserContactDTO DTO)
		{
			var userContact = await _unitOfWork.Contact.GetAsync(u => u.Id == DTO.ContactId);

			if (userContact is null)
			{
				throw new NotFoundException($"{nameof(UserContact)} {ResponseMessage.NotFound}");
			}
			userContact.PersonalEmail = DTO.PersonalEmail ?? userContact.PersonalEmail;
			userContact.PhoneNumber = DTO.PhoneNumber ?? userContact.PhoneNumber;
			userContact.MobileNumber = DTO.MobileNumber ?? userContact.MobileNumber;
			userContact.PassportNumber = DTO.PassportNumber ?? userContact.PassportNumber;
			userContact.NationalId = DTO.NationalId ?? userContact.NationalId;
			userContact.SSN = DTO.SSN ?? userContact.SSN;
			userContact.TaxIdNumber = DTO.TaxIdNumber ?? userContact.TaxIdNumber;
			userContact.OrganizationId = DTO.OrganizationId ?? userContact.OrganizationId;


			_unitOfWork.Contact.UpdateAsync(userContact);
			await _unitOfWork.SaveAsync();


			return new APIResponse<UserContactDTO>
			{
				IsSuccess = true,
				Message = ResponseMessage.IsSuccess,
				Result = _mapper.Map<UserContactDTO>(userContact)
			};
		}
		public async Task<APIResponse<UserEmploymentInformationDTO>> UpdateEmploymentInformation(UserEmploymentInformationDTO DTO)
		{
			var userEmploymentInformation = await _unitOfWork.EmploymentInformation.GetAsync(u => u.Id == DTO.EmploymentInformationId);
			if (userEmploymentInformation is null)
			{
				throw new NotFoundException($"{nameof(UserEmploymentInformation)} {ResponseMessage.NotFound}");
			}
			userEmploymentInformation.EffectiveDate = DTO.EffectiveDate ?? userEmploymentInformation.EffectiveDate;
			userEmploymentInformation.EmploymentDate = DTO.EmploymentDate ?? userEmploymentInformation.EmploymentDate;
			userEmploymentInformation.Type = DTO.Type ?? userEmploymentInformation.Type;
			userEmploymentInformation.WeeklyHours = DTO.WeeklyHours ?? userEmploymentInformation.WeeklyHours;
			userEmploymentInformation.WorkingPattern = DTO.WorkingPattern ?? userEmploymentInformation.WorkingPattern;
			userEmploymentInformation.OrganizationId = DTO.OrganizationId ?? userEmploymentInformation.OrganizationId;
			userEmploymentInformation.DepartmentId = DTO.DepartmentId ?? userEmploymentInformation.DepartmentId;


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
