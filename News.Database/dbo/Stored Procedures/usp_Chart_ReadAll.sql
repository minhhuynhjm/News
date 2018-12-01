-- =============================================
-- Author:      minhhuynh
-- Create date: 26-10-2018
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[usp_Chart_ReadAll]
@User INT OUTPUT,
@Post INT OUTPUT,
@Comment INT OUTPUT
AS
Begin
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
	---------------------------------------------------
	BEGIN TRY
	SELECT @User = COUNT([dbo].[AspNetUsers].[Id]) 
	FROM [dbo].[AspNetUsers]

	SELECT @Post = COUNT([dbo].[Posts].[PostId])
	FROM   [dbo].[Posts]

	SELECT @Comment = COUNT([dbo].[Comments].[CommentId])
	FROM [dbo].[Comments]
	---------------------------------------------------
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
End
