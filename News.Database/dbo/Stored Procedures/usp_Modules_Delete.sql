-- =============================================
-- Author:      minhhuynh
-- Create date: 06-12-2018
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[usp_Modules_Delete]
	@Id INT
AS
Begin
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
	---------------------------------------------------
	 DECLARE @return AS BIT = 1 

		BEGIN TRY 
			BEGIN TRAN; 

		DELETE FROM [dbo].[Modules] WHERE [Id] = @Id
	---------------------------------------------------
	COMMIT 
		END TRY 

		BEGIN CATCH 
			SET @return = 0 
			ROLLBACK TRANSACTION 

			THROW; 
		END CATCH 

		SELECT @return 
End