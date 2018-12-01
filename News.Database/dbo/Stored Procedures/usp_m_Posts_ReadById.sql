-- =============================================
-- Author:      minhhuynh
-- Create date: 28-11-2018
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[usp_m_Posts_ReadById]
	@PostId INT
AS
BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
	---------------------------------------------------
	BEGIN TRY

	SELECT p.*, c.*

	FROM   [Posts] p JOIN [Categories] c ON
		   p.[CategoryId] = c.[CategoryId]
	WHERE p.[PostId] = @PostId
	---------------------------------------------------
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END