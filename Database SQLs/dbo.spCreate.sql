CREATE PROCEDURE [dbo].[spCreate]
	@table nvarchar(50),
	@values nvarchar(max) 
AS
BEGIN
	DECLARE @sql nvarchar(MAX)
	SET @sql = 'INSERT INTO '+QUOTENAME(@table)+' VALUES ('+@values+')';
	EXEC sp_executesql @sql
END