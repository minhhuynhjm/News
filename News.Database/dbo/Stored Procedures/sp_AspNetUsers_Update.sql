
-- =============================================
-- Author:      minhhuynh
-- Create date: 10-11-2018
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[sp_AspNetUsers_Update]
	@Id INT,
	@FullName NVARCHAR(200),
	@UserName VARCHAR(200),
	@Email VARCHAR(200),
	@PhoneNumber VARCHAR(200),
	@Image NVARCHAR(MAX)
AS
BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
	---------------------------------------------------
	 DECLARE @return AS BIT = 1 

		BEGIN TRY 
			BEGIN TRAN; 

		 UPDATE [dbo].[AspNetUsers]
			SET [UserName] = @UserName, 
				[FullName] = @FullName, 
				[Email] = @Email,
				[PhoneNumber] = @PhoneNumber,
				[Image] = @Image
			WHERE [Id] = @Id
	
	COMMIT 
		END TRY 

		BEGIN CATCH 
			SET @return = 0 
			ROLLBACK TRANSACTION 

			THROW; 
		END CATCH 

		SELECT @return 
END
