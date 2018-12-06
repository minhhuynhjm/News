-- =============================================
-- Author:      minhhuynh
-- Create date: 26-10-2018
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[usp_Chart_ReadToday]
@Post INT OUTPUT,
@Comment INT OUTPUT
AS
Begin
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
	---------------------------------------------------
	BEGIN TRY
	SET @Comment = 0
	SET @Post = 0

	SELECT @Comment = COUNT(CommentId)
	FROM Comments
	WHERE DATEDIFF(DAY, CommentDate, GETDATE()) = 0
	GROUP BY DATEDIFF(DAY, CommentDate, GETDATE())

	SELECT @Post = COUNT(PostId)
	FROM Posts
	WHERE DATEDIFF(DAY, PostDate, GETDATE()) = 0
	GROUP BY DATEDIFF(DAY, PostDate, GETDATE())

	---------------------------------------------------
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
End