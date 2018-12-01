
-- =============================================
-- Author:      minhhuynh
-- Create date: 10-11-2018
-- Description:	
-- =============================================
CREATE PROC [dbo].[sp_AspNetUserRoles_Delete]
	@UserId INT
AS
BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
	---------------------------------------------------
	 DECLARE @return AS BIT = 1 

		BEGIN TRY 
			BEGIN TRAN; 

			DELETE FROM [AspNetUserRoles] WHERE [UserId] = @UserId
	
		COMMIT 
		END TRY 

		BEGIN CATCH 
			SET @return = 0 
			ROLLBACK TRANSACTION 

			THROW; 
		END CATCH 

		SELECT @return 
END
