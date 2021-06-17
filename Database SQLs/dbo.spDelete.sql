CREATE PROCEDURE [dbo].[spDelete]
	@table nvarchar(50),
	@id int
AS
BEGIN
	DECLARE @sql nvarchar(MAX)
	SET @sql = 'DELETE FROM ' + QUOTENAME(@table) + ' WHERE Id = @id'
	EXEC sp_executesql @sql,N'@id int',@id
END