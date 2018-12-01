-- =============================================
-- Author:      minhhuynh
-- Create date: 20-10-2018
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[usp_Comments_ReadAllPaging]
	@Take INT,	
	@Skip INT,
	@Output INT OUT
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

		ORDER BY c.[CommentId] DESC
		OFFSET @Skip ROWS      
		FETCH NEXT @Take ROWS ONLY;

		SET @Output = (SELECT COUNT(*)
					   FROM [dbo].[Comments] c)
										
	---------------------------------------------------
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
End
