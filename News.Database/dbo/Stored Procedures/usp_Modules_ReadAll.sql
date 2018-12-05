-- =============================================
-- Author:      minhhuynh
-- Create date: 05-12-2018
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[usp_Modules_ReadAll]
AS
Begin
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
	---------------------------------------------------
	BEGIN TRY

		SELECT a.*
		FROM [dbo].[Modules] a
	---------------------------------------------------
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
End