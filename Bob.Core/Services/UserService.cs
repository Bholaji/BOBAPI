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

        public async Task<APIResponse<UserResponseDTO>> CreateUser(UserRequestDTO userDTO)
        {
            try
            {
				User user = _mapper.Map<User>(userDTO);
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
	}
}
