using AutoMapper;
using Bob.Model.DTO;
using Bob.Model.DTO.UserDTO;
using Bob.Model.Entities;

namespace Bob.Core
{
	public class ProfileConfig : Profile
	{
		public ProfileConfig()
		{
			CreateMap<UserResponseDTO, User>().ReverseMap();
			CreateMap<UserRequestDTO, User>().ReverseMap();
			CreateMap<UpdateUserDTO, User>().ReverseMap();

			CreateMap<UserAddressDTO, UserAddress>().ReverseMap();
			CreateMap<UserContactDTO, UserContact>().ReverseMap();
		}

	}
}
