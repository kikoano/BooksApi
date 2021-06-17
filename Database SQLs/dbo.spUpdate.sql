CREATE PROCEDURE [dbo].[spUpdate]
	@table nvarchar(50),
	@id int,
	@values nvarchar(max) 
AS
BEGIN
	DECLARE @sql nvarchar(MAX)
	SET @sql = 'UPDATE ' + QUOTENAME(@table) +' SET ' + @values + ' WHERE Id = @id'
	EXEC sp_executesql @sql,N'@id int',@id
END