-- =============================================
-- Author:      minhhuynh
-- Create date: 20-10-2018
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[usp_Posts_ReadByPaging]
	@Title NVARCHAR(200),	
	@Take INT,	
	@Skip INT,
	@Output INT OUT	
AS
Begin
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
	---------------------------------------------------
	BEGIN TRY
		IF(@Title = '')
		BEGIN
			SELECT p.[PostId], FORMAT( p.[PostModify], 'dd MMMM yyyy', 'en-US' ) AS PostModify, p.[PostDecription], p.[PostStatus], p.[PostTitle],p.[Image],  p.[Tag], c.[CategoryId], c.[CategoryName], u.[UserName]
			FROM [dbo].[Posts] p
			INNER JOIN [dbo].[Categories] c ON p.[CategoryId] = c.[CategoryId]
			INNER JOIN [dbo].[AspNetUsers] u ON p.[PostAuthorId] = u.[Id]

			ORDER BY p.[PostModify] DESC
			OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY;

			SET @Output =  (SELECT COUNT(p.[PostId]) FROM [dbo].[Posts] p)
		END
		ELSE
		BEGIN
			SELECT p.[PostId], FORMAT( p.[PostModify], 'dd MMMM yyyy', 'en-US' ) AS PostModify, p.[PostDecription], p.[PostStatus], p.[PostTitle],p.[Image],  p.[Tag], c.[CategoryId], c.[CategoryName], u.[UserName]
			FROM [dbo].[Posts] p
			INNER JOIN [dbo].[Categories] c ON p.[CategoryId] = c.[CategoryId]
			INNER JOIN [dbo].[AspNetUsers] u ON p.[PostAuthorId] = u.[Id]
			WHERE p.[PostTitle] LIKE '%'+ @Title +'%'
			ORDER BY p.[PostId] DESC
			OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY;

			SET @Output =  (SELECT COUNT(p.[PostModify]) FROM [dbo].[Posts] p
							WHERE p.[PostTitle] LIKE '%'+ @Title +'%')
		END
	---------------------------------------------------
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
End