-- =============================================  
-- Author:      minhhuynh  -- Create date: 20-10-2018  
-- Description:   -- =============================================  
CREATE PROCEDURE [dbo].[usp_Comments_ReadAllPaging] 
	@Take int,
	@Skip int,
	@Output int OUT,
	@SortDataField varchar(100),
	@SortOrder varchar(100),
	@Keyword nvarchar(256)
AS
BEGIN
  SET NOCOUNT ON;
  SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

  DECLARE @CountResults TABLE (CountReturned INT)
  DECLARE @countRows VARCHAR(2000),
          @whereQuery NVARCHAR(max) = '1=1 ',
          @sqlStatement VARCHAR(2000)
  BEGIN TRY
    IF (@Keyword <> '')
      SELECT
        @whereQuery = CONCAT(@whereQuery, ' AND  p.[PostTitle] LIKE (N''%', @Keyword, '%'') OR c.[CommentAuthor] LIKE (N''%', @Keyword, '%'')')
    SELECT
      @sqlStatement =
      '    SELECT c.* , p.[PostTitle]    
		   FROM [dbo].[Comments] c    
		   INNER JOIN [dbo].[Posts] p   
		   ON c.[PostId] = p.[PostId]' +
		   ' WHERE ' + @whereQuery +
           ' ORDER BY c.[' + @SortDataField + '] ' + @SortOrder +
           ' OFFSET ' + CAST(@Skip AS varchar(20)) + ' ROWS' +
          ' FETCH NEXT ' + CAST(@Take AS varchar(20)) + ' ROWS ONLY';
    EXEC (@sqlStatement)
    SET @countRows = 
	'SELECT COUNT(c.[CommentId])     
    FROM [dbo].[Comments] c     
    INNER JOIN [dbo].[Posts] p     
    ON c.[PostId] = p.[PostId] WHERE ' + @whereQuery
    INSERT INTO @CountResults
    EXEC (@countRows)
    SET @Output = (SELECT
      CountReturned
    FROM @CountResults)
  END TRY
  BEGIN CATCH
    THROW;
  END CATCH
END
