-- =============================================
-- Author:      minhhuynh
-- Create date: 20-10-2018
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[usp_Posts_Create]
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

		INSERT INTO [dbo].[Posts]  
					([PostTitle], 
					[PostContent], 
					[PostDate], 
					[PostModify],
					[PostStatus], 
					[PostDecription], 
					[CommentStatus], 
					[Image], 
					[Tag], 
					[CategoryId], 
					[PostAuthorId]) 

			 VALUES(@PostTitle,
					@PostContent, 
					@PostDate, 
					@PostModify, 
					@PostStatus, 
					@PostDecription, 
					@CommentStatus, 
					@Image, 
					@Tag, 
					@CategoryId, 
					@PostAuthorId)
	
	COMMIT 
		END TRY 

		BEGIN CATCH 
			SET @return = 0 
			ROLLBACK TRANSACTION 

			THROW; 
		END CATCH 

		SELECT @return 
END
