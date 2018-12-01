-- =============================================
-- Author:      minhhuynh
-- Create date: 12-11-2018
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[usp_Comments_ReadByPostId]
	@Id INT
AS
Begin
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
	---------------------------------------------------
	BEGIN TRY

		SELECT  c.* , u.[Id], u.[Image]
		FROM    [Comments] c
		INNER JOIN [Posts] p ON c.[PostId] = p.[PostId]		  		
		INNER JOIN [AspNetUsers] u ON c.[UserId] = u.[Id]
		WHERE p.[PostId] = @Id
	---------------------------------------------------
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
End