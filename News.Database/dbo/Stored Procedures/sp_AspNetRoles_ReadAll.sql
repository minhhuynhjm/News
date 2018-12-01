-- =============================================
-- Author:      minhhuynh
-- Create date: 10-11-2018
-- Description:	
-- =============================================
CREATE PROC [dbo].[sp_AspNetRoles_ReadAll]
AS
Begin
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
	---------------------------------------------------
	BEGIN TRY

		SELECT r.*
		FROM [AspNetRoles] r
	---------------------------------------------------
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
End