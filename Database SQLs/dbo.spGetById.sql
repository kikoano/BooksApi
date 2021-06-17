CREATE PROCEDURE [dbo].[spGetById]
	@table nvarchar(50),
	@id int
AS
BEGIN
	DECLARE @sql nvarchar(MAX)
	SET @sql = 'SELECT * FROM '+QUOTENAME(@table)+' WHERE Id = @id'
	EXEC sp_executesql @sql,N'@id int',@id
END