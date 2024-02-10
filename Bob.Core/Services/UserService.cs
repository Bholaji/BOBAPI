using AutoMapper;
using Bob.Core.Services.IServices;
using Bob.DataAccess.Repository.IRepository;
using Bob.Model;
using Bob.Model.DTO;
using Bob.Model.DTO.UserDTO;
using Bob.Model.Entities;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Bob.Core.Services
{
    public class UserService: IUserService
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

		//USERS

        public async Task<APIResponse<UserResponseDTO>> CreateUser(UserRequestDTO userDTO)
        {
            try
            {
				User user = _mapper.Map<User>(userDTO);
				var today = DateTime.Now;
				user.CreationDate = today;
				user.ModificationDate = today;
				user.SetFullName();

				await _unitOfWork.User.CreateAsync(user);

				return new APIResponse<UserResponseDTO>
				{
					IsSuccess = true,
					Message = ResponseMessage.IsSuccess,
					Result = _mapper.Map<UserResponseDTO>(user)
				};
			}
            catch (Exception ex)

            {
				_logger.LogError(ex.Message);
				return new APIResponse<UserResponseDTO>
				{
					IsSuccess = false,
					Message = ResponseMessage.IsError,
					Result = default
				};

			}
           
        }

		public async Task<APIResponse<List<UserResponseDTO>>> GetUsers()
		{
			try
			{
				IEnumerable<User> users = await _unitOfWork.User.GetAllAsync();

				return new APIResponse<List<UserResponseDTO>>
				{
					IsSuccess = true,
					Message = ResponseMessage.IsSuccess,
					Result = _mapper.Map<List<UserResponseDTO>>(users)
				};
			}
			catch (Exception ex)
			{

				_logger.LogError(ex.Message);
				return new APIResponse<List<UserResponseDTO>>
				{
					IsSuccess = false,
					Message = ResponseMessage.IsError,
					Result = default
				};
			}
		}

		public async Task<APIResponse<UserResponseDTO>> GetUser(Guid id)
		{
			try
			{
				var user = await _unitOfWork.User.GetAsync(u => u.Id == id);

				if(user == null)
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
			catch (Exception ex)

			{
				_logger.LogError(ex.Message);
				return new APIResponse<UserResponseDTO>
				{
					IsSuccess = false,
					Message = ResponseMessage.IsError,
					Result = default
				};

			}
		}

		public async Task<APIResponse<UpdateUserDTO>> UpdateUser(Guid id, UpdateUserDTO updateUserDTO)
		{
			try
			{

				User oldUser = await _unitOfWork.User.GetAsync(u => u.Id == id);

				oldUser.FirstName = updateUserDTO.FirstName ?? oldUser.FirstName;
				oldUser.Surname = updateUserDTO.Surname ?? oldUser.Surname;
				oldUser.FullName = updateUserDTO.FullName ?? oldUser.FullName;
				oldUser.DispalyName = updateUserDTO.DispalyName ?? oldUser.DispalyName;
				oldUser.MiddleName = updateUserDTO.MiddleName ?? oldUser.MiddleName;
				oldUser.Email = updateUserDTO.Email ?? oldUser.Email;
				oldUser.Prefix = updateUserDTO.Prefix ?? oldUser.Prefix;
				oldUser.Pronouns = updateUserDTO.Pronouns ?? oldUser.Pronouns;
				oldUser.Nationality1 = updateUserDTO.Nationality1 ?? oldUser.Nationality1;
				oldUser.Nationality2 = updateUserDTO.Nationality2 ?? oldUser.Nationality2;
				oldUser.Language1 = updateUserDTO.Language1 ?? oldUser.Language1;
				oldUser.Language2 = updateUserDTO.Language2 ?? oldUser.Language2;

				oldUser.DateOfBirth = updateUserDTO.DateOfBirth ?? oldUser.DateOfBirth;

				await _unitOfWork.User.UpdateAsync(oldUser);

				return new APIResponse<UpdateUserDTO>
				{
					IsSuccess = true,
					Message = ResponseMessage.IsSuccess,
					Result = _mapper.Map<UpdateUserDTO>(oldUser)
				};

			}
			catch (Exception ex)
			{

				_logger.LogError(ex.Message);
				return new APIResponse<UpdateUserDTO>
				{
					IsSuccess = false,
					Message = ResponseMessage.IsError,
					Result = default
				};
			}
		}

		//ADDRESS

		public async Task<APIResponse<UserAddressDTO>> CreateAddress(Guid id, UserAddressDTO userAddressDTO)
		{
			try
			{
				User user = await _unitOfWork.User.GetAsync(u => u.Id == id);

				UserAddress newUserAddress = _mapper.Map<UserAddress>(userAddressDTO);

				newUserAddress.UserId = user.Id; 
				await _unitOfWork.Address.CreateAsync(newUserAddress);

				return new APIResponse<UserAddressDTO>
				{
					IsSuccess = true,
					Message = ResponseMessage.IsSuccess,
					Result = _mapper.Map<UserAddressDTO>(newUserAddress)
				};

			}
			catch (Exception ex)
			{

				_logger.LogError(ex.Message);
				return new APIResponse<UserAddressDTO>
				{
					IsSuccess = false,
					Message = ResponseMessage.IsError,
					Result = default
				};
			}
		}

		public async Task<APIResponse<List<UserAddressDTO>>> GetAllAddress()
		{
			
			try
			{
				IEnumerable<UserAddress> userAdress =  await _unitOfWork.Address.GetAllAsync();

				return new APIResponse<List<UserAddressDTO>>
				{
					IsSuccess = true,
					Message = ResponseMessage.IsSuccess,
					Result = _mapper.Map<List<UserAddressDTO>>(userAdress)
				};

			}
			catch (Exception ex)
			{

				_logger.LogError(ex.Message);
				return new APIResponse<List<UserAddressDTO>>
				{
					IsSuccess = false,
					Message = ResponseMessage.IsError,
					Result = default
				};
			}
		}

		public async Task<APIResponse<UserAddressDTO>> GetAddress(Guid id)
		{
			try
			{
				UserAddress userAddress = await _unitOfWork.Address.GetAsync(u => u.Id == id);

				return new APIResponse<UserAddressDTO>
				{
					IsSuccess = true,
					Message = ResponseMessage.IsSuccess,
					Result = _mapper.Map<UserAddressDTO>(userAddress)
				};
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				return new APIResponse<UserAddressDTO>
				{
					IsSuccess = false,
					Message = ResponseMessage.IsError,
					Result = default
				};
			}
		}

		public async Task<APIResponse<UserAddressDTO>> UpdateAddress(Guid id, UserAddressDTO userAddressDTO)
		{
			try
			{
				UserAddress userAddress = await _unitOfWork.Address.GetAsync(u => u.Id == id);

				userAddress.AddressLine1 = userAddressDTO.AddressLine1 ?? userAddress.AddressLine1;
				userAddress.AddressLine2 = userAddressDTO.AddressLine2 ?? userAddress.AddressLine2;
				userAddress.City = userAddressDTO.City ?? userAddress.City;
				userAddress.PostalCode = userAddressDTO.PostalCode ?? userAddress.PostalCode;
				userAddress.Country = userAddressDTO.Country ?? userAddress.Country;
				userAddress.State = userAddressDTO.State ?? userAddress.State;
				userAddress.ModifiedBy = userAddressDTO.ModifiedBy ?? userAddress.ModifiedBy;

				await _unitOfWork.Address.UpdateAsync(userAddress);

				return new APIResponse<UserAddressDTO>
				{
					IsSuccess = true,
					Message = ResponseMessage.IsSuccess,
					Result = _mapper.Map<UserAddressDTO>(userAddress)
				};
			}
			catch (Exception ex)
			{

				_logger.LogError(ex.Message);
				return new APIResponse<UserAddressDTO>
				{
					IsSuccess = false,
					Message = ResponseMessage.IsError,
					Result = default
				};
			}
		}

		//CONTACT

		public async Task<APIResponse<UserContactDTO>> CreateContact(Guid id, UserContactDTO userContactDTO)
		{
			try
			{
				User user = await _unitOfWork.User.GetAsync(u => u.Id == id);

				UserContact newContact = _mapper.Map<UserContact>(userContactDTO);

				newContact.UserId = user.Id;
				await _unitOfWork.Contact.CreateAsync(newContact);

				return new APIResponse<UserContactDTO>
				{
					IsSuccess = true,
					Message = ResponseMessage.IsSuccess,
					Result = _mapper.Map<UserContactDTO>(newContact)
				};

			}
			catch (Exception ex)
			{

				_logger.LogError(ex.Message);
				return new APIResponse<UserContactDTO>
				{
					IsSuccess = false,
					Message = ResponseMessage.IsError,
					Result = default
				};
			}
		}

		public async Task<APIResponse<List<UserContactDTO>>> GetAllContact()
		{

			try
			{
				IEnumerable<UserContact> userContact = await _unitOfWork.Contact.GetAllAsync();

				return new APIResponse<List<UserContactDTO>>
				{
					IsSuccess = true,
					Message = ResponseMessage.IsSuccess,
					Result = _mapper.Map<List<UserContactDTO>>(userContact)
				};

			}
			catch (Exception ex)
			{

				_logger.LogError(ex.Message);
				return new APIResponse<List<UserContactDTO>>
				{
					IsSuccess = false,
					Message = ResponseMessage.IsError,
					Result = default
				};
			}
		}

		public async Task<APIResponse<UserContactDTO>> GetContact(Guid id)
		{
			try
			{
				UserContact userContact = await _unitOfWork.Contact.GetAsync(u => u.Id == id);

				return new APIResponse<UserContactDTO>
				{
					IsSuccess = true,
					Message = ResponseMessage.IsSuccess,
					Result = _mapper.Map<UserContactDTO>(userContact)
				};
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				return new APIResponse<UserContactDTO>
				{
					IsSuccess = false,
					Message = ResponseMessage.IsError,
					Result = default
				};
			}
		}

		public async Task<APIResponse<UserContactDTO>> UpdateContact(Guid id, UserContactDTO userContactDTO)
		{
			try
			{
				UserContact userContact = await _unitOfWork.Contact.GetAsync(u => u.Id == id);

				userContact.PersonalEmail = userContactDTO.PersonalEmail ?? userContact.PersonalEmail;
				userContact.PhoneNumber = userContactDTO.PhoneNumber ?? userContact.PhoneNumber;
				userContact.MobileNumber = userContactDTO.MobileNumber ?? userContact.MobileNumber;
				userContact.PassportNumber = userContactDTO.PassportNumber ?? userContact.PassportNumber;
				userContact.NationalId = userContactDTO.NationalId ?? userContact.NationalId;
				userContact.SSN = userContactDTO.SSN ?? userContact.SSN;
				userContact.TaxIdNumber = userContactDTO.TaxIdNumber ?? userContact.TaxIdNumber;

				await _unitOfWork.Contact.UpdateAsync(userContact);

				return new APIResponse<UserContactDTO>
				{
					IsSuccess = true,
					Message = ResponseMessage.IsSuccess,
					Result = _mapper.Map<UserContactDTO>(userContact)
				};
			}
			catch (Exception ex)
			{

				_logger.LogError(ex.Message);
				return new APIResponse<UserContactDTO>
				{
					IsSuccess = false,
					Message = ResponseMessage.IsError,
					Result = default
				};
			}
		}
	}
}
