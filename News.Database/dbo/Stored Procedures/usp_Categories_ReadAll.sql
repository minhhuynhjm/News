﻿-- =============================================
-- Author:      minhhuynh
-- Create date: 20-10-2018
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[usp_Categories_ReadAll]
AS
Begin
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
	---------------------------------------------------
	BEGIN TRY

		SELECT [Categories].* 
		FROM [dbo].[Categories]
	---------------------------------------------------
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
End
