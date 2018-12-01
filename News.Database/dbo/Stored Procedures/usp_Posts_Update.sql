
-- =============================================
-- Author:      minhhuynh
-- Create date: 20-10-2018
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[usp_Posts_Update]
	@PostId INT,
	@PostTitle NVARCHAR(Max),
	@PostContent NVARCHAR(Max),
	@PostStatus BIT,
	@CommentStatus BIT,
	@PostDate VARCHAR(200),
	@PostModify VARCHAR(200),
	@PostDecription NVARCHAR(Max),
	@Image NVARCHAR(MAX),
	@Tag VARCHAR(300),
	@CategoryId INT,
	@PostAuthorId INT
AS
BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
	---------------------------------------------------
	 DECLARE @return AS BIT = 1 

		BEGIN TRY 
			BEGIN TRAN; 

		UPDATE [dbo].[Posts]

	SET [Posts].[PostTitle] = @PostTitle,
		[Posts].[PostContent] = @PostContent,
		[Posts].[PostStatus] = @PostStatus,
		[Posts].[CommentStatus] = @CommentStatus,
		[Posts].[PostDate] = @PostDate,
		[Posts].[PostModify] = @PostModify,
		[Posts].[PostDecription] = @PostDecription,
		[Posts].[Image] = @Image,
		[Posts].[Tag] = @Tag,
		[Posts].[CategoryId] = @CategoryId,
		[Posts].[PostAuthorId] = @PostAuthorId

	WHERE [Posts].PostId = @PostId
	
	COMMIT 
		END TRY 

		BEGIN CATCH 
			SET @return = 0 
			ROLLBACK TRANSACTION 

			THROW; 
		END CATCH 

		SELECT @return 
END
