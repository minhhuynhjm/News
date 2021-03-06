﻿
-- =============================================
-- Author:      minhhuynh
-- Create date: 20-10-2018
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[usp_Categories_Create]
	@CategoryName NVARCHAR(200),
	@Decription NVARCHAR(Max),
	@Parent int
AS
BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
	---------------------------------------------------
	 DECLARE @return AS BIT = 1 

		BEGIN TRY 
			BEGIN TRAN; 

		 INSERT INTO [dbo].[Categories] 
					([CategoryName],
					 [Decription],
					 [Parent])
			 VALUES (@CategoryName, 
					 @Decription, 
					 @Parent)
	
	COMMIT 
		END TRY 

		BEGIN CATCH 
			SET @return = 0 
			ROLLBACK TRANSACTION 

			THROW; 
		END CATCH 

		SELECT @return 
END
