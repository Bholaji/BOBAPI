using AutoMapper;
using Bob.Model.DTO;
using Bob.Model.DTO.CommentDTO;
using Bob.Model.DTO.PostDTO;
using Bob.Model.DTO.ShoutoutDTO;
using Bob.Model.DTO.TaskDTO;
using Bob.Model.DTO.UserDTO;
using Bob.Model.Entities;
using Bob.Model.Entities.Home;

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

			CreateMap<UserCompositeDTO, User>().ReverseMap();
			CreateMap<UserCompositeDTO, UserAddress>().ReverseMap();
			CreateMap<UserCompositeDTO, UserContact>().ReverseMap();
			CreateMap<UserCompositeDTO, UserEmploymentInformation>().ReverseMap();
			CreateMap<UserCompositeDTO, UserFinancial>().ReverseMap();
			CreateMap<UserCompositeDTO, UserPayroll>().ReverseMap();
			CreateMap<UserCompositeDTO, UserSocial>().ReverseMap();

			CreateMap<UserSocialDTO, UserSocial>().ReverseMap();
			CreateMap<UserFinancialDTO, UserFinancial>().ReverseMap();
			CreateMap<UserPayrollDTO, UserPayroll>().ReverseMap();
			CreateMap<UserEmploymentInformationDTO, UserEmploymentInformation>().ReverseMap();
			CreateMap<OrganizationDTO, Organization>().ReverseMap();

			CreateMap<CreatePostRequestDTO, Post>().ReverseMap();
			CreateMap<PostResponseDTO, Post>().ReverseMap();
			CreateMap<UpdatePostRequestDTO, Post>().ReverseMap();
			CreateMap<GetPostDTO, Post>().ReverseMap();

			CreateMap<CommentResponseDTO, Comment>().ReverseMap();
			CreateMap<CreateCommentRequestDTO, Comment>().ReverseMap();
			CreateMap<UpdateCommentDTO, Comment>().ReverseMap();
			CreateMap<GetCommentDTO, Comment>().ReverseMap();

			CreateMap<UpdateUserRequest, User>().ReverseMap();

			CreateMap<TaskRequestDTO, UserTask>().ReverseMap();
			CreateMap<ToogleStatusDTO, UserTask>().ReverseMap();
			CreateMap<GetUserTaskDTO, UserTask>().ReverseMap();
			CreateMap<UpdateTaskDTO, UserTask>().ReverseMap();


		}

	}
}
