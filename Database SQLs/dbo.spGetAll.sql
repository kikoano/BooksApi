CREATE PROCEDURE [dbo].[spGetAll]
	@table nvarchar(50)
AS
BEGIN
	DECLARE @sql nvarchar(MAX)
	SET @sql = 'SELECT * FROM '+QUOTENAME(@table)
	EXEC sp_executesql @sql
END