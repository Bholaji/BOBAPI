namespace Bob.Core
{
	public static class ResponseMessage
	{
		public const string IsSuccess = "Operation Successful";

		public const string IsError = "An internal error occured";

		public const string NotFound = "Not Found";

		public const string DeletePostError = "Cannot delete the post 30 minutes after posting";

		public const string DeleteCommentError = "Cannot delete the comment 30 minutes after commenting";

		public const string NoComment = "No comment!";
	}
}
