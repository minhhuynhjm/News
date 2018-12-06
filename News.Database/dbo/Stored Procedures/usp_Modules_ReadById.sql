-- =============================================
-- Author:      minhhuynh
-- Create date: 06-12-2018
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[usp_Modules_ReadById]
	@Id INT
AS
Begin
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
	---------------------------------------------------
	BEGIN TRY

		SELECT  c.* 
		FROM    [Modules] c
		WHERE   c.[Id] = @Id
	---------------------------------------------------
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
End