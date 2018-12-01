-- =============================================
-- Author:      minhhuynh
-- Create date: 01-12-2018
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[usp_m_Posts_ReadAll]
AS
BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
	---------------------------------------------------
	BEGIN TRY

	SELECT p.*, c.*

	FROM   [Posts] p JOIN [Categories] c ON
		   p.[CategoryId] = c.[CategoryId]
	
	ORDER BY p.[PostModify] DESC
	---------------------------------------------------
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END