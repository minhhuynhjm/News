-- =============================================
-- Author:      minhhuynh
-- Create date: 25-10-2018
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[usp_Posts_ReadAllTag]
AS
Begin
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
	---------------------------------------------------
	BEGIN TRY

		SELECT p.[Tag]
		FROM [dbo].[Posts] p
	---------------------------------------------------
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
End