using AutoMapper;
using Bob.Core.Services.IServices;
using Bob.DataAccess.Repository.IRepository;
using Bob.Model;
using Bob.Model.DTO.CommentDTO;
using Bob.Model.DTO.PostDTO;
using Bob.Model.DTO.ShoutoutDTO;
using Bob.Model.Entities;
using Bob.Model.Entities.Home;
using Microsoft.Extensions.Logging;

namespace Bob.Core.Services
{
	public class PostService: IPostService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly ILogger<PostService> _logger;
		public PostService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<PostService> logger)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_logger = logger;
		}
		public async Task<APIResponse<PostResponseDTO>> CreatePost(CreatePostRequestDTO postRequestDTO)
		{
			try
			{
				User user = await _unitOfWork.User.GetAsync(u => u.Id == postRequestDTO.UserId);
				Post post = _mapper.Map<Post>(postRequestDTO);
				post.OrganizationId = user.OrganizationId;
				post.UserId = user.Id;
				await _unitOfWork.Post.CreateAsync(post);
				await _unitOfWork.SaveAsync();

				var response = new APIResponse<PostResponseDTO>
				{
					IsSuccess = true,
					Message = ResponseMessage.IsSuccess,
					Result = _mapper.Map<PostResponseDTO>(post)
				};

				response.Result.PostId = post.Id;
				return response;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				return new APIResponse<PostResponseDTO>
				{
					IsSuccess = false,
					Message = ResponseMessage.IsError,
					Result = default
				};
			}
		}
		
		public async Task<APIResponse<PostResponseDTO>> UpdatePost(UpdatePostRequestDTO postRequestDTO)
		{
			try
			{
				Post oldPost = await _unitOfWork.Post.GetAsync(u => u.Id == postRequestDTO.UserId);

				oldPost.Title = postRequestDTO.Title ?? oldPost.Title;
				oldPost.Content = postRequestDTO.Content ?? oldPost.Content;
				oldPost.ImageUrl = postRequestDTO.ImageUrl ?? oldPost.ImageUrl;

				_unitOfWork.Post.UpdateAsync(oldPost);

				await _unitOfWork.SaveAsync();

				var response = new APIResponse<PostResponseDTO>
				{
					IsSuccess = true,
					Message = ResponseMessage.IsSuccess,
					Result = _mapper.Map<PostResponseDTO>(oldPost)
				};
				response.Result.PostId = oldPost.Id;
				return response;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				return new APIResponse<PostResponseDTO>
				{
					IsSuccess = false,
					Message = ResponseMessage.IsError,
					Result = default
				};

			}
		}

		public async Task<APIResponse<List<GetPostDTO>>> GetPosts()
		{
			try
			{
				IEnumerable<Post> posts = await _unitOfWork.Post.GetAllAsync();

				return new APIResponse<List<GetPostDTO>>
				{
					IsSuccess = true,
					Message = ResponseMessage.IsSuccess,
					Result = _mapper.Map<List<GetPostDTO>>(posts)
				};
			}
			catch (Exception ex)
			{

				_logger.LogError(ex.Message);
				return new APIResponse<List<GetPostDTO>>
				{
					IsSuccess = false,
					Message = ResponseMessage.IsError,
					Result = default
				};
			}
		}

		public async Task<APIResponse<GetPostDTO>> GetAPost(Guid id)
		{
			try
			{
				Post post = await _unitOfWork.Post.GetAsync(u => u.Id == id);

				return new APIResponse<GetPostDTO>
				{
					IsSuccess = true,
					Message = ResponseMessage.IsSuccess,
					Result = _mapper.Map<GetPostDTO>(post)
				};

			}
			catch (Exception ex)
			{

				_logger.LogError(ex.Message);
				return new APIResponse<GetPostDTO>
				{
					IsSuccess = false,
					Message = ResponseMessage.IsError,
					Result = default
				};
			}
		}

		public async Task<APIResponse<PostResponseDTO>> DeleteAPost(Guid id)
		{
			try
			{
				Post post = await _unitOfWork.Post.GetAsync(u => u.Id == id);
				await _unitOfWork.Post.RemoveAsync(post);
				await _unitOfWork.SaveAsync();

				var response = new APIResponse<PostResponseDTO>
				{
					IsSuccess = true,
					Message = ResponseMessage.IsSuccess,
					Result = _mapper.Map<PostResponseDTO>(post)
				};
				response.Result.PostId = post.Id;
				return response;
			}
			catch (Exception ex)
			{

				_logger.LogError(ex.Message);
				return new APIResponse<PostResponseDTO>
				{
					IsSuccess = false,
					Message = ResponseMessage.IsError,
					Result = default
				};
			}
		}

		//Comment

		public async Task<APIResponse<CommentResponseDTO>> CreateComment(Guid postId, CreateCommentRequestDTO DTO)
		{
			try
			{
				//User user = await _unitOfWork.User.GetAsync(u => u.Id == DTO.UserId);

				Post post = await _unitOfWork.Post.GetAsync(u => u.Id == postId);

				Comment comment = _mapper.Map<Comment>(DTO);
				
				comment.OrganizationId = post.OrganizationId;

				//comment.OrganizationId = user.OrganizationId;

				//comment.UserId = DTO.UserId;

				await _unitOfWork.Comment.CreateAsync(comment);
				await _unitOfWork.SaveAsync();

				var response = new APIResponse<CommentResponseDTO>
				{
					IsSuccess = true,
					Message = ResponseMessage.IsSuccess,
					Result = _mapper.Map<CommentResponseDTO>(comment)
				};

				response.Result.CommentId = comment.Id;
				return response;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				return new APIResponse<CommentResponseDTO>
				{
					IsSuccess = false,
					Message = ResponseMessage.IsError,
					Result = default
				};
			}
		}

		public async Task<APIResponse<CommentResponseDTO>> UpdateComment(UpdateCommentDTO DTO)
		{
			try
			{
				Comment comment = await _unitOfWork.Comment.GetAsync(u => u.Id == DTO.CommentId);

				comment.CommentBody = DTO.CommentBody ?? comment.CommentBody;

				_unitOfWork.Comment.UpdateAsync(comment);

				await _unitOfWork.SaveAsync();

				var response = new APIResponse<CommentResponseDTO>
				{
					IsSuccess = true,
					Message = ResponseMessage.IsSuccess,
					Result = _mapper.Map<CommentResponseDTO>(comment)
				};
				response.Result.CommentId = comment.Id;
				return response;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				return new APIResponse<CommentResponseDTO>
				{
					IsSuccess = false,
					Message = ResponseMessage.IsError,
					Result = default
				};

			}
		}


		public async Task<APIResponse<List<GetCommentDTO>>> GetComment(Guid postId)
		{
			try
			{
				List<Comment> comment;

				Post post = await _unitOfWork.Post.GetAsync(u=>u.Id == postId);

				if(post != null)
				{
					 comment = await _unitOfWork.Comment.GetAllAsync();
					
				}else
				{
					return new APIResponse<List<GetCommentDTO>>
					{
						IsSuccess = false,
						Message = "Post not found",
						Result = default
					};
				}

				return new APIResponse<List<GetCommentDTO>>
					{
						IsSuccess = true,
						Message = ResponseMessage.IsSuccess,
						Result = _mapper.Map<List<GetCommentDTO>>(comment)
					};

			}
			catch (Exception ex)
			{

				_logger.LogError(ex.Message);
				return new APIResponse<List<GetCommentDTO>>
				{
					IsSuccess = false,
					Message = ResponseMessage.IsError,
					Result = default
				};
			}
		}

		public async Task<APIResponse<CommentResponseDTO>> DeleteAComment(Guid postId, Guid id)
		{
			try
			{
				Post post = await _unitOfWork.Post.GetAsync(u=>u.Id == postId);

				Comment comment;

				if (post != null)
				{
					comment = await _unitOfWork.Comment.GetAsync(u => u.Id == id);
				}
				else
				{
					return new APIResponse<CommentResponseDTO>
					{
						IsSuccess = false,
						Message = "Post not found",
						Result = default
					};
				}

				await _unitOfWork.Comment.RemoveAsync(comment);
				await _unitOfWork.SaveAsync();

				var response = new APIResponse<CommentResponseDTO>
				{
					IsSuccess = true,
					Message = ResponseMessage.IsSuccess,
					Result = _mapper.Map<CommentResponseDTO>(comment)
				};
				response.Result.CommentId = comment.Id;

				return response;
			}
			catch (Exception ex)
			{

				_logger.LogError(ex.Message);
				return new APIResponse<CommentResponseDTO>
				{
					IsSuccess = false,
					Message = ResponseMessage.IsError,
					Result = default
				};
			}
		}
	}
}
