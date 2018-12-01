-- =============================================
-- Author:      minhhuynh
-- Create date: 20-10-2018
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[usp_Categories_ReadById]
	@CategoryId INT
AS
Begin
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
	---------------------------------------------------
	BEGIN TRY

		SELECT  c.* 
		FROM    [Categories] c
		WHERE   c.CategoryId = @CategoryId
	---------------------------------------------------
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
End
