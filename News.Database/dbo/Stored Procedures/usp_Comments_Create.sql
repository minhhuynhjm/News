
-- =============================================
-- Author:      minhhuynh
-- Create date: 20-10-2018
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[usp_Comments_Create]
	@CommentAuthor      NVARCHAR(100),
	@CommentAuthorEmail NVARCHAR(100),
	@CommentDate        VARCHAR(100),
	@CommentContent     NVARCHAR(1000),
	@Status				BIT,
	@CommentParent      INT,
	@UserId             INT,
	@PostId             INT,
	@CommentId			INT OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
	---------------------------------------------------

		 	INSERT INTO [dbo].[Comments] 
						([CommentAuthor], 
						[CommentAuthorEmail], 
						[CommentDate], 
						[CommentContent],
						[Status], 
						[CommentParent], 
						[UserId], 
						[PostId])

				 VALUES (@CommentAuthor, 
						@CommentAuthorEmail, 
						@CommentDate, 
						@CommentContent, 
						@Status, 
						@CommentParent, 
						@UserId, 
						@PostId)	

	SET @CommentId =  CAST(SCOPE_IDENTITY() as int) 
	
END