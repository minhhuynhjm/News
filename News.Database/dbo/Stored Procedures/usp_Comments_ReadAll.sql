-- =============================================
-- Author:      minhhuynh
-- Create date: 20-10-2018
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[usp_Comments_ReadAll]
AS
Begin
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
	---------------------------------------------------
	BEGIN TRY

		SELECT c.* , p.[PostTitle]
		FROM [dbo].[Comments] c
		INNER JOIN [dbo].[Posts] p
		ON c.[PostId] = p.[PostId]

	---------------------------------------------------
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
End
