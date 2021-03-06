﻿-- =============================================
-- Author:      minhhuynh
-- Create date: 25-11-2018
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[usp_Posts_ReadByCategoryId]
	@CategoryId INT,
	@PageNumber INT,
	@PageSize   INT,
	@TotalRows INT OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
	---------------------------------------------------
	BEGIN TRY

		SET NOCOUNT ON;

		SELECT @TotalRows = Count([dbo].[Posts].[PostId])
		FROM   [dbo].[Posts]
		WHERE [Posts].CategoryId = @CategoryId

	SELECT p.[PostId], FORMAT( p.[PostModify], 'dd MMMM yyyy', 'en-US' ) AS PostModify, p.[PostTitle], p.[Tag], p.[Image], p.[PostDecription],
		   c.[CategoryId], c.[CategoryName]

	FROM   [Posts] p INNER JOIN [Categories] c ON
		   p.[CategoryId] = c.[CategoryId]
	WHERE c.[CategoryId] = @CategoryId

	ORDER BY p.[PostId] DESC
	OFFSET @PageSize * (@PageNumber - 1) ROWS
	FETCH NEXT @PageSize ROWS ONLY OPTION (RECOMPILE);
	---------------------------------------------------
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END
