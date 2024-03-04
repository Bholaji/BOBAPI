using AutoMapper;
using Bob.Core.Exceptions;
using Bob.Core.Services.IServices;
using Bob.DataAccess.Repository.IRepository;
using Bob.Model;
using Bob.Model.DTO.CommentDTO;
using Bob.Model.DTO.PaginationDTO;
using Bob.Model.DTO.PostDTO;
using Bob.Model.DTO.ShoutoutDTO;
using Bob.Model.Entities;
using Bob.Model.Entities.Home;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using InvalidOperationException = Bob.Core.Exceptions.InvalidOperationException;

namespace Bob.Core.Services
{
	public class PostService : IPostService
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
			User user = await _unitOfWork.User.GetAsync(u => u.Id == postRequestDTO.UserId);
			if (user is null)
			{
				throw new NotFoundException($"{nameof(User)} {ResponseMessage.NotFound}");
			}
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

		public async Task<APIResponse<PostResponseDTO>> UpdatePost(UpdatePostRequestDTO postRequestDTO)
		{
			Post oldPost = await _unitOfWork.Post.GetAsync(u => u.Id == postRequestDTO.PostId);

			if (oldPost is null)
			{
				throw new NotFoundException($"{nameof(Post)} {ResponseMessage.NotFound}");
			}

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

		public async Task<APIResponse<List<GetPostDTO>>> GetPosts(PaginationDTO DTO)
		{
			IEnumerable<Post> posts = await _unitOfWork.Post.GetAllAsync(pageSize: DTO.PageSize, pageNumber: DTO.PageNumber);

			if (posts is null)
			{
				throw new NotFoundException($"{nameof(Post)} {ResponseMessage.NotFound}");
			}

			return new APIResponse<List<GetPostDTO>>
			{
				IsSuccess = true,
				Message = ResponseMessage.IsSuccess,
				Result = _mapper.Map<List<GetPostDTO>>(posts)
			};
		}

		public async Task<APIResponse<GetPostDTO>> GetAPost(Guid postId)
		{
			Post post = await _unitOfWork.Post.GetAsync(u => u.Id == postId);
			if (post is null)
			{
				throw new NotFoundException($"{nameof(Post)} {ResponseMessage.NotFound}");
			}
			return new APIResponse<GetPostDTO>
			{
				IsSuccess = true,
				Message = ResponseMessage.IsSuccess,
				Result = _mapper.Map<GetPostDTO>(post)
			};
		}

		public async Task<APIResponse<PostResponseDTO>> DeleteAPost(Guid postId)
		{
			Post post = await _unitOfWork.Post.GetAsync(u => u.Id == postId);

			if (post is null)
			{
				throw new NotFoundException($"{nameof(Post)} {ResponseMessage.NotFound}");
			}

			TimeSpan timeSpan = DateTime.UtcNow - post.CreationDate;
			TimeSpan deleteThreshold = TimeSpan.FromMinutes(30);

			if(timeSpan.TotalMinutes > deleteThreshold.TotalMinutes)
			{
				throw new InvalidOperationException(ResponseMessage.DeletePostError);
			}
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

		//Comment

		public async Task<APIResponse<CommentResponseDTO>> CreateComment(Guid postId, CreateCommentRequestDTO DTO)
		{

			Post post = await _unitOfWork.Post.GetAsync(u => u.Id == postId);

			if(post is null)
			{
				throw new NotFoundException($"{nameof(Post)} {ResponseMessage.NotFound}");
			}

			Comment comment = _mapper.Map<Comment>(DTO);

			comment.OrganizationId = post.OrganizationId;

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

		public async Task<APIResponse<CommentResponseDTO>> UpdateComment(UpdateCommentDTO DTO)
		{
			Comment comment = await _unitOfWork.Comment.GetAsync(u => u.Id == DTO.CommentId);

			if(comment is null)
			{
				throw new NotFoundException($"{nameof(Comment)} {ResponseMessage.NotFound}");
			}

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

		public async Task<APIResponse<List<GetCommentDTO>>> GetComment(CommentPaginationDTO DTO)
		{

			Post post = await _unitOfWork.Post.GetAsync(u => u.Id == DTO.PostId);

			if (post is null)
			{
				throw new NotFoundException($"{nameof(Post)} {ResponseMessage.NotFound}");

			}
			var comment = await _unitOfWork.Comment.GetAllAsync(u=>u.PostId == DTO.PostId,  pageSize: DTO.PageSize, pageNumber: DTO.PageNumber);

			if(comment.Count == 0)
			{
				throw new NotFoundException(ResponseMessage.NoComment);
			}

			return new APIResponse<List<GetCommentDTO>>
			{
				IsSuccess = true,
				Message = ResponseMessage.IsSuccess,
				Result = _mapper.Map<List<GetCommentDTO>>(comment)
			};
		}

		public async Task<APIResponse<CommentResponseDTO>> DeleteAComment(DeletePostDTO DTO)
		{
			Post post = await _unitOfWork.Post.GetAsync(u => u.Id == DTO.PostId);

			Comment comment;

			if (post is null)
			{
				throw new NotFoundException($"{nameof(Post)} {ResponseMessage.NotFound}");
			}
			comment = await _unitOfWork.Comment.GetAsync(u => u.Id == DTO.CommentId);

			if (comment is null)
			{
				throw new NotFoundException($"{nameof(Comment)} {ResponseMessage.NotFound}");
			}

			TimeSpan timeSpan = DateTime.UtcNow - comment.CreationDate;

			//let it come from appsetings
			TimeSpan deleteThreshold = TimeSpan.FromMinutes(30);

			if(timeSpan.TotalMinutes > deleteThreshold.TotalMinutes)
			{
				throw new InvalidOperationException(ResponseMessage.DeleteCommentError);
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
	}
}
