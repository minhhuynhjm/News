
-- =============================================
-- Author:      minhhuynh
-- Create date: 20-10-2018
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[usp_Categories_Update]
	@CategoryName NVARCHAR(200),
	@Decription NVARCHAR(Max),
	@Parent int,
	@CategoryId int
AS
BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
	---------------------------------------------------
	 DECLARE @return AS BIT = 1 

		BEGIN TRY 
			BEGIN TRAN; 

		 UPDATE [dbo].[Categories]
			SET [Categories].[CategoryName] = @CategoryName, 
				[Categories].[Decription] = @Decription, 
				[Categories].[Parent] = @Parent
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
