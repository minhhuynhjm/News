
-- =============================================
-- Author:      minhhuynh
-- Create date: 20-10-2018
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[usp_Comments_Update]
	@CommentAuthor      NVARCHAR(100),
	@CommentAuthorEmail NVARCHAR(100),
	@CommentDate        VARCHAR(100),
	@CommentContent     NVARCHAR(1000),
	@Status				BIT,
	@CommentParent      INT,
	@UserId             INT,
	@PostId             INT,
	@CommentId			INT
AS
BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
	---------------------------------------------------
	 DECLARE @return AS BIT = 1 

		BEGIN TRY 
			BEGIN TRAN; 

		 	UPDATE  [dbo].[Comments]
			SET		[Comments].[CommentAuthor] = @CommentAuthor,
					[Comments].[CommentAuthorEmail] = @CommentAuthorEmail,
					[Comments].[CommentDate] = @CommentDate,
					[Comments].[CommentContent] = @CommentContent,
					[Comments].[Status] = @Status,
					[Comments].[CommentParent] = @CommentParent,
					[Comments].[UserId] = @UserId,
					[Comments].[PostId] = @PostId

			WHERE	[Comments].CommentId = @CommentId

	COMMIT 
		END TRY 

		BEGIN CATCH 
			SET @return = 0 
			ROLLBACK TRANSACTION 

			THROW; 
		END CATCH 

		SELECT @return 
END
