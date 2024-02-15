using AutoMapper;
using Bob.DataAccess.Repository.IRepository;
using Bob.Model.DTO.PostDTO;
using Bob.Model.DTO.ShoutoutDTO;
using Bob.Model.Entities.Home;
using Bob.Model.Entities;
using Bob.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bob.Model.DTO.CommentDTO;
using Bob.Core.Services.IServices;

namespace Bob.Core.Services
{
	public class CommentService: ICommentService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly ILogger<CommentService> _logger;

		public CommentService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CommentService> logger)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_logger = logger;
		}

		public async Task<APIResponse<CommentResponseDTO>> CreateComment(CreateCommentRequestDTO DTO)
		{
			try
			{
				User user = await _unitOfWork.User.GetAsync(u => u.Id == DTO.UserId);
				Comment comment = _mapper.Map<Comment>(DTO);
				comment.OrganizationId = user.OrganizationId;
				comment.UserId = user.Id;
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

		public async Task<APIResponse<List<GetCommentDTO>>> GetComments()
		{
			try
			{
				IEnumerable<Comment> comment = await _unitOfWork.Comment.GetAllAsync();

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

		public async Task<APIResponse<GetCommentDTO>> GetAComment(Guid id)
		{
			try
			{
				Comment comment = await _unitOfWork.Comment.GetAsync(u => u.Id == id);

				return new APIResponse<GetCommentDTO>
				{
					IsSuccess = true,
					Message = ResponseMessage.IsSuccess,
					Result = _mapper.Map<GetCommentDTO>(comment)
				};

			}
			catch (Exception ex)
			{

				_logger.LogError(ex.Message);
				return new APIResponse<GetCommentDTO>
				{
					IsSuccess = false,
					Message = ResponseMessage.IsError,
					Result = default
				};
			}
		}

		public async Task<APIResponse<CommentResponseDTO>> DeleteAComment(Guid id)
		{
			try
			{
				Comment comment = await _unitOfWork.Comment.GetAsync(u => u.Id == id);
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
