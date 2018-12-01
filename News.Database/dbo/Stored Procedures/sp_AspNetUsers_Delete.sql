-- =============================================
-- Author:      minhhuynh
-- Create date: 10-11-2018
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[sp_AspNetUsers_Delete]
	@Id INT
AS
Begin
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
	---------------------------------------------------
	 DECLARE @return AS BIT = 1 

		BEGIN TRY 
			BEGIN TRAN; 

			Delete FROM [dbo].[AspNetUsers] WHERE [Id] = @Id

		COMMIT 
		END TRY 

		BEGIN CATCH 
			SET @return = 0 
			ROLLBACK TRANSACTION 

			THROW; 
		END CATCH 

		SELECT @return 
END