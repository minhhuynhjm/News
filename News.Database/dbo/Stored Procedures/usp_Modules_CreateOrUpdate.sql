
CREATE PROCEDURE [dbo].[usp_Modules_CreateOrUpdate]
	@Id INT,
	@Title NVARCHAR(50),
	@Link VARCHAR(50),
	@Sort INT,
	@Isactive BIT
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
			INSERT INTO [dbo].[Modules]					
					([Title],
					[Link],
					[Sort],
					[Isactive])
			VALUES (@Title,
					@Link,
					@Sort,
					@Isactive)
		END
		ELSE
		BEGIN
			UPDATE [dbo].[Modules]
			SET
				[Title] = @Title,
				[Link] = @Link,
				[Sort] = @Sort,
				[Isactive] = @Isactive
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