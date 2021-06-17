CREATE PROCEDURE [dbo].[spUpdateBooks]
	@id int,
	@title nvarchar(30),
	@author nvarchar(30),
	@description nvarchar(100)
AS
BEGIN
	UPDATE Books SET title = @title, author = @author, description = @description WHERE Id = @id
END