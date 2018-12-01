﻿-- =============================================
-- Author:      minhhuynh
-- Create date: 09-11-2018
-- Description:	
-- =============================================
CREATE PROC [dbo].[sp_AspNetUsers_ReadAll]
AS
Begin
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
	---------------------------------------------------
	BEGIN TRY

		SELECT u.[Id], u.[FullName], u.[Email], u.[UserName], u.[PhoneNumber], r.[Name] AS RoleName, r.Id AS RoleId
		FROM [dbo].[AspNetUsers] u 
		LEFT JOIN [AspNetUserRoles] ur 
		ON u.[Id] = ur.[UserId]
		LEFT JOIN [AspNetRoles] r
		ON ur.[RoleId] = r.[Id]
	---------------------------------------------------
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
End