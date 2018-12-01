-- =============================================
-- Author:      minhhuynh
-- Create date: 20-10-2018
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[usp_Posts_ReadAll]
AS
Begin
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
	---------------------------------------------------
	BEGIN TRY

		SELECT p.[PostId], FORMAT( p.[PostModify], 'dd MMMM yyyy', 'en-US' ) AS PostModify, p.[PostDecription], p.[PostStatus], p.[PostTitle],p.[Image],  p.[Tag], c.[CategoryId], c.[CategoryName], u.[UserName]
		FROM [dbo].[Posts] p
		INNER JOIN [dbo].[Categories] c ON p.[CategoryId] = c.[CategoryId]
		INNER JOIN [dbo].[AspNetUsers] u ON p.[PostAuthorId] = u.[Id]

		ORDER BY p.[PostId] DESC
	---------------------------------------------------
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
End
