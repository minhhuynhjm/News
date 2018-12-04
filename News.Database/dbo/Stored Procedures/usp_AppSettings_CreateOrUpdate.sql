
CREATE PROCEDURE [dbo].[usp_AppSettings_CreateOrUpdate]
	@Id INT,
	@Name NVARCHAR(100),
	@Description NVARCHAR(500),
	@Logo	NVARCHAR(300),
	@Company NVARCHAR(100),
	@Address NVARCHAR(100),
	@Phone VARCHAR(50),
	@Email VARCHAR(50),
	@Website VARCHAR(50)
AS
BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
	---------------------------------------------------
	 DECLARE @return AS BIT = 1 

	BEGIN TRY 
		BEGIN TRAN; 

		IF(@Id = 0)
		BEGIN
			INSERT INTO [dbo].[AppSettings]
					([Id],
					[Name],
					[Description],
					[Logo],
					[Company],
					[Address],
					[Phone],
					[Email],
					[Website])
			VALUES (1,
					@Name,
					@Description,
					@Logo,
					@Company,
					@Address,
					@Phone,
					@Email,
					@Website)
		END
		ELSE
		BEGIN
			UPDATE [dbo].[AppSettings]
			SET
				[Name] = @Name,
				[Description] = @Description,
				[Logo] = @Logo,
				[Company] = @Company,
				[Address] = @Address,
				[Phone] = @Phone,
				[Email] = @Email,
				[Website] = @Website
			WHERE [Id] = @Id
		END
	
	COMMIT 
		END TRY 

		BEGIN CATCH 
			SET @return = 0 
			ROLLBACK TRANSACTION 

			THROW; 
		END CATCH 

		SELECT @return 
END