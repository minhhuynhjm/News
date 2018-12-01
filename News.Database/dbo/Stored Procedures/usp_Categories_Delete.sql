
-- =============================================
-- Author:      minhhuynh
-- Create date: 20-10-2018
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[usp_Categories_Delete]
	@CategoryId INT
AS
BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
	---------------------------------------------------
	 DECLARE @return AS BIT = 1 

		BEGIN TRY 
			BEGIN TRAN; 

		 DElETE FROM [Categories]
		WHERE [Categories].[CategoryId] = @CategoryId
	
	COMMIT 
		END TRY 

		BEGIN CATCH 
			SET @return = 0 
			ROLLBACK TRANSACTION 

			THROW; 
		END CATCH 

		SELECT @return 
END
