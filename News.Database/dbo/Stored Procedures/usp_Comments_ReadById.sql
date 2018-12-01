-- =============================================
-- Author:      minhhuynh
-- Create date: 20-10-2018
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[usp_Comments_ReadById]
	@CommentId INT
AS
Begin
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
	---------------------------------------------------
	BEGIN TRY

		SELECT  c.* 
		FROM    [Comments] c
		WHERE   c.[CommentId] = @CommentId
	---------------------------------------------------
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
End
