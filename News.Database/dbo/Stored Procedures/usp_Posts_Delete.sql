
-- =============================================
-- Author:      minhhuynh
-- Create date: 20-10-2018
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[usp_Posts_Delete]
	@PostId int
AS
BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
	---------------------------------------------------
	 DECLARE @return AS BIT = 1 

		BEGIN TRY 
			BEGIN TRAN; 

		 DELETE FROM [Posts] WHERE [Posts].[PostId] = @PostId
	
	COMMIT 
		END TRY 

		BEGIN CATCH 
			SET @return = 0 
			ROLLBACK TRANSACTION 

			THROW; 
		END CATCH 

		SELECT @return 
END
